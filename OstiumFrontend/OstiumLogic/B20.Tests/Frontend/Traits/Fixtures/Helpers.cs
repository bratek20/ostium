using B20.Frontend.Element;
using B20.Frontend.Postion;
using B20.Frontend.Traits;

namespace B20.Tests.Frontend.Traits.Fixtures
{
    public class Helpers
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