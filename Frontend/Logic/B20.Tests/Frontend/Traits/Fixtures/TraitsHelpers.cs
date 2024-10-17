using System.Collections.Generic;
using B20.Frontend.UiElements;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;

namespace B20.Tests.Frontend.Traits.Fixtures
{
    public class TraitsHelpers
    {
        public static void Click(UiElement element)
        {
            element.GetTrait<Clickable>().Click();
        }
        
        public static void StartDrag(UiElement element, Position2d p)
        {
            element.GetTrait<Draggable>().StartDrag(p);
        }
        
        public static void EndDrag(UiElement element, Position2d p)
        {
            element.GetTrait<Draggable>().EndDrag(p);
        }
        
        public static void DragToRect(UiElement element, UiElement target)
        {
            var currentPos = element.GetTrait<WithPosition2d>().Model;
            var draggable = element.GetTrait<Draggable>();
            var targetRect = target.GetTrait<WithRect>().Rect;
            
            draggable.StartDrag(currentPos);
            draggable.EndDrag(targetRect.Center);
        }

        public static void PlaceElements(Window window)
        {
            List<UiElement> allElements = new List<UiElement>();
            foreach (var element in window.Elements)
            {
                allElements.Add(element);
                allElements.AddRange(element.Descendants);
            }

            int x = 1;
            foreach (var element in allElements)
            {
                element.Traits.ForEach(trait =>
                {
                    if (trait is WithPosition2d withPosition2d)
                    {
                        withPosition2d.Init(new Position2d(x, 0), p => { });
                        x++;
                    }
                    else if (trait is WithRect withRect)
                    {
                        var rect = new Rect(x, 1, 1, 1);
                        withRect.SetRectProvider(() => rect);
                        x+=2;
                    }
                });
            }
        }
    }
}