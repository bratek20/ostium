using B20.Frontend.Traits;
using UnityEngine;

namespace B20.View
{
    public class UnityPosition: Position
    {
        public Vector3 Value { get; }
        
        public UnityPosition(Vector3 value)
        {
            Value = value;
        }
    }
}