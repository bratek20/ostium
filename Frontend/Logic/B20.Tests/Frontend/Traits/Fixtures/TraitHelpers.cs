using System;
using B20.Architecture.Contexts.Context;
using B20.Frontend.Element;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using Microsoft.AspNetCore.Mvc;

namespace B20.Tests.Frontend.Traits.Fixtures
{
    public class TraitHelpers
    {
        public static void Click(ElementVm element)
        {
            element.GetTrait<Clickable>().Click();
        }
        
        public static void StartDrag(ElementVm element, Position2d p)
        {
            element.GetTrait<Draggable>().StartDrag(p);
        }
        
        public static void EndDrag(ElementVm element, Position2d p)
        {
            element.GetTrait<Draggable>().EndDrag(p);
        }
    }
}