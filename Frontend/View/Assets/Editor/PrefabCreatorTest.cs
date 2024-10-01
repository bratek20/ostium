using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GamesManagement.View;

public class PrefabCreatorTest
{
    private const string PrefabPath = "Assets/Prefabs/CreatedGameView.prefab";

    [SetUp]
    public void Setup()
    {
        // Make sure to run the prefab creation before each test if needed
        PrefabCreator.CreatePrefab();
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

    [Test]
    public void PrefabIsCreated()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");
    }

    [Test]
    public void PrefabHasCorrectHierarchy()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");

        Transform createdGameViewTransform = prefab.transform;
        Assert.AreEqual(3, createdGameViewTransform.childCount, "Prefab should have exactly 3 child objects (IdLabel, CreatorLabel, DeleteButton).");

        Assert.IsNotNull(createdGameViewTransform.Find("IdLabel"), "IdLabel should exist as a child.");
        Assert.IsNotNull(createdGameViewTransform.Find("CreatorLabel"), "CreatorLabel should exist as a child.");
        Assert.IsNotNull(createdGameViewTransform.Find("DeleteButton"), "DeleteButton should exist as a child.");
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

    [Test]
    public void PrefabHasCorrectSizeBasedOnChildren()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");

        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
        Assert.IsNotNull(rectTransform, "Prefab should have a RectTransform component.");

        // Expected size based on child sizes
        float expectedWidth = Mathf.Max(400f, 400f) + 50f; // Maximum width of the children + padding (left + right)
        float expectedHeight = 50f + 50f + 50f + (2 * 25f) + 50f; // Sum of child heights + spacing + padding (top + bottom)

        Assert.AreEqual(new Vector2(expectedWidth, expectedHeight), rectTransform.sizeDelta, "The container size should match the calculated size based on children and padding.");
    }

    [Test]
    public void ChildrenArePositionedCorrectly()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        Assert.IsNotNull(prefab, "Prefab should be created and exist at the specified path.");

        RectTransform idLabelTransform = prefab.transform.Find("IdLabel").GetComponent<RectTransform>();
        RectTransform creatorLabelTransform = prefab.transform.Find("CreatorLabel").GetComponent<RectTransform>();
        RectTransform deleteButtonTransform = prefab.transform.Find("DeleteButton").GetComponent<RectTransform>();

        Assert.IsNotNull(idLabelTransform, "IdLabel should have a RectTransform component.");
        Assert.IsNotNull(creatorLabelTransform, "CreatorLabel should have a RectTransform component.");
        Assert.IsNotNull(deleteButtonTransform, "DeleteButton should have a RectTransform component.");

        // Check positions: children should be spaced by 25 units, and their positions should respect the padding
        float padding = 25f;
        float spacing = 25f;

        Assert.AreEqual(new Vector2(0, -padding), idLabelTransform.anchoredPosition, "IdLabel should be positioned at the top with padding.");
        Assert.AreEqual(new Vector2(0, -(50f + padding + spacing)), creatorLabelTransform.anchoredPosition, "CreatorLabel should be positioned below IdLabel with spacing.");
        Assert.AreEqual(new Vector2(0, -(50f + 50f + padding + (2 * spacing))), deleteButtonTransform.anchoredPosition, "DeleteButton should be positioned below CreatorLabel with spacing.");
    }
}
