using System;

namespace B20.Ext
{
    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _isPresent;

        private Optional(T value, bool isPresent)
        {
            _value = value;
            _isPresent = isPresent;
        }

        public static Optional<T> Of(T value)
        {
            if (value == null)
            {
                return Empty();
            }
            return new Optional<T>(value, true);
        }

        public static Optional<T> Empty()
        {
            return new Optional<T>(default(T), false);
        }

        public bool IsPresent()
        {
            return _isPresent;
        }

        public bool IsEmpty()
        {
            return !_isPresent;
        }

        public T Get()
        {
            if (!IsPresent())
            {
                throw new InvalidOperationException("Value is not present");
            }
            return _value;
        }

        public T OrElse(T defaultValue)
        {
            return IsPresent() ? _value : defaultValue;
        }

        public T OrElseFunc(Func<T> defaultValueFunc)
        {
            return IsPresent() ? _value : defaultValueFunc();
        }

        public T OrElseThrow(Exception exception)
        {
            if (IsPresent())
            {
                return _value;
            }
            throw exception;
        }

        public T OrElseThrowMessage(string message)
        {
            return OrElseThrow(new InvalidOperationException(message));
        }

        public Optional<T> Let(Action<T> action)
        {
            if (IsPresent())
            {
                action(_value);
            }
            return this;
        }

        public Optional<U> Map<U>(Func<T, U> mapper)
        {
            if (!IsPresent())
            {
                return Optional<U>.Empty();
            }
            return Optional<U>.Of(mapper(_value));
        }

        public Optional<U> FlatMap<U>(Func<T, Optional<U>> mapper)
        {
            if (!IsPresent())
            {
                return Optional<U>.Empty();
            }
            return mapper(_value);
        }

        public Optional<T> Filter(Func<T, bool> predicate)
        {
            if (!IsPresent())
            {
                return this;
            }
            return predicate(_value) ? this : Empty();
        }

        public bool Equals(Optional<T> other)
        {
            if (!IsPresent() && !other.IsPresent())
            {
                return true;
            }
            if (!IsPresent() || !other.IsPresent())
            {
                return false;
            }
            return _value.Equals(other._value);
        }

        public bool HasValue(T value)
        {
            if (!IsPresent())
            {
                return false;
            }
            return _value.Equals(value);
        }
    }
}