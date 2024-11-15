using static MathematicalExpressions.operations.types.functions.power.PowerFunctions;

namespace MathematicalExpressions.operations.vectors
{
    public class ExpressionsVector : Vector<Expression>
    {
        public ExpressionsVector(int size) : base(size) { }
        public ExpressionsVector(IEnumerable<Expression> components) : base(components) { }
        public ExpressionsVector(Vector<Expression> vectorToCopy) : base(vectorToCopy) { }
        public ExpressionsVector(int size, IEnumerable<Expression> components) : base(size, components) { }

        public override Expression Norm
        {
            get
            {
                Expression squaredNorm = Components[0] * Components[0];

                for (int i = 1; i < Size; ++i)
                    squaredNorm += Components[i] * Components[i];

                return Sqrt(squaredNorm);
            }
        }

        public override void Add(Vector<Expression> vector)
        {
            ValidateForNull(vector);
            int size = Size;

            if (size < vector.Size)
            {
                var components = new Expression[vector.Size];
                Components.CopyTo(components, 0);
                Components = components;

                for (int i = size; i < Size; ++i)
                    Components[i] = vector.Components[i];
            } 
            else
                size = vector.Size;

            for (int i = 0; i < size; ++i)
                Components[i] += vector.Components[i];
        }

        public override void Subtract(Vector<Expression> vector)
        {
            ValidateForNull(vector);
            int size = Size;

            if (size < vector.Size)
            {
                var components = new Expression[vector.Size];
                Components.CopyTo(components, 0);
                Components = components;

                for (int i = size; i < Size; ++i)
                    Components[i] = -vector.Components[i];
            }
            else
                size = vector.Size;

            for (int i = 0; i < size; ++i)
                Components[i] -= vector.Components[i];
        }

        public override void MultiplyByScalar(Expression scalar)
        {
            for (int i = 0; i < Size; ++i)
                Components[i] *= scalar;
        }

        public override void Reverse() => MultiplyByScalar(-1);

        public static ExpressionsVector GetSum(ExpressionsVector vector1, ExpressionsVector vector2)
        {
            ValidateForNull(vector1);
            ValidateForNull(vector2);

            ExpressionsVector resultVector = new(vector1);
            resultVector.Add(vector2);
            return resultVector;
        }

        public static ExpressionsVector GetDifference(ExpressionsVector vector1, ExpressionsVector vector2)
        {
            ValidateForNull(vector1);
            ValidateForNull(vector2);

            ExpressionsVector resultVector = new(vector1);
            resultVector.Subtract(vector2);
            return resultVector;
        }

        public static ExpressionsVector GetMultiplicationByScalar(ExpressionsVector vector, Expression scalar)
        {
            ValidateForNull(vector);
            ValidateForNull(scalar);

            ExpressionsVector resultVector = new(vector);
            resultVector.MultiplyByScalar(scalar);
            return resultVector;
        }

        public static Expression GetDotProduct(ExpressionsVector vector1, ExpressionsVector vector2)
        {
            ValidateForNull(vector1);
            ValidateForNull(vector2);

            if (vector1.Size != vector2.Size)
                throw new ArgumentException("sizes");

            Expression dotProduct = vector1.Components[0] * vector2.Components[0];
            var size = vector1.Size;

            for (int i = 1; i < size; ++i)
                dotProduct += vector1.Components[i] * vector2.Components[i];

            return dotProduct;
        }

        public static ExpressionsVector operator +(ExpressionsVector vector) => new(vector);
        public static ExpressionsVector operator -(ExpressionsVector vector)
        {
            ExpressionsVector resultVector = new(vector);
            resultVector.Reverse();
            return resultVector;
        }

        public static implicit operator ExpressionsVector(Expression expression) => new([expression]);
        public static explicit operator Expression(ExpressionsVector vector)
        {
            if (vector.Size != 1)
                throw new NotSupportedException();
            return vector.Components[0];
        }

        public static ExpressionsVector operator +(ExpressionsVector vector1, ExpressionsVector vector2) => GetSum(vector1, vector2);
        public static ExpressionsVector operator -(ExpressionsVector vector1, ExpressionsVector vector2) => GetDifference(vector1, vector2);
        public static ExpressionsVector operator *(ExpressionsVector vector1, ExpressionsVector vector2)
        {
            if (Math.Min(vector1.Size, vector2.Size) == 1)
                return GetMultiplicationByScalar(vector1.Size >= vector2.Size
                    ? vector1
                    : vector2,
                    vector1.Size >= vector2.Size
                    ? (Expression)vector2
                    : (Expression)vector1);

            return GetDotProduct(vector1, vector2);
        }
    }
}