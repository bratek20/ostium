using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Impl;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Architecture.Context.Tests
{
    public class ImplTest
    {
        public static ContextBuilder CreateBuilder()
        {
            return new ContextBuilderLogic();
        }

        class SimpleClass
        {
            
        }
        
        class ReferencingClass
        {
            public SimpleClass SimpleClass { get; }
            
            public ReferencingClass(SimpleClass simpleClass)
            {
                SimpleClass = simpleClass;    
            }
        }

        interface SomeInterface
        {
            
        }

        class SomeInterfaceImpl : SomeInterface
        {
            
        }
        
        [Fact]
        public void TestReferencingClass()
        {
            // given
            var c = CreateBuilder()
                .SetClass<SimpleClass>()
                .SetClass<ReferencingClass>()
                .Build();
            
            // when
            var obj = c.Get<ReferencingClass>();
            
            // then
            Assert.IsType<ReferencingClass>(obj);
            Assert.IsType<SimpleClass>(obj.SimpleClass);
        }

        [Fact]
        public void TestInterfaceImpl()
        {
            // given
            var c = CreateBuilder()
                .SetImpl<SomeInterface, SomeInterfaceImpl>()
                .Build();
            
            // when
            var obj = c.Get<SomeInterface>();
            
            // then
            Assert.IsType<SomeInterfaceImpl>(obj);
        }
        
        class SomeContextModule : ContextModule
        {
            public void Apply(ContextBuilder builder)
            {
                builder.SetImpl<SomeInterface, SomeInterfaceImpl>();
            }
        }
        
        [Fact]
        public void ShouldSupportContextModule()
        {
            // given
            var c = CreateBuilder()
                .WithModules(new SomeContextModule())
                .Build();
            
            // when
            var obj = c.Get<SomeInterface>();
            
            // then
            Assert.IsType<SomeInterfaceImpl>(obj);
        }
        
        class ClassWithValue
        {
            public int Value { get; }
            
            public ClassWithValue(int value)
            {
                Value = value;
            }
        }
        
        [Fact]
        public void ShouldSupportImplObject()
        {
            // given
            var c = CreateBuilder()
                .SetImplObject(new ClassWithValue(42))
                .Build();
            
            // when
            var obj = c.Get<ClassWithValue>();
            
            // then
            AssertExt.Equal(obj.Value, 42);
        }

        public class DefaultScopeIsSingletonTests
        {
            private B20.Architecture.Contexts.Api.Context c;

            // Common setup done in the constructor
            public DefaultScopeIsSingletonTests()
            {
                c = CreateBuilder()
                    .SetClass<SimpleClass>()
                    .SetImpl<SomeInterface, SomeInterfaceImpl>()
                    .Build();
            }
            
            [Fact]
            public void SetClass() => TestBeingSingleton<SimpleClass>();
            [Fact]
            public void SetImpl() => TestBeingSingleton<SomeInterface>();

            private void TestBeingSingleton<T>() where T: class
            {
                // when
                var obj1 = c.Get<T>();
                var obj2 = c.Get<T>();
            
                // then
                Assert.True(obj1 == obj2);
            }
        }
        
        public class ForcingPrototypeScopeTests
        {
            private B20.Architecture.Contexts.Api.Context c;

            // Common setup done in the constructor
            public ForcingPrototypeScopeTests()
            {
                c = CreateBuilder()
                    .SetClass<SimpleClass>(InjectionMode.Prototype)
                    .Build();
            }
            
            [Fact]
            public void SetClass() => TestBeingPrototype<SimpleClass>();

            private void TestBeingPrototype<T>() where T: class
            {
                // when
                var obj1 = c.Get<T>();
                var obj2 = c.Get<T>();
            
                // then
                Assert.True(obj1 != obj2);
            }
        }

        public class FieldInjectionTests
        {
            class ClassWithField
            {
                public SimpleClass SimpleClass { get; set;  }
                public ClassWithValue ClassWithValue { get; } = new ClassWithValue(5);
            }
            
            private ClassWithField obj;

            // Common setup done in the constructor
            public FieldInjectionTests()
            {
                var c = CreateBuilder()
                    .SetClass<SimpleClass>()
                    .SetClass<ClassWithField>()
                    .Build();
                
                obj = c.Get<ClassWithField>();
            }

            [Fact]
            public void ShouldInjectSimpleClass()
            {
                Assert.IsType<SimpleClass>(obj.SimpleClass);
            }

            [Fact]
            public void ShouldNotRequireAlreadySetField()
            {
                AssertExt.Equal(obj.ClassWithValue.Value, 5);
            }
        }
    }
}