using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using B20.Architecture.Serialization.Context;
using PrefabCreator.Api;
using Serialization.Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PrefabCreator.Impl
{
    public class PrefabBlueprintLogic
    {
        private PrefabBlueprint blueprint;
        private string prefabsPath;

        public PrefabBlueprintLogic(PrefabBlueprint blueprint, string prefabsPath)
        {
            this.blueprint = blueprint;
            this.prefabsPath = prefabsPath;
        }
        
        private Type ViewType => Type.GetType(blueprint.GetViewType());
        
        public void Create()
        {
            GameObject gameObject = new GameObject(blueprint.GetName(), typeof(RectTransform), typeof(Image));
            
            gameObject.AddComponent(ViewType);
            
            Image backgroundImage = gameObject.GetComponent<Image>();
            backgroundImage.color = Color.grey;
            
            //SaveAndDestroyTmpObject(gameObject);
            Fill(gameObject);
        }
        
        private void SaveAndDestroyTmpObject(GameObject gameObject)
        {
            string prefabPath = $"{prefabsPath}/{gameObject.name}.prefab";
            
            bool success = false;
            PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath, out success);
            
            Debug.Log($"{gameObject.name} Prefab saved at: {prefabPath}, success: {success}");
            
            GameObject.DestroyImmediate(gameObject);
        }
        
                
        private void Fill(GameObject gameObject)
        {
            float padding = 25f;
            float spacing = 25f;

            //GameObject gameObject = (GameObject)PrefabUtility.InstantiatePrefab(GetPrefabOfComponentType(blueprint.GetViewType()));
            var rectTransform = gameObject.GetComponent<RectTransform>();

            float containerWidth = 0;
            float containerHeight = 0;
            
            foreach (var child in blueprint.GetChildren())
            {
                GameObject prefab = GetPrefabOfComponentType(child.GetViewType());
                GameObject childObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, gameObject.transform);
                childObject.name = child.GetName();
                RectTransform childRect = childObject.GetComponent<RectTransform>();
                containerWidth = Mathf.Max(containerWidth, childRect.sizeDelta.x);
                containerHeight += childRect.sizeDelta.y + spacing;
            }
            
            if (blueprint.GetChildren().Count > 0)
            {
                containerHeight -= spacing;
            }
            
            containerWidth += 2 * padding;
            containerHeight += 2 * padding;
            // Set the size of the container
            rectTransform.sizeDelta = new Vector2(containerWidth, containerHeight);

            var childY = containerHeight / 2 - padding;
            foreach (var child in blueprint.GetChildren())
            {
                GameObject childObject = gameObject.transform.Find(child.GetName()).gameObject;
                var component = childObject.GetComponent(child.GetViewType());
                childY -= childObject.GetComponent<RectTransform>().sizeDelta.y / 2;
                childObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, childY);
                
                if (component == null)
                {
                    Debug.LogError($"Component of type '{child.GetViewType()}' not found on GameObject '{child.GetName()}'.");
                    return;
                }
                ViewType.GetField(child.GetName(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(gameObject.GetComponent(ViewType), component);
                
                childY -= childObject.GetComponent<RectTransform>().sizeDelta.y / 2 + spacing;
            }

            SaveAndDestroyTmpObject(gameObject);
        }

                
        private GameObject GetPrefabOfComponentType(string typeName)
        {
            // Find all prefab asset GUIDs in the project
            string[] allPrefabGuids = AssetDatabase.FindAssets("t:Prefab");

            // Iterate through each prefab
            foreach (string guid in allPrefabGuids)
            {
                // Get the asset path from the GUID
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // Load the prefab at that path
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (prefab != null)
                {
                    // Try to get the component of the specified type
                    var component = prefab.GetComponent(typeName);

                    // If the component exists, return the prefab
                    if (component != null)
                    {
                        return prefab;
                    }
                }
            }

            throw new Exception($"Prefab for component type {typeName} not found");
        }
    }
    
    public class PrefabCreatorApiLogic: PrefabCreatorApi
    {
        public void StartModulePrefabs(string modulesPath, string moduleName)
        {
            string prefabsPath = Path.Combine(modulesPath, moduleName, "Prefabs");

            var blueprintsLogic = ReadPrefabBlueprints(prefabsPath)
                .Select(blueprint => new PrefabBlueprintLogic(blueprint, prefabsPath));
            
            foreach (var blueprintLogic in blueprintsLogic)
            {
                blueprintLogic.Create();
            }
            //
            // foreach (var blueprintLogic in blueprintsLogic)
            // {
            //     blueprintLogic.Fill();
            // }
        }

        private List<PrefabBlueprint> ReadPrefabBlueprints(string fullPath)
        {
            // Get all JSON files in the directory
            string[] jsonFiles = Directory.GetFiles(fullPath, "*.json");

            var serializer = SerializationFactory.CreateSerializer();
            List<PrefabBlueprint> blueprints = new List<PrefabBlueprint>();
            foreach (var jsonFile in jsonFiles)
            {
                string jsonContent = File.ReadAllText(jsonFile);
                var b = serializer.Deserialize<PrefabBlueprint>(SerializedValue.Create(jsonContent, SerializationType.JSON));
                blueprints.Add(b);
            }
            
            return blueprints;
        }

        public void DeleteModulePrefabs(string modulesPath, string moduleName)
        {
            var path = GetPrefabsPath(modulesPath, moduleName);
            
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { path });
            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                AssetDatabase.DeleteAsset(assetPath);
            }
        }
        
        private String GetPrefabsPath(string modulesPath, string moduleName)
        {
            return Path.Combine(modulesPath, moduleName, "Prefabs");
        }
    }
}
