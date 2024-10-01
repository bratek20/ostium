using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GamesManagement.View;
using B20.Frontend.Elements.View;

public class PrefabCreator : MonoBehaviour
{
    [MenuItem("Tools/Create CreatedGameView Prefab")]
    public static void CreatePrefab()
    {
        // Step 1: Create a new GameObject for the CreatedGameView as a UI element
        GameObject createdGameViewObject = new GameObject("CreatedGameView", typeof(RectTransform));
        CreatedGameView createdGameViewScript = createdGameViewObject.AddComponent<CreatedGameView>();

        // Set the parent object as a UI element with RectTransform
        RectTransform rectTransform = createdGameViewObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 200);  // Set size of the UI element, adjust as necessary
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Center pivot and anchor
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        Image backgroundImage = createdGameViewObject.AddComponent<Image>();
        backgroundImage.color = Color.grey;
        
        // Step 2: Load Label and Button prefabs
        GameObject labelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/B20/Frontend/Elements/Prefabs/Label.prefab");
        GameObject buttonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/B20/Frontend/Elements/Prefabs/Button.prefab");

        if (labelPrefab == null || buttonPrefab == null)
        {
            Debug.LogError("Failed to load Label or Button prefabs.");
            return;
        }

        // Step 3: Create and assign the LabelView for 'id'
        GameObject idLabelObject = (GameObject)PrefabUtility.InstantiatePrefab(labelPrefab, createdGameViewObject.transform);
        idLabelObject.name = "IdLabel";
        RectTransform idRect = idLabelObject.GetComponent<RectTransform>();
        idRect.anchoredPosition = new Vector2(0, 50); // Position within UI, adjust as necessary
        LabelView idLabelView = idLabelObject.GetComponent<LabelView>();

        // Step 4: Create and assign the LabelView for 'creator'
        GameObject creatorLabelObject = (GameObject)PrefabUtility.InstantiatePrefab(labelPrefab, createdGameViewObject.transform);
        creatorLabelObject.name = "CreatorLabel";
        RectTransform creatorRect = creatorLabelObject.GetComponent<RectTransform>();
        creatorRect.anchoredPosition = new Vector2(0, 0); // Position within UI, adjust as necessary
        LabelView creatorLabelView = creatorLabelObject.GetComponent<LabelView>();

        // Step 5: Create and assign the ButtonView for 'delete'
        GameObject deleteButtonObject = (GameObject)PrefabUtility.InstantiatePrefab(buttonPrefab, createdGameViewObject.transform);
        deleteButtonObject.name = "DeleteButton";
        RectTransform deleteRect = deleteButtonObject.GetComponent<RectTransform>();
        deleteRect.anchoredPosition = new Vector2(0, -50); // Position within UI, adjust as necessary
        ButtonView deleteButtonView = deleteButtonObject.GetComponent<ButtonView>();

        // Step 6: Assign references in the CreatedGameView script
        createdGameViewScript.GetType().GetField("id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, idLabelView);
        createdGameViewScript.GetType().GetField("creator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, creatorLabelView);
        createdGameViewScript.GetType().GetField("delete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(createdGameViewScript, deleteButtonView);

        // Step 7: Create a folder for prefabs if it doesn't exist
        string folderPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Step 8: Save the CreatedGameView GameObject as a prefab
        string prefabPath = $"{folderPath}/CreatedGameView.prefab";
        bool success = false;
        PrefabUtility.SaveAsPrefabAsset(createdGameViewObject, prefabPath, out success);

        // Clean up
        DestroyImmediate(createdGameViewObject);

        Debug.Log($"CreatedGameView Prefab created at: {prefabPath}, success: {success}");
    }
}
