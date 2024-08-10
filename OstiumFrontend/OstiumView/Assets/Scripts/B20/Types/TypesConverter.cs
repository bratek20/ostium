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

        //convert to world screen space
        public static Rect Convert(RectTransform rectTransform)
        {
            var leftBottom = rectTransform.TransformPoint(rectTransform.rect.min);
            var rightTop = rectTransform.TransformPoint(rectTransform.rect.max);
            var size = rightTop - leftBottom;
            return new Rect(Convert(leftBottom), Convert(size));
        }
    }
}