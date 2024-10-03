using B20.Frontend.Elements.View;
using UnityEngine;

namespace SomeNamespace
{
    public class TestReferencingView1: ElementView<EmptyViewModel>
    {
        [SerializeField]
        TestReferencingView2 prefab2;
        
        public TestReferencingView2 Prefab2 => prefab2;
    }
}