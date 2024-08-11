using B20.View;
using UnityEditor;

[CustomEditor(typeof(ElementView<>), true)]
public class ElementViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("ViewModel Properties - TODO", EditorStyles.boldLabel);
    }
}