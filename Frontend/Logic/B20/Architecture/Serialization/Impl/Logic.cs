using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serialization.Api;
using JsonException = System.Text.Json.JsonException;

namespace B20.Architecture.Serialization.Impl
{
    public class SerializerLogic : Serializer
    {
        private readonly JsonSerializerSettings _jsonSettings;

        public SerializerLogic()
        {
            // Configure Newtonsoft.Json to include all fields and ignore getters
            _jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic | 
                                                System.Reflection.BindingFlags.Public | 
                                                System.Reflection.BindingFlags.Instance,
                    IgnoreSerializableAttribute = true,
                    NamingStrategy = new CamelCaseNamingStrategy(),
                    // Include all fields
                    SerializeCompilerGeneratedMembers = true
                },
                MissingMemberHandling = MissingMemberHandling.Ignore,
                // Ensure fields are visible
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        public SerializedValue Serialize(object value)
        {
            var jsonString = JsonConvert.SerializeObject(value, _jsonSettings);
            return SerializedValue.Create(
                value: jsonString,
                type: SerializationType.JSON
            );
        }

        public T Deserialize<T>(SerializedValue serializedValue)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(serializedValue.GetValue(), _jsonSettings);
            }
            catch (JsonException e)
            {
                throw HandleJsonException(e);
            }
        }

        public List<T> DeserializeList<T>(SerializedValue serializedValue)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(serializedValue.GetValue(), _jsonSettings);
            }
            catch (JsonException e)
            {
                throw HandleJsonException(e);
            }
        }

        private DeserializationException HandleJsonException(JsonException e)
        {
            var internalMsg = e.Message ?? string.Empty;
            var missingFieldName = ExtractMissingFieldName(internalMsg);
            if (missingFieldName != null)
            {
                return new DeserializationException($"Deserialization failed: missing value for field `{missingFieldName}`");
            }
            return new DeserializationException($"Deserialization failed: unmapped message: {internalMsg}");
        }

        private string ExtractMissingFieldName(string message)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"property '([a-zA-Z0-9]+)' due to missing");
            var match = regex.Match(message);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}