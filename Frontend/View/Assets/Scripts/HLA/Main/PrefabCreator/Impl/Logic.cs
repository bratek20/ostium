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
        
        private string Path => $"{prefabsPath}/{blueprint.GetName()}.prefab";
        
        private BlueprintType BlueprintType => blueprint.GetBlueprintType();
        private Type ViewType => Type.GetType(blueprint.GetViewType());
        
        public void Create()
        {
            GameObject gameObject = new GameObject(blueprint.GetName(), typeof(RectTransform), typeof(Image));
            
            gameObject.AddComponent(ViewType);
            
            Image backgroundImage = gameObject.GetComponent<Image>();
            Color backgroundColor = Color.grey;
            if (BlueprintType == BlueprintType.Window)
            {
                backgroundColor = new Color(0.49f, 0.65f, 0.69f);
            }
            backgroundImage.color = backgroundColor;
            
            Fill(gameObject);
        }

        public void Delete()
        {
            AssetDatabase.DeleteAsset(Path);    
        }
        
        private void SaveAndDestroyTmpObject(GameObject gameObject)
        {
            bool success = false;
            PrefabUtility.SaveAsPrefabAsset(gameObject, Path, out success);
            
            Debug.Log($"{gameObject.name} Prefab saved at: {Path}, success: {success}");
            
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
            var containerSize = new Vector2(containerWidth, containerHeight);
            if (BlueprintType == BlueprintType.Window)
            {
                containerSize = new Vector2(1080, 1920);
            }
            rectTransform.sizeDelta = containerSize;

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
                
                var x = gameObject.GetComponent(ViewType);
                ViewType.GetField(child.GetName(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(x, component);
                
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
            string prefabsPath = GetPrefabsPath(modulesPath, moduleName);

            var blueprintsLogic = ReadPrefabBlueprints(prefabsPath)
                .OrderBy(blueprint => blueprint.GetCreationOrder())
                .Select(blueprint => new PrefabBlueprintLogic(blueprint, prefabsPath));
            
            foreach (var blueprintLogic in blueprintsLogic)
            {
                blueprintLogic.Create();
            }
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

            var blueprintsLogic = ReadPrefabBlueprints(path)
                .OrderByDescending(b => b.GetCreationOrder())
                .Select(b => new PrefabBlueprintLogic(b, path));

            foreach (var blueprintLogic in blueprintsLogic)
            {
                blueprintLogic.Delete();
            }
        }
        
        private String GetPrefabsPath(string modulesPath, string moduleName)
        {
            return Path.Combine(modulesPath, moduleName, "Prefabs");
        }
    }
}
