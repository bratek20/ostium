using System;
using B20.Ext;
using Xunit;

namespace B20.Tests.Ext
{
    public class OptionalTest
    {
        [Fact]
        public void TestOf()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();
            var optional3 = Optional<int>.Empty();

            Assert.True(optional1.IsPresent());
            Assert.False(optional2.IsPresent());
            Assert.False(optional3.IsPresent());
        }

        [Fact]
        public void TestIsPresent()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.True(optional1.IsPresent());
            Assert.False(optional2.IsPresent());
        }

        [Fact]
        public void TestGet()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.Equal(42, optional1.Get());
            Assert.Throws<InvalidOperationException>(() => optional2.Get());
        }

        [Fact]
        public void TestOrElse()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.Equal(42, optional1.OrElse(0));
            Assert.Equal(0, optional2.OrElse(0));
        }

        [Fact]
        public void TestOrElseFunc()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.Equal(42, optional1.OrElseFunc(() => 0));
            Assert.Equal(0, optional2.OrElseFunc(() => 0));
        }

        [Fact]
        public void TestOrElseThrow()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.Equal(42, optional1.OrElseThrow(new InvalidOperationException("Error")));
            Assert.Throws<InvalidOperationException>(() => optional2.OrElseThrow(new InvalidOperationException("Error")));
        }

        [Fact]
        public void TestMap()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            var mapped1 = optional1.Map(x => x * 2);
            var mapped2 = optional2.Map(x => x * 2);

            Assert.Equal(84, mapped1.Get());
            Assert.False(mapped2.IsPresent());
        }

        [Fact]
        public void TestFlatMap()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            var mapped1 = optional1.FlatMap(x => Optional<int>.Of(x * 2));
            var mapped2 = optional2.FlatMap(x => Optional<int>.Of(x * 2));

            Assert.Equal(84, mapped1.Get());
            Assert.False(mapped2.IsPresent());
        }

        [Fact]
        public void TestFilter()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            var filtered1 = optional1.Filter(x => x > 40);
            var filtered2 = optional1.Filter(x => x < 40);
            var filtered3 = optional2.Filter(x => x > 40);

            Assert.Equal(42, filtered1.Get());
            Assert.False(filtered2.IsPresent());
            Assert.False(filtered3.IsPresent());
        }

        [Fact]
        public void TestEquals()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Of(42);
            var optional3 = Optional<int>.Empty();

            Assert.True(optional1.Equals(optional1));
            Assert.True(optional1.Equals(optional2));
            Assert.False(optional1.Equals(optional3));
        }

        [Fact]
        public void TestHasValue()
        {
            var optional1 = Optional<int>.Of(42);
            var optional2 = Optional<int>.Empty();

            Assert.True(optional1.HasValue(42));
            Assert.False(optional1.HasValue(0));
            Assert.False(optional2.HasValue(42));
            Assert.False(optional2.HasValue(0));
        }
    }
}