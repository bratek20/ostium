using System;
using System.Collections;
using System.Collections.Generic;
using B20.Frontend.Elements.View;
using B20.Frontend.UiElements;
using JetBrains.Annotations;
using UnityEngine;

namespace SomeNamespace
{
    public class EmptyViewModelGroup : UiElementGroup<EmptyViewModel, EmptyModel>
    {
        public EmptyViewModelGroup(Func<EmptyViewModel> elementFactory) : base(elementFactory)
        {
        }
    }

    public class TestElementGroupView : UiElementGroupView<TestLabelButtonView, EmptyViewModel, EmptyModel>
    {
        public TestLabelButtonView ElementPrefab
        {
            get
            {
                return elementPrefab;
            }
            
            set
            {
                elementPrefab = value;
            }
        }
        
        public void SetSpacing(float spacing)
        {
            elementSpacing = spacing;
        }
        
        public void SetDirection(Direction direction)
        {
            this.direction = direction;
        }
    }
}

