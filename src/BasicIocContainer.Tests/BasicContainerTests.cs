using Xunit;

namespace BasicIocContainer.Tests
{
    public class BasicContainerTests
    {
        public BasicContainerTests()
        {
            _classUnderTest = new BasicContainer();
        }

        private readonly BasicContainer _classUnderTest;

        [Fact]
        public void when_registered_with_valid_generic_arguments_succeeds()
        {
            _classUnderTest.Register<ICalculator, ScientificCalculator>();
        }

        [Fact]
        public void when_register_with_valid_generic_arguments_and_life_cycle_method_succeeds()
        {
            _classUnderTest.Register<ICalculator, ScientificCalculator>(LifeCycle.Singleton);
        }


        [Fact]

        public void when_resolving_noparameter_constructor_type()
        {
            var resolvedType = _classUnderTest.ResolveType<Addition>().GetType().FullName;
            Assert.Equal(resolvedType, typeof(Addition).FullName);
        }

        [Fact]
        public void when_resolving_parameter_constructor_type()
        {
            _classUnderTest.Register<ICalculator, ScientificCalculator>();
            var resolvedType = _classUnderTest.ResolveType<ICalculator>();
            Assert.Equal(resolvedType.Add(1, 2), 3);
        }

        [Fact]
        public void when_resolving_test_if_new_instance_is_returned_when_transient()
        {
            _classUnderTest.Register<ICalculator, ScientificCalculator>();
            var resolvedType = _classUnderTest.ResolveType<ICalculator>();
            var resolvedType1 = _classUnderTest.ResolveType<ICalculator>();
            Assert.NotSame(resolvedType1, resolvedType);
        }

        [Fact]
        public void when_resolving_a_singleton_new_instance_is_not_created()
        {
            _classUnderTest.Register<ICalculator, ScientificCalculator>(LifeCycle.Singleton);
            var resolvedType = _classUnderTest.ResolveType<ICalculator>();
            var resolvedType1 = _classUnderTest.ResolveType<ICalculator>();
            Assert.Same(resolvedType1, resolvedType);
        }

        [Fact]
        public void when_resolving_test_if_singleton()
        {
            _classUnderTest.Register<ICalculator, SimpleCalculator>();
            _classUnderTest.Register<Addition, Addition>(LifeCycle.Singleton);
            var resolvedType = _classUnderTest.ResolveType<ICalculator>();
            var resolvedType1 = _classUnderTest.ResolveType<ICalculator>();
        }

        [Fact]
        public void when_resolving_unregistered_type()
        {
            Assert.Throws<TypeNotRegisteredException>(() => _classUnderTest.ResolveType<ICalculator>());
        }

        [Fact]
        public void test_scenario1_succeeds()
        {
            _classUnderTest.Register<A,B>();
            var resolvedType = _classUnderTest.ResolveType<A>();
            Assert.Equal(1,resolvedType.print());
        }
        [Fact]
        public void test_scenario2_with_lifecycle_singleton_succeeds()
        {
            _classUnderTest.Register<U, V>();
            _classUnderTest.Register<W,W>(LifeCycle.Singleton);
            _classUnderTest.Register<Y, Y>(LifeCycle.Singleton);
            var resolvedType = _classUnderTest.ResolveType<U>();
            Assert.Equal(4, resolvedType.print());
        }

        [Fact]
        public void test_scenario2_with_lifecycle_transient_succeeds()
        {
            _classUnderTest.Register<U, V>();
            var resolvedType = _classUnderTest.ResolveType<U>();
            Assert.Equal(7, resolvedType.print());
        }
    }
}