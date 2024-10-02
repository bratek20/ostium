using UnityEditor;
using UnityEngine;

namespace Other
{
    public class HlaMenu: MonoBehaviour
    {
        [MenuItem("HLA/Start Module Prefabs")]
        public static void StartModulePrefabs()
        {
            new PrefabCreator().CreatePrefab(new PrefabCreator.Args
            {
                prefabName = "EmptyView",
                viewTypeName = "SomeNamespace.TestEmptyView"
            });
        }
    }
}