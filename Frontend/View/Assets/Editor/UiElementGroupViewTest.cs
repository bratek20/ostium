using System.Collections.Generic;
using B20.Frontend.Elements.View;
using MyNamespace;
using NUnit.Framework;
using UnityEngine;

public class UiElementGroupViewTest
{
    [Test]
    public void ShouldWork()
    {
        var myPrefab = PrefabCreatorTest.AssertPrefabCreatedAndGet("Assets/Scripts/B20/Tests/MyModule/Prefabs/MyPrefab.prefab");

        var g = new GameObject("test", typeof(RectTransform), typeof(MyGroupView));
        var view = g.GetComponent<MyGroupView>();
        view.ElementPrefab = PrefabCreatorTest.AssertHasComponentAndGet<MyView>(myPrefab); //350 x 250
        view.SetDirection(Direction.Horizontal);
        view.SetSpacing(10);
        
        var viewModel = new MyViewModelGroup(() => new MyViewModel());
        view.Bind(viewModel);
        
        viewModel.Update(new List<MyModel>()
        {
            new MyModel(),
            new MyModel(),
        });
        
        // padding 25
        float expectedWidth = 25 + 350 * 2 + 10 + 25;
        float expectedHeight = 25 + 250 + 25;
        PrefabCreatorTest.AssertSize(g, expectedWidth, expectedHeight);  
    }
}
