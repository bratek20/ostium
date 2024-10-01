using B20.Frontend.Elements.View;
using B20.Frontend.UiElements;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GamesManagement.View;
using SingleGame.View;
using SomeNamespace;

public class PrefabCreatorTest
{
    private const string PrefabPath = "Assets/Prefabs/CreatedGameView.prefab";

    private PrefabCreator creator;
    private GameObject prefab;

    [SetUp]
    public void Setup()
    {
        creator = new PrefabCreator();
        // Make sure to run the prefab creation before each test if needed
        creator.CreatePrefab();
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
    }

    [Test]
    public void ShouldCreateBasedOnArgs()
    {
        creator.CreatePrefab2(new PrefabCreator.Args
        {
            prefabName = "TestEmptyView",
            viewTypeName = "SomeNamespace.TestEmptyView"
        });
        
        var prefab = AssertPrefabCreatedAndGet("TestEmptyView");
        AssertPrefabHasComponent<TestEmptyView>(prefab);
        
        DeletePrefab("TestEmptyView");
    }
    
    private GameObject AssertPrefabCreatedAndGet(string prefabName)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/{prefabName}.prefab");
        Assert.IsNotNull(prefab, $"{prefabName} prefab should be created and exist at the specified path.");
        return prefab;
    }
    
    private void AssertPrefabHasComponent<T>(GameObject prefab) where T : Component
    {
        T component = prefab.GetComponent<T>();
        Assert.IsNotNull(component, $"Prefab should have the {typeof(T)} component.");
    }
    
    [TearDown]
    public void Cleanup()
    {
        // Clean up the generated prefab after each test
        if (AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath) != null)
        {
            AssetDatabase.DeleteAsset(PrefabPath);
        }
    }
    
    private void DeletePrefab(string prefabName)
    {
        string prefabPath = $"Assets/Prefabs/{prefabName}.prefab";
        if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
        {
            AssetDatabase.DeleteAsset(prefabPath);
        }
    }

    [Test]
    public void PrefabIsCreated()
    {
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");
    }

    [Test]
    public void PrefabHasCorrectHierarchy()
    {
        Transform createdGameViewTransform = prefab.transform;
        Assert.AreEqual(3, createdGameViewTransform.childCount, "Prefab should have exactly 3 child objects (IdLabel, CreatorLabel, DeleteButton).");
        
        AssertChildExists("id");
        AssertChildExists("creator");
        AssertChildExists("delete");
    }
    
    private void AssertChildExists(string childName)
    {
        Assert.IsNotNull(prefab.transform.Find(childName), $"{childName} should exist as a child.");
    }

    [Test]
    public void PrefabHasCorrectComponents()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");

        // Verify CreatedGameView has necessary components
        CreatedGameView createdGameView = prefab.GetComponent<CreatedGameView>();
        Assert.IsNotNull(createdGameView, "Prefab should have the CreatedGameView component.");

        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        Assert.IsNotNull(rectTransform, "Prefab should have a RectTransform component.");
        
        Image backgroundImage = prefab.GetComponent<Image>();
        Assert.IsNotNull(backgroundImage, "Prefab should have an Image component.");
        Assert.AreEqual(Color.grey, backgroundImage.color, "The background image should have a grey color.");
    }

    private const float EXPECTED_LABEL_WIDTH = 300;
    private const float EXPECTED_LABEL_HEIGHT = 100;
    
    private const float EXPECTED_BUTTON_WIDTH = 200;
    private const float EXPECTED_BUTTON_HEIGHT = 80;
    
    private const float EXPECTED_PADDING = 25;
    private const float EXPECTED_SPACING = 25;
    
    [Test]
    public void PrefabHasCorrectSizeBasedOnChildren()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");

        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        Assert.IsNotNull(rectTransform, "Prefab should have a RectTransform component.");

        AssertChildSize("id", EXPECTED_LABEL_WIDTH, EXPECTED_LABEL_HEIGHT);
        AssertChildSize("creator", EXPECTED_LABEL_WIDTH, EXPECTED_LABEL_HEIGHT);
        AssertChildSize("delete", EXPECTED_BUTTON_WIDTH, EXPECTED_BUTTON_HEIGHT);
        
        // Expected size based on child sizes
        float expectedWidth = Mathf.Max(EXPECTED_LABEL_WIDTH, EXPECTED_BUTTON_WIDTH) + 2 * EXPECTED_PADDING;
        float expectedHeight = 2 * EXPECTED_LABEL_HEIGHT + EXPECTED_BUTTON_HEIGHT + 2 * EXPECTED_PADDING + 2 * EXPECTED_SPACING;

        Assert.AreEqual(new Vector2(expectedWidth, expectedHeight), rectTransform.sizeDelta, "The container size should match the calculated size based on children and padding.");
    }
    
    private void AssertChildSize(string childName, float expectedWidth, float expectedHeight)
    {
        RectTransform childTransform = prefab.transform.Find(childName).GetComponent<RectTransform>();
        Assert.IsNotNull(childTransform, $"{childName} should have a RectTransform component.");
        Assert.AreEqual(new Vector2(expectedWidth, expectedHeight), childTransform.sizeDelta, $"{childName} should have the expected size.");
    }

    [Test]
    public void ChildrenArePositionedCorrectly()
    {
        AssertChildPostion("id", 115);
        AssertChildPostion("creator", -10);
        AssertChildPostion("delete", -125);
    }
    
    private void AssertChildPostion(string childName, float yPosition)
    {
        RectTransform childTransform = prefab.transform.Find(childName).GetComponent<RectTransform>();
        Assert.IsNotNull(childTransform, $"{childName} should have a RectTransform component.");
        Assert.AreEqual(new Vector2(0, yPosition), childTransform.anchoredPosition, $"{childName} should be positioned correctly.");
    }
}
