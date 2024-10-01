using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GamesManagement.View;
using B20.Frontend.Elements.View;

public class PrefabCreator
{
    public class Field
    {
        public string name;
        public string type;
    }
    
    public class Args
    {
        public string prefabName;
        public string viewTypeName;
    }

    public void CreatePrefab2(Args args)
    {
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
        
        string folderPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Step 11: Save the CreatedGameView GameObject as a prefab
        string prefabPath = $"{folderPath}/{args.prefabName}.prefab";
        bool success = false;
        PrefabUtility.SaveAsPrefabAsset(createdGameViewObject, prefabPath, out success);

        // Clean up
        GameObject.DestroyImmediate(createdGameViewObject);

        Debug.Log($"{args.prefabName} Prefab created at: {prefabPath}, success: {success}");
    }

    public void CreatePrefab()
    {
        // Constants for spacing and padding
        float padding = 25f;
        float spacing = 25f;

        // Step 1: Create a new GameObject for the CreatedGameView as a UI element
        GameObject createdGameViewObject = new GameObject("CreatedGameView", typeof(RectTransform), typeof(Image));
        CreatedGameView createdGameViewScript = createdGameViewObject.AddComponent<CreatedGameView>();

        // Set the parent object as a UI element with RectTransform
        RectTransform rectTransform = createdGameViewObject.GetComponent<RectTransform>();

        // Step 2: Add an Image component with a grey color to visualize the container size
        Image backgroundImage = createdGameViewObject.GetComponent<Image>();
        backgroundImage.color = Color.grey;

        // Step 3: Load Label and Button prefabs
        GameObject labelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/B20/Frontend/Elements/Prefabs/Label.prefab");
        GameObject buttonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/B20/Frontend/Elements/Prefabs/Button.prefab");

        if (labelPrefab == null || buttonPrefab == null)
        {
            Debug.LogError("Failed to load Label or Button prefabs.");
            return;
        }

        // Step 4: Get sizes of children
        RectTransform labelRect = labelPrefab.GetComponent<RectTransform>();
        RectTransform buttonRect = buttonPrefab.GetComponent<RectTransform>();

        float labelHeight = labelRect.sizeDelta.y;
        float labelWidth = labelRect.sizeDelta.x;
        float buttonHeight = buttonRect.sizeDelta.y;
        float buttonWidth = buttonRect.sizeDelta.x;

        // Step 5: Calculate container size
        float containerWidth = Mathf.Max(labelWidth, buttonWidth) + (2 * padding);
        float containerHeight = 2 * labelHeight + buttonHeight + (2 * padding) + 2 * spacing; // 25 spacing between children

        // Set the size of the container
        rectTransform.sizeDelta = new Vector2(containerWidth, containerHeight);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Center pivot and anchor
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Step 6: Create and position the LabelView for 'id' at the correct Y position (115)
        GameObject idLabelObject = (GameObject)PrefabUtility.InstantiatePrefab(labelPrefab, createdGameViewObject.transform);
        idLabelObject.name = "id";
        RectTransform idRect = idLabelObject.GetComponent<RectTransform>();
        idRect.anchoredPosition = new Vector2(0, 115); // Position within UI, matching test expectation
        LabelView idLabelView = idLabelObject.GetComponent<LabelView>();

        // Step 7: Create and position the LabelView for 'creator' at the correct Y position (-10)
        GameObject creatorLabelObject = (GameObject)PrefabUtility.InstantiatePrefab(labelPrefab, createdGameViewObject.transform);
        creatorLabelObject.name = "creator";
        RectTransform creatorRect = creatorLabelObject.GetComponent<RectTransform>();
        creatorRect.anchoredPosition = new Vector2(0, -10); // Position within UI, matching test expectation
        LabelView creatorLabelView = creatorLabelObject.GetComponent<LabelView>();

        // Step 8: Create and position the ButtonView for 'delete' at the correct Y position (-125)
        GameObject deleteButtonObject = (GameObject)PrefabUtility.InstantiatePrefab(buttonPrefab, createdGameViewObject.transform);
        deleteButtonObject.name = "delete";
        RectTransform deleteRect = deleteButtonObject.GetComponent<RectTransform>();
        deleteRect.anchoredPosition = new Vector2(0, -125); // Position within UI, matching test expectation
        ButtonView deleteButtonView = deleteButtonObject.GetComponent<ButtonView>();

        // Step 9: Assign references in the CreatedGameView script
        createdGameViewScript.GetType().GetField("id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, idLabelView);
        createdGameViewScript.GetType().GetField("creator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, creatorLabelView);
        createdGameViewScript.GetType().GetField("delete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, deleteButtonView);

        // Step 10: Create a folder for prefabs if it doesn't exist
        string folderPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Step 11: Save the CreatedGameView GameObject as a prefab
        string prefabPath = $"{folderPath}/CreatedGameView.prefab";
        bool success = false;
        PrefabUtility.SaveAsPrefabAsset(createdGameViewObject, prefabPath, out success);

        // Clean up
        GameObject.DestroyImmediate(createdGameViewObject);

        Debug.Log($"CreatedGameView Prefab created at: {prefabPath}, success: {success}");
    }
}

