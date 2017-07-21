using Xunit;

namespace BasicIocContainer.Tests
{
    public class BasicContainerTests
    {

        private readonly BasicContainer _classUnderTest;

        public BasicContainerTests()
        {
            _classUnderTest = new BasicContainer();
        }

        [Fact]
        public void when_register_type_return_nothing()
        {
            _classUnderTest.Register<ICalculator,ScientificCalculator>();
        }

        [Fact]
        public void when_resolving_unregistered_type()
        {
            Assert.Throws<TypeNotRegisteredException>(() => _classUnderTest.ResolveType<ICalculator>());
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
            Assert.Equal(resolvedType.Add(1,2),3);
        }
    }

    interface ICalculator
    {
        int Add(int i, int j);
        int Substract(int i, int j);

    }

    class SimpleCalculator : ICalculator
    {
        private readonly Addition _addition;
        private readonly Substraction _substraction;


        public SimpleCalculator(Addition addition, Substraction substraction)
        {
            this._addition = addition;
            this._substraction = substraction;
        }

        public int Add(int i, int j)
        {
            return _addition.Add(i, j);
        }

        public int Substract(int i, int j)
        {
            return _substraction.Substract(i, j);
        }
    }

    class ScientificCalculator : ICalculator
    {
        private readonly Addition _addition;
        private readonly Substraction _substraction;

        public ScientificCalculator(Addition addition, Substraction substraction)
        {
            this._addition = addition;
            this._substraction = substraction;
        }
        public int Add(int i, int j)
        {
            return _addition.Add(i, j);
        }

        public int Substract(int i, int j)
        {
            return _substraction.Substract(i, j);
        }
    }

    class Addition
    {
       /* private int a;
        private int b;
        Addition(int a, int b)
        {
            this.a = a;
            this.b = b;
        }*/
        public int Add(int a, int b)
        {
            return a+b;
        }
    }

    class Substraction
    {

        public int Substract(int i, int j)
        {
            return i - j;
        }
    }



}
