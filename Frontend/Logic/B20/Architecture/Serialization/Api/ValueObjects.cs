// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;

namespace Serialization.Api {
    public class SerializedValue {
        readonly string value;
        readonly string type;

        public SerializedValue(
            string value,
            string type
        ) {
            this.value = value;
            this.type = type;
        }
        public string GetValue() {
            return value;
        }
        public SerializationType GetType() {
            return (SerializationType)Enum.Parse(typeof(SerializationType), type);
        }
        public static SerializedValue Create(string value, SerializationType type) {
            return new SerializedValue(value, type.ToString());
        }
    }
}