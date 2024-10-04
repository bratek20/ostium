using System.Collections.Generic;
using B20.Frontend.Elements.View;
using NUnit.Framework;
using PrefabCreator;
using PrefabCreator.Api;
using PrefabCreator.Impl;
using SomeNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabCreatorTest
{
    private const string TEST_MODULES_PATH = "Assets/Scripts/HLA/Tests";
    private PrefabCreatorApi creator;

    private GameObject emptyPrefab;
    private GameObject labelButtonPrefab;
    private GameObject referencingPrefab1;
    private GameObject referencingPrefab2;
    private GameObject windowPrefab;
    
    [SetUp]
    public void Setup()
    {
        creator = new PrefabCreatorApiLogic();
        creator.StartModulePrefabs(TEST_MODULES_PATH, "SomeModule");
        
        emptyPrefab = AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestEmptyPrefab.prefab");
        labelButtonPrefab = AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestLabelButtonPrefab.prefab");
        referencingPrefab1 = AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestReferencingPrefab1.prefab");
        referencingPrefab2 = AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestReferencingPrefab2.prefab");
        windowPrefab = AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestWindowPrefab.prefab");
    }

    [Test]
    public void ShouldCreatePrefabsFromBlueprints()
    {
        
    }
    
    [Test]
    public void ShouldDeleteAllPrefabs()
    {
        creator.DeleteModulePrefabs(TEST_MODULES_PATH, "SomeModule");
        
        AssertPrefabDeleted($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestEmptyPrefab.prefab");
        AssertPrefabDeleted($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestLabelButtonPrefab.prefab");
        AssertPrefabDeleted($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestReferencingPrefab1.prefab");
        AssertPrefabDeleted($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestReferencingPrefab2.prefab");
    }
        
    [TearDown]
    public void Clean()
    {
        creator.DeleteModulePrefabs(TEST_MODULES_PATH, "SomeModule");
    }
    
    [Test]
    public void UiElement_ShouldHaveViewComponentAdjustSizeAndHasGreyBackground()
    {
        AssertHasComponentAndGet<TestEmptyView>(emptyPrefab);
    
        AssertSize(emptyPrefab, 50, 50);
        
        AssertColor(emptyPrefab, Color.gray);
    }
    
    [Test]
    public void Window_ShouldHaveViewComponentHaveConstantSizeAndHasLightBlueBackground()
    {
        AssertHasComponentAndGet<TestWindowView>(windowPrefab);
    
        AssertSize(windowPrefab, 1080, 1920);
        
        AssertColor(windowPrefab, new Color(87, 94,96));
    }
    
    private void AssertColor(GameObject go, Color expectedColor)
    {
        Image image = go.GetComponent<Image>();
        Assert.IsNotNull(image, "Prefab should have an Image component.");
        Assert.AreEqual(expectedColor, image.color, "The background image should have the expected color.");
    }
    
    [Test]
    public void ShouldAddChildrenAdjustItsOwnSizeAndPutChildrenAtCorrectPosition()
    {
        AssertChildSize(labelButtonPrefab, "myLabel", 300, 100);
        AssertChildSize(labelButtonPrefab, "myButton", 200, 80);
    
        float expectedWidth = 300 + 2 * 25; //350, widest child + 2 * padding
        float expectedHeight = 100 + 80 + 25 + 2 * 25; //255, sum of heights + spacing + 2 * padding
        AssertSize(labelButtonPrefab, expectedWidth, expectedHeight);
    
        // container uses central anchoring
        // it 255 height, so half of it is 127.5
        // there is 25 padding and label height is 100 but anchored at center so half is 50
        // so label should be at 127.5 - 25 - 50 = 52.5
        AssertChildPosition(labelButtonPrefab, "myLabel", 52.5f);
    
        // now we add remaining size of label and spacing + 50 + 25
        // button has 80 height, so half is 40
        // so button should be at 52.5 - 25 - 50 - 40 = -62.5   
        AssertChildPosition(labelButtonPrefab, "myButton", -62.5f);
    }
    
    [Test]
    public void ShouldSetReferencesToChildren()
    {
        var view = AssertHasComponentAndGet<TestLabelButtonView>(labelButtonPrefab);
        
        var myLabelGo = AssertHasChildAndGet(labelButtonPrefab, "myLabel");
        var myButtonGo = AssertHasChildAndGet(labelButtonPrefab, "myButton");
    
        var myLabelView = AssertHasComponentAndGet<LabelView>(myLabelGo);
        var myButtonView = AssertHasComponentAndGet<ButtonView>(myButtonGo);
        
        Assert.AreEqual(myLabelView, view.MyLabel);
        Assert.AreEqual(myButtonView, view.MyButton);
    }
    
    [Test]
    public void ShouldSetReferencesToOtherCreatedPrefabs()
    {
        var view1 = AssertHasComponentAndGet<TestReferencingView1>(referencingPrefab1);
        
        var prefab2 = AssertHasChildAndGet(referencingPrefab1, "prefab2");
        var prefab2View = AssertHasComponentAndGet<TestReferencingView2>(prefab2);
        
        Assert.AreEqual(prefab2View, view1.Prefab2);
        
        var view2 = AssertHasComponentAndGet<TestReferencingView2>(referencingPrefab2);
        
        var myLabelButton = AssertHasChildAndGet(referencingPrefab2, "myLabelButton");
        var myLabelButtonView = AssertHasComponentAndGet<TestLabelButtonView>(myLabelButton);
        
        Assert.AreEqual(myLabelButtonView, view2.MyLabelButton);
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
    
    private void AssertPrefabDeleted(string prefabPath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{prefabPath}");
        Assert.IsNull(prefab, $"Prefab should be deleted at path {prefabPath}");
    }
    
    private T AssertHasComponentAndGet<T>(GameObject prefab) where T : Component
    {
        T component = prefab.GetComponent<T>();
        Assert.IsNotNull(component, $"Prefab should have the {typeof(T)} component.");
        return component;
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
