using MathematicalExpressions.operations.vectors;

namespace MathematicalExpressionsTests.auxiliary
{
    internal class IntegersVectorHeir : Vector<int>
    {
        public IntegersVectorHeir(int size) : base(size) { }
        public IntegersVectorHeir(IEnumerable<int> components) : base(components) { }
        public IntegersVectorHeir(Vector<int> vectorToCopy) : base(vectorToCopy) { }
        public IntegersVectorHeir(int size, IEnumerable<int> components) : base(size, components) { }

        public override int Norm => throw new NotImplementedException();
        public override void Add(Vector<int> vector) => throw new NotImplementedException();
        public override void MultiplyByScalar(int scalar) => throw new NotImplementedException();
        public override void Reverse() => throw new NotImplementedException();
        public override void Subtract(Vector<int> vector) => throw new NotImplementedException();
    }
}