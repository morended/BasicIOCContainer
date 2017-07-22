namespace BasicIocContainer.Tests
{
    internal interface ICalculator
    {
        int Add(int i, int j);
        int Substract(int i, int j);
    }

    internal class SimpleCalculator : ICalculator
    {
        private readonly Addition _addition;
        private readonly Substraction _substraction;

        public SimpleCalculator(Addition addition, Substraction substraction)
        {
            _addition = addition;
            _substraction = substraction;
        }

        public Addition Addition { get; set; }

        public int Add(int i, int j)
        {
            return _addition.Add(i, j);
        }

        public int Substract(int i, int j)
        {
            return _substraction.Substract(i, j);
        }
    }

    public class ScientificCalculator : ICalculator
    {
        private static int a;
        private readonly Addition _addition;
        private readonly Substraction _substraction;

        public ScientificCalculator(Addition addition, Substraction substraction)
        {
            a++;
            _addition = addition;
            _substraction = substraction;
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

    public class Addition
    {
        private int a;
        private int b;

        public Addition(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class Substraction
    {
        public int Substract(int i, int j)
        {
            return i - j;
        }
    }
}