using MathematicalExpressions.operations.operands;
using static MathematicalExpressions.operations.types.functions.trigonometric.TrigonomentricFunctions;
using static MathematicalExpressions.operations.types.functions.power.PowerFunctions;
using MathematicalExpressions;
using MathematicalExpressions.operations.vectors;

namespace MathematicalExpressionsTests.operations.vectors
{
    [TestClass]
    public class ExpressionsVectorTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly Expression testVariableX = new Variable(testVariableXName);
        private static readonly string testVariableYName = "y";
        private static readonly Expression testVariableY = new Variable(testVariableYName);

        private static readonly double testConstant10Value = 10;
        private static readonly Expression testConstant10 = new Constant(testConstant10Value);
        private static readonly double testConstant20Value = 20;
        private static readonly Expression testConstant20 = new Constant(testConstant20Value);

        private static readonly Expression[] testVector1Components = new[] { +7, testVariableX + testVariableY, Sin(-testVariableY - 1) / 4 };
        private static readonly Expression[] testVector2Components = new[] { testConstant10, testVariableY };
        private static readonly Expression[] testVector3Components = new[] { testVariableX, testConstant20 };
        private static readonly Expression[] testVector4Components = new[] { testVariableX };
        private static readonly Expression[] testVector5Components = new[] { testVariableX, testVariableY, testConstant20 };
        private static readonly ExpressionsVector testVector1 = new(testVector1Components);
        private static readonly ExpressionsVector testVector2 = new(testVector2Components);
        private static readonly ExpressionsVector testVector3 = new(testVector3Components);
        private static readonly ExpressionsVector testVector4 = new(testVector4Components);
        private static readonly ExpressionsVector testVector5 = new(testVector5Components);

        [TestMethod]
        public void Norm_WithCorrectVector_ReturnRootOfComponentsSquaresSumAsExpression()
           => Assert.AreEqual(Sqrt((Constant)7 * +7
                + (testVariableX + testVariableY) * (testVariableX + testVariableY)
                + Sin(-testVariableY - 1) / 4 * (Sin(-testVariableY - 1) / 4)),
                testVector1.Norm);
        [TestMethod]
        public void Add_PassWithSmallerOrEqualSize_AddAndReturnCorrectly()
        {
            var expectedComponents = new Expression[testVector2.Size];

            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i];
            for (int i = 0; i < testVector3.Size; ++i)
                expectedComponents[i] += testVector3.Components[i];

