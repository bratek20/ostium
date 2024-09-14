using B20.Frontend.UiElements;
using B20.Frontend.Postion;
using B20.Frontend.Traits;

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
    }
}