using B20.Frontend.Elements.View;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoolSwitchView), true)]
public class VisibleViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        base.OnInspectorGUI();

        // Cast the target object to VisibleView
        BoolSwitchView visibleView = (BoolSwitchView)target;

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