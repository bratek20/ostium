using System;
using System.Collections.Generic;
using PrefabCreator.Api;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PrefabCreator.Impl
{
    public class PrefabCreatorApiLogic: PrefabCreatorApi
    {
        public class Field
        {
            public string name;
            public string type;
        }
        
        public class Args
        {
            public string prefabDirPath;
            public string prefabName;
            public string viewTypeName;
            public List<Field> fields = new List<Field>();
        }

        public void CreatePrefab(Args args)
        {
            float padding = 25f;
            float spacing = 25f;
            
            // Create the prefab object with a given name
            GameObject createdGameViewObject = new GameObject(args.prefabName, typeof(RectTransform), typeof(Image));

            // Retrieve the type from the given typeName
            var type = Type.GetType(args.viewTypeName);

            // Check if the type is valid
            if (type == null)
            {
                Debug.LogError($"Type '{args.viewTypeName}' not found.");
                return;
            }

            // Add the component of the retrieved type to the GameObject
            createdGameViewObject.AddComponent(type);
            
            // Set the parent object as a UI element with RectTransform
            RectTransform rectTransform = createdGameViewObject.GetComponent<RectTransform>();

            // Step 2: Add an Image component with a grey color to visualize the container size
            Image backgroundImage = createdGameViewObject.GetComponent<Image>();
            backgroundImage.color = Color.grey;

            float containerWidth = 0;
            float containerHeight = 0;
            
            foreach (var field in args.fields)
            {
                GameObject prefab = GetPrefabOfComponentType(field.type);
                GameObject childObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, createdGameViewObject.transform);
                childObject.name = field.name;
                RectTransform childRect = childObject.GetComponent<RectTransform>();
                containerWidth = Mathf.Max(containerWidth, childRect.sizeDelta.x);
                containerHeight += childRect.sizeDelta.y + spacing;
            }
            if (args.fields.Count > 0)
            {
                containerHeight -= spacing;
            }
            
            containerWidth += 2 * padding;
            containerHeight += 2 * padding;
            // Set the size of the container
            rectTransform.sizeDelta = new Vector2(containerWidth, containerHeight);

            var childY = containerHeight / 2 - padding;
            foreach (var field in args.fields)
            {
                GameObject childObject = createdGameViewObject.transform.Find(field.name).gameObject;
                var component = childObject.GetComponent(field.type);
                childY -= childObject.GetComponent<RectTransform>().sizeDelta.y / 2;
                childObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, childY);
                
                if (component == null)
                {
                    Debug.LogError($"Component of type '{field.type}' not found on GameObject '{field.name}'.");
                    return;
                }
                type.GetField(field.name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewObject.GetComponent(type), component);
                
                childY -= childObject.GetComponent<RectTransform>().sizeDelta.y / 2 + spacing;
            }

            string folderPath = args.prefabDirPath;
            EnsurePathExists(folderPath);

            // Step 11: Save the CreatedGameView GameObject as a prefab
            string prefabPath = $"{folderPath}/{args.prefabName}.prefab";
            bool success = false;
            PrefabUtility.SaveAsPrefabAsset(createdGameViewObject, prefabPath, out success);

            // Clean up
            GameObject.DestroyImmediate(createdGameViewObject);

            Debug.Log($"{args.prefabName} Prefab created at: {prefabPath}, success: {success}");
        }
        
        private void EnsurePathExists(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", path);
            }
        }

        public void StartModule(string modulesPath, string moduleName)
        {
            throw new NotImplementedException();
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
}
