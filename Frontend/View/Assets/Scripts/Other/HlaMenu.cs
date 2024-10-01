using UnityEditor;
using UnityEngine;

namespace Other
{
    public class HlaMenu: MonoBehaviour
    {
        [MenuItem("HLA/Create CreatedGameView Prefab")]
        public static void CreatePrefab()
        {
            new PrefabCreator().CreatePrefab(new PrefabCreator.Args
            {
                prefabName = "EmptyView",
                viewTypeName = "SomeNamespace.TestEmptyView"
            });
        }
    }
}