using B20.View;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VisibleView), true)]
public class VisibleViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        base.OnInspectorGUI();

        // Cast the target object to VisibleView
        VisibleView visibleView = (VisibleView)target;

        // Display the ViewModel properties
        EditorGUILayout.LabelField("ViewModel Properties", EditorStyles.boldLabel);

        if (visibleView.ViewModel != null)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Toggle("Model Value", visibleView.ViewModel.Model);
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Toggle Visibility"))
            {
                visibleView.ViewModel.Update(!visibleView.ViewModel.Model);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("ViewModel is not bound.", MessageType.Warning);
        }
    }
}