            var resultVector = new ExpressionsVector(testVector2);
            resultVector.Add(testVector3);

            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void Add_PassWithLargerSize_RecreateWithNewSizeThenAddAndReturnCorrectly()
        {
            var expectedComponents = new Expression[testVector5.Size];

            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i];
            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] += testVector5.Components[i];
            for (int i = testVector2.Size; i < testVector5.Size; ++i)
                expectedComponents[i] = testVector5.Components[i];

            var resultVector = new ExpressionsVector(testVector2);
            resultVector.Add(testVector5);

            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void Add_PassNull_ThrowArgumentNullException()
        { try { testVector1.Add(null); } catch (ArgumentNullException) { return; } Assert.Fail(); }
        [TestMethod]
        public void Subtract_PassWithSmallerOrEqualSize_SubtractAndReturnCorrectly()
        {
            var resultVector = new ExpressionsVector(testVector2);
            resultVector.Subtract(testVector3);

            var expectedComponents = new Expression[testVector2.Size];
            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i];
            for (int i = 0; i < testVector3.Size; ++i)
                expectedComponents[i] -= testVector3.Components[i];
            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void Subtract_PassWithLargerSize_RecreateWithNewSizeThenSubtractAndReturnCorrectly()
        {
            var expectedComponents = new Expression[testVector5.Size];

            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i];
            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] -= testVector5.Components[i];
            for (int i = testVector2.Size; i < testVector5.Size; ++i)
                expectedComponents[i] = -testVector5.Components[i];

            var resultVector = new ExpressionsVector(testVector2);
            resultVector.Subtract(testVector5);

            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void Subtract_PassNull_ThrowArgumentNullException()
        { try { testVector1.Subtract(null); } catch (ArgumentNullException) { return; } Assert.Fail(); }
        [TestMethod]
        public void MultiplyByScalar_PassCorrectData_MultiplyEachComponentAndReturnCorrectly()
        {
            Expression scalar = testVariableX + 1;
            var expectedComponents = new Expression[testVector2.Size];

            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i] * scalar;

            var resultVector = new ExpressionsVector(testVector2);
            resultVector.MultiplyByScalar(scalar);

            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void Reverse_WithCorrectValue_ReverseComponentsAndReturnCorrectly()
        {
            Expression reverseCoefficient = -1;
            var expectedComponents = new Expression[testVector2.Size];

            for (int i = 0; i < testVector2.Size; ++i)
                expectedComponents[i] = testVector2.Components[i] * reverseCoefficient;

            var resultVector = new ExpressionsVector(testVector2);
            resultVector.Reverse();

            Assert.AreEqual(new ExpressionsVector(expectedComponents), resultVector);
        }
        [TestMethod]
        public void GetSum_PassCorrectVectors_ReturnSumVector()
        {
            ExpressionsVector sumVector = new ExpressionsVector(testVector3);
            sumVector.Add(testVector4);
            Assert.AreEqual(sumVector, ExpressionsVector.GetSum(testVector3, testVector4));
        }
        [TestMethod]
        public void GetDifference_WithCorrectVectors_ReturnDifferenceVector()
        {
            ExpressionsVector differenceVector = new ExpressionsVector(testVector3);
            differenceVector.Subtract(testVector4);
            Assert.AreEqual(differenceVector, ExpressionsVector.GetDifference(testVector3, testVector4));
        }
        [TestMethod]
        public void GetMultiplicationByScalar_PassCorrectData_ReturnVectorWithMultipliedComponents()
        {
            Expression scalar = Sqrt(Sin(2 + testVariableX / 4));
            ExpressionsVector resultVector = new ExpressionsVector(testVector1);
            resultVector.MultiplyByScalar(scalar);
            Assert.AreEqual(resultVector, ExpressionsVector.GetMultiplicationByScalar(testVector1, scalar));
        }
        [TestMethod]
        public void GetDotProduct_PassVectorsWithDifferentSizes_ThrowArgumentException()
        { try { ExpressionsVector.GetDotProduct(testVector1, testVector2); } catch (ArgumentException) { return; } Assert.Fail(); }
        [TestMethod]
        public void GetDotProduct_PassVectorsWithSameSizes_ReturnSumOfCorrespondingComponentsProductsAsExpression()
        {
            Expression dotProduct = testVector2.Components[0] * testVector3.Components[0];
            int vectorSize = testVector2.Size;

            for (int i = 1; i < vectorSize; ++i)
                dotProduct += testVector2.Components[i] * testVector3.Components[i];
            Assert.AreEqual(dotProduct, ExpressionsVector.GetDotProduct(testVector2, testVector3));
        }
        [TestMethod]
        public void UnaryPlusOperator_PassCorrectVector_ReturnNewSameVector()
            => Assert.AreEqual(testVector5, +testVector5);
        [TestMethod]
        public void UnaryMinusOperator_PassCorrectVector_ReturnReverseVector()
        {
            var resultVector = new ExpressionsVector(testVector1);
            resultVector.Reverse();
            Assert.AreEqual(resultVector, -testVector1);
        }
        [TestMethod]
        public void BinaryPlusOperator_PassCorrectVectors_ReturnSumVector()
            => Assert.AreEqual(ExpressionsVector.GetSum(testVector3, testVector4), testVector3 + testVector4);
        [TestMethod]
        public void BinaryMinusOperator_PassCorrectVectors_ReturnDifferenceVector()
            => Assert.AreEqual(ExpressionsVector.GetDifference(testVector3, testVector4), testVector3 - testVector4);
        [TestMethod]
        public void MultiplicationOperator_PassNotScalars_ReturnDotProductAsExpression()
            => Assert.AreEqual(ExpressionsVector.GetDotProduct(testVector2, testVector3), testVector2 * testVector3);
        [TestMethod]
        public void MultiplicationOperator_PassScalarAndNotScalar_ReturnVectorMultipliedByScalar()
            => Assert.AreEqual(ExpressionsVector.GetMultiplicationByScalar(testVector3, (Expression)testVector4), testVector4 * testVector3);
        [TestMethod]
        public void MultiplicationOperator_PassNotScalarAndScalar_ReturnVectorMultipliedByScalar()
            => Assert.AreEqual(ExpressionsVector.GetMultiplicationByScalar(testVector3, (Expression)testVector4), testVector3 * testVector4);
        [TestMethod]
        public void MultiplicationOperator_PassDifferentScalars_ReturnVectorFromFirstScalarMultipliedBySecondScalar()
        {
            var newVectorScalar = new ExpressionsVector(new Expression[] { testVariableY });
            Assert.AreEqual(ExpressionsVector.GetMultiplicationByScalar(newVectorScalar, (Expression)testVector4), newVectorScalar * testVector4);
        }
    }
}