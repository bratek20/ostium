using System.Collections.Generic;
using B20.Frontend.Elements.View;
using NUnit.Framework;
using PrefabCreator;
using PrefabCreator.Impl;
using SomeNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabCreatorTest
{
    protected PrefabCreatorApiLogic creator;

    [SetUp]
    public void Setup()
    {
        creator = new PrefabCreatorApiLogic();
    }

    [TestFixture]
    public class EmptyViewScope : PrefabCreatorTest
    {
        private GameObject prefab;
        
        [SetUp]
        public void Create()
        {
            creator.CreatePrefab(new PrefabCreatorApiLogic.Args
            {
                prefabDirPath = "Assets/Prefab",
                prefabName = "TestEmptyView",
                viewTypeName = "SomeNamespace.TestEmptyView"
            });
        
            prefab = AssertPrefabCreatedAndGet("Assets/Prefab/TestEmptyView.prefab");
        }

        [TearDown]
        public void Delete()
        {
            DeletePrefab("Assets/Prefab/TestEmptyView.prefab");
        }

        [Test]
        public void ShouldAddViewComponentAdjustSizeAndHasGreyBackground()
        {
            AssertHasComponentAndGet<TestEmptyView>(prefab);
        
            AssertSize(prefab, 50, 50);
            
            Image backgroundImage = prefab.GetComponent<Image>();
            Assert.IsNotNull(backgroundImage, "Prefab should have an Image component.");
            Assert.AreEqual(Color.grey, backgroundImage.color, "The background image should have a grey color.");
        }
    }
    
    [TestFixture]
    public class LabelButtonViewScope: PrefabCreatorTest
    {
        private GameObject prefab;
        
        [SetUp]
        public void Create()
        {
            creator.CreatePrefab(new PrefabCreatorApiLogic.Args
            {
                prefabDirPath = "Assets/Prefab",
                prefabName = "TestLabelButtonView",
                viewTypeName = "SomeNamespace.TestLabelButtonView",
                fields = new List<PrefabCreatorApiLogic.Field>
                {
                    new() { path = "Assets/Scripts/B20/Frontend/Elements/Prefabs/Label.prefab", name = "myLabel", type = "B20.Frontend.Elements.View.LabelView" },
                    new() { path = "Assets/Scripts/B20/Frontend/Elements/Prefabs/Button.prefab", name = "myButton", type = "B20.Frontend.Elements.View.ButtonView" }
                }
            });
        
            prefab = AssertPrefabCreatedAndGet("Assets/Prefab/TestLabelButtonView.prefab");
        }
        
        [TearDown]
        public void Delete()
        {
            DeletePrefab("Assets/Prefab/TestLabelButtonView.prefab");
        }
        
        [Test]
        public void ShouldAddChildrenAdjustItsOwnSizeAndPutChildrenAtCorrectPosition()
        {
            AssertChildSize(prefab, "myLabel", 300, 100);
            AssertChildSize(prefab, "myButton", 200, 80);

            float expectedWidth = 300 + 2 * 25; //350, widest child + 2 * padding
            float expectedHeight = 100 + 80 + 25 + 2 * 25; //255, sum of heights + spacing + 2 * padding
            AssertSize(prefab, expectedWidth, expectedHeight);

            // container uses central anchoring
            // it 255 height, so half of it is 127.5
            // there is 25 padding and label height is 100 but anchored at center so half is 50
            // so label should be at 127.5 - 25 - 50 = 52.5
            AssertChildPosition(prefab, "myLabel", 52.5f);
        
            // now we add remaining size of label and spacing + 50 + 25
            // button has 80 height, so half is 40
            // so button should be at 52.5 - 25 - 50 - 40 = -62.5   
            AssertChildPosition(prefab, "myButton", -62.5f);
        }
        
        [Test]
        public void ShouldSetReferencesToChildren()
        {
            var view = AssertHasComponentAndGet<TestLabelButtonView>(prefab);
            
            var myLabelGo = AssertHasChildAndGet(prefab, "myLabel");
            var myButtonGo = AssertHasChildAndGet(prefab, "myButton");

            var myLabelView = AssertHasComponentAndGet<LabelView>(myLabelGo);
            var myButtonView = AssertHasComponentAndGet<ButtonView>(myButtonGo);
            
            Assert.AreEqual(myLabelView, view.MyLabel);
            Assert.AreEqual(myButtonView, view.MyButton);
        }
    }
    
    private void AssertSize(GameObject go, float width, float height)
    {
        var rt = go.GetComponent<RectTransform>();
        Assert.AreEqual(new Vector2(width, height), rt.sizeDelta);
    }
    
    private GameObject AssertPrefabCreatedAndGet(string prefabPath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}");
        Assert.IsNotNull(prefab, $"No prefab found at path {prefabPath}");
        return prefab;
    }
    
    private T AssertHasComponentAndGet<T>(GameObject prefab) where T : Component
    {
        T component = prefab.GetComponent<T>();
        Assert.IsNotNull(component, $"Prefab should have the {typeof(T)} component.");
        return component;
    }

    private void DeletePrefab(string prefabPath)
    {
        if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
        {
            AssetDatabase.DeleteAsset(prefabPath);
        }
    }

    private GameObject AssertHasChildAndGet(GameObject go, string childName)
    {
        var child = go.transform.Find(childName);
        Assert.IsNotNull(child, $"{childName} should exist as a child.");
        return child.gameObject;
    }

    private void AssertChildSize(GameObject go, string childName, float expectedWidth, float expectedHeight)
    {
        RectTransform childTransform = go.transform.Find(childName).GetComponent<RectTransform>();
        Assert.IsNotNull(childTransform, $"{childName} should have a RectTransform component.");
        Assert.AreEqual(new Vector2(expectedWidth, expectedHeight), childTransform.sizeDelta, $"{childName} should have the expected size.");
    }

    private void AssertChildPosition(GameObject go, string childName, float yPosition)
    {
        RectTransform childTransform = go.transform.Find(childName).GetComponent<RectTransform>();
        Assert.IsNotNull(childTransform, $"{childName} should have a RectTransform component.");
        Assert.AreEqual(new Vector2(0, yPosition), childTransform.anchoredPosition, $"{childName} should be positioned correctly.");
    }
}
