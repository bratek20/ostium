using System.Collections.Generic;
using B20.Architecture.Serialization.Context;
using B20.Architecture.Serialization.Impl;
using B20.Tests.ExtraAsserts;
using Serialization.Api;
using Xunit;

namespace B20.Tests.Architecture.Serialization
{
    public class SerializationImplTest
    {
        public class SomeValue
        {
            public string Value { get; }

            public SomeValue(string value)
            {
                Value = value;
            }
        }

        public class TestObject
        {
            private string value;
            private int number;
            private string nullable;

            public TestObject(string value, int number, string nullable)
            {
                this.value = value;
                this.number = number;
                this.nullable = nullable;
            }

            public SomeValue GetValue() => new SomeValue(value);
            public int GetNumber() => number;
            public string GetNullable() => nullable;

            public static TestObject Create(SomeValue value, int number, string nullable)
            {
                return new TestObject(value.Value, number, nullable);
            }
        }

        private Serializer serializer;

        public SerializationImplTest()
        {
            serializer = SerializationFactory.CreateSerializer();
        }

        [Fact]
        public void Should_Serialize_As_JSON()
        {
            var testObject = new TestObject("test", 1, null);

            var serializedValue = serializer.Serialize(testObject);

            AssertExt.Equals(serializedValue.GetValue(), "{\"value\":\"test\",\"number\":1,\"nullable\":null}");
            AssertExt.Equals(serializedValue.GetType(), SerializationType.JSON);
        }

        // [Fact]
        // public void Should_Deserialize_Object_From_JSON()
        // {
        //     var serializedValue = new SerializedValue
        //     {
        //         Value = "{\"Value\":\"test\",\"Number\":1}",
        //         Type = SerializationType.JSON
        //     };
        //
        //     var deserializedObject = serializer.Deserialize<TestObject>(serializedValue);
        //
        //     deserializedObject.Should().BeEquivalentTo(new TestObject("test", 1, null));
        // }
        //
        // [Fact]
        // public void Should_Deserialize_List_From_JSON()
        // {
        //     var serializedValue = new SerializedValue
        //     {
        //         Value = "[{\"Value\":\"test\",\"Number\":1}]",
        //         Type = SerializationType.JSON
        //     };
        //
        //     var deserializedObject = serializer.DeserializeList<TestObject>(serializedValue);
        //
        //     deserializedObject.Should().BeEquivalentTo(new List<TestObject> { new TestObject("test", 1, null) });
        // }
    }
}