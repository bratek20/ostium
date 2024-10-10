using System;
using System.Collections;
using System.Collections.Generic;
using B20.Frontend.Elements.View;
using B20.Frontend.UiElements;
using JetBrains.Annotations;
using UnityEngine;

namespace MyNamespace
{
    
    public class MyGroupView : UiElementGroupView<MyView, MyViewModel, MyModel>
    {
        public MyView ElementPrefab
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

