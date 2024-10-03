using System;
using System.Collections.Generic;
using System.IO;
using B20.Architecture.Serialization.Context;
using PrefabCreator.Api;
using Serialization.Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PrefabCreator.Impl
{
    public class PrefabCreatorApiLogic: PrefabCreatorApi
    {
        private void CreatePrefab(PrefabBlueprint blueprint, string prefabsPath)
        {
            float padding = 25f;
            float spacing = 25f;
            
            // Create the prefab object with a given name
            GameObject gameObject = new GameObject(blueprint.GetName(), typeof(RectTransform), typeof(Image));

            // Retrieve the type from the given typeName
            var type = Type.GetType(blueprint.GetViewType());

            // Check if the type is valid
            if (type == null)
            {
                Debug.LogError($"Type '{blueprint.GetViewType()}' not found.");
                return;
            }

            // Add the component of the retrieved type to the GameObject
            gameObject.AddComponent(type);
            
            // Set the parent object as a UI element with RectTransform
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            // Step 2: Add an Image component with a grey color to visualize the container size
            Image backgroundImage = gameObject.GetComponent<Image>();
            backgroundImage.color = Color.grey;

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
                type.GetField(child.GetName(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(gameObject.GetComponent(type), component);
                
                childY -= childObject.GetComponent<RectTransform>().sizeDelta.y / 2 + spacing;
            }

            // Step 11: Save the CreatedGameView GameObject as a prefab
            string prefabPath = $"{prefabsPath}/{blueprint.GetName()}.prefab";
            bool success = false;
            PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath, out success);

            // Clean up
            GameObject.DestroyImmediate(gameObject);

            Debug.Log($"{blueprint.GetName()} Prefab created at: {prefabPath}, success: {success}");
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

        public void StartModulePrefabs(string modulesPath, string moduleName)
        {
            string prefabsPath = Path.Combine(modulesPath, moduleName, "Prefabs");

            var blueprints = ReadPrefabBlueprints(prefabsPath);
            foreach (var blueprint in blueprints)
            {
                CreatePrefab(blueprint, prefabsPath);
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
