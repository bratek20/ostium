using System.Collections.Generic;
using B20.Frontend.Elements.View;
using NUnit.Framework;
using PrefabCreator.Impl;
using SomeNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiElementGroupViewTest
{
    private const string TEST_MODULES_PATH = "Assets/Scripts/HLA/Tests";
    
    [Test]
    public void ShouldWork()
    {
        var labelButtonPrefab = PrefabCreatorTest.AssertPrefabCreatedAndGet($"{TEST_MODULES_PATH}/SomeModule/Prefabs/TestLabelButtonPrefab.prefab");

        var g = new GameObject("test", typeof(RectTransform), typeof(TestElementGroupView));
        var view = g.GetComponent<TestElementGroupView>();
        view.ElementPrefab = PrefabCreatorTest.AssertHasComponentAndGet<TestLabelButtonView>(labelButtonPrefab); //350 x 255
        view.SetDirection(Direction.Horizontal);
        view.SetSpacing(10);
        
        var viewModel = new EmptyViewModelGroup(() => new EmptyViewModel());
        view.Bind(viewModel);
        
        viewModel.Update(new List<EmptyModel>()
        {
            new EmptyModel(),
            new EmptyModel(),
        });
        
        // padding 25
        float expectedWidth = 25 + 350 * 2 + 10 + 25;
        float expectedHeight = 25 + 255 + 25;
        PrefabCreatorTest.AssertSize(g, expectedWidth, expectedHeight);  
    }
}
