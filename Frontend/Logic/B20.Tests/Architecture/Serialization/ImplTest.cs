using System;
using System.Collections.Generic;
using B20.Architecture.Exceptions.Fixtures;
using B20.Architecture.Serialization.Context;
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
            protected bool Equals(TestObject other)
            {
                return value == other.value && number == other.number && nullable == other.nullable;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestObject)obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(value, number, nullable);
            }
            
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

            AssertExt.Equal(serializedValue.GetValue(), "{\"value\":\"test\",\"number\":1,\"nullable\":null}");
            AssertExt.Equal(serializedValue.GetType(), SerializationType.JSON);
        }

        [Fact]
        public void Should_Deserialize_Object_From_JSON()
        {
            var serializedValue = SerializedValue.Create("{\"value\":\"test\",\"number\":1,\"nullable\":null}",SerializationType.JSON);

            var deserializedObject = serializer.Deserialize<TestObject>(serializedValue);

            AssertExt.Equal(deserializedObject, new TestObject("test", 1, null));
        }

        [Fact]
        public void Should_Deserialize_List_From_JSON()
        {
            var serializedValue = SerializedValue.Create(
                "[{\"value\":\"test\",\"number\":1,\"nullable\":null}]",
                SerializationType.JSON
            );

            var deserializedObject = serializer.DeserializeList<TestObject>(serializedValue);

            AssertExt.Equal(deserializedObject, new List<TestObject> { new TestObject("test", 1, null) });
        }
        
        [Fact]
        public void BUG_Should_Throw_Exception_When_Deserializing_From_Invalid_JSON()
        {
            var serializedValue = SerializedValue.Create("{}", SerializationType.JSON);
            
            // ExceptionsAsserts.ThrowsApiException
            // (
            //     () => serializer.Deserialize<TestObject>(serializedValue),
            //     e =>
            //     {
            //         e.Type = typeof(DeserializationException);
            //         e.Message = "Deserialization failed: missing value for field `value`";
            //     }
            // );
            
            ExceptionsAsserts.DoesNotThrowAnyException(
                () => serializer.Deserialize<TestObject>(serializedValue)
            );
        }
    }
}
