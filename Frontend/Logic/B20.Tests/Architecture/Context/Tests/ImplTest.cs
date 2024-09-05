using B20.Architecture.ContextModule.Api;
using B20.Architecture.ContextModule.Impl;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Architecture.Context.Tests
{
    public class ImplTest
    {
        public ContextBuilder CreateBuilder()
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
    }
}