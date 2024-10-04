using PrefabCreator.Context;
using UnityEditor;
using UnityEngine;

public class HlaMenu : EditorWindow
{
    private const string MODULES_PATH = "Assets/Scripts/Ostium";
    private string moduleName = "";

    [MenuItem("HLA/Start Module Prefabs")]
    public static void ShowWindow()
    {
        // Show an editor window
        GetWindow<HlaMenu>("Start Module Prefabs");
    }

    private void OnGUI()
    {
        // Input field for the module name
        GUILayout.Label("Enter Module Name", EditorStyles.boldLabel);
        moduleName = EditorGUILayout.TextField("Module Name", moduleName);

        // Start module prefabs button
        if (GUILayout.Button("Execute"))
        {
            if (string.IsNullOrEmpty(moduleName))
            {
                EditorUtility.DisplayDialog("Error", "Module name cannot be empty", "OK");
            }
            else
            {
                StartModulePrefabs();
            }
        }
    }

    private void StartModulePrefabs()
    {
        var api = PrefabCreatorFactory.CreateApi();
        api.StartModulePrefabs(MODULES_PATH, moduleName);
        EditorUtility.DisplayDialog("Success", $"Started module prefabs for: {moduleName}", "OK");
    }
}