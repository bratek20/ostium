// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;

namespace Translations.Api {
    public class TranslationKey {
        public string Value { get; }

        public TranslationKey(
            string value
        ) {
            Value = value;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Value == ((TranslationKey)obj).Value;
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
}