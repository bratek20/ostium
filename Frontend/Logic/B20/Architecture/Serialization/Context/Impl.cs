using B20.Architecture.Serialization.Impl;
using Serialization.Api;

namespace B20.Architecture.Serialization.Context
{
    public class SerializationFactory
    {
        public static Serializer CreateSerializer()
        {
            return new SerializerLogic();
        }
    }
}