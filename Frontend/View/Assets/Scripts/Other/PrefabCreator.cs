using Other;
using UnityEditor;
using UnityEngine;


public class PrefabCreator : MonoBehaviour
{
    [MenuItem("Tools/Create Prefab From Code")]
    public static void CreatePrefab()
    {
        // Step 1: Create a new GameObject
        GameObject newObject = new GameObject("MyPrefab");

        // Step 2: Add a script to the GameObject
        MyScript script = newObject.AddComponent<MyScript>();

        // Step 3: Set serializable fields of the script
        script.someIntField = 10;

        // Step 4: Create a folder for prefabs if it doesn't exist
        string folderPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Step 5: Save the GameObject as a prefab
        string prefabPath = $"{folderPath}/MyPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(newObject, prefabPath);

        // Clean up
        DestroyImmediate(newObject);

        Debug.Log($"Prefab created at: {prefabPath}");
    }
}

