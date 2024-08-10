using B20.Frontend.Postion;
using UnityEngine;
using Rect = B20.Frontend.Postion.Rect;

namespace B20.Types
{
    public class TypesConverter
    {
        public static Position2D Convert(Vector3 v)
        {
            return new Position2D((int) v.x, (int) v.y);
        }
        
        public static Rect Convert(RectTransform rectTransform)
        {
            // Extract the position and size from RectTransform
            Vector2 position = rectTransform.anchoredPosition;
            Vector2 size = rectTransform.sizeDelta;

            var topLeft = new Position2D((int) position.x, (int) position.y);
            var bottomRight = new Position2D((int) (position.x + size.x), (int) (position.y + size.y));
            return new Rect(topLeft, bottomRight);
        }
    }
}