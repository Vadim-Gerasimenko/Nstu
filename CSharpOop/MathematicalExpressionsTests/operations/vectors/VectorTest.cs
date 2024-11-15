using MathematicalExpressions.operations.vectors;
using MathematicalExpressionsTests.auxiliary;

namespace MathematicalExpressionsTests.operations.vectors
{
    [TestClass]
    public class VectorTest
    {
        private static readonly int[] testVectorComponents = { -1, 2 };
        private static readonly IntegersVectorHeir testVector = new(testVectorComponents);

        [TestMethod]
        public void Components_WithCorrectValues_ReturnArrayOfCorrectValues()
        {
            if (testVector.Size != testVectorComponents.Length) Assert.Fail();

            foreach (var component in testVectorComponents)
                if (!testVector.Components.Contains(component)) Assert.Fail();
        }

        [TestMethod]
        public void Size_WithCorrectSize_ReturnSameSizeAsInt()
            => Assert.AreEqual(testVectorComponents.Length, testVector.Size);
        [TestMethod]
        public void BaseConstructorValidateSize_PassNegativeSize_ThrowArgumentException()
        { try { Vector<int> vector = new IntegersVectorHeir(-1); } catch (ArgumentException) { return; } Assert.Fail(); }
        [TestMethod]
        public void BaseConstructorValidateSize_PassZeroSize_ThrowArgumentException()
        { try { Vector<int> vector = new IntegersVectorHeir(0); } catch (ArgumentException) { return; } Assert.Fail(); }
        [TestMethod]
        public void BaseConstructorValidateSize_PassCorrectSize_ReturnCorrectly()
        { try { Vector<int> vector = new IntegersVectorHeir(3); } catch (ArgumentException) { Assert.Fail(); } }
        [TestMethod]
        public void BaseConstructorValidateForNull_PassNull_ArgumentNullException()
        { try { Vector<int> vector = new IntegersVectorHeir(1, null); } catch (ArgumentNullException) { return; } Assert.Fail(); }
        [TestMethod]
        public void BaseConstructorValidateForNull_PassNotNull_ReturnCorrectly()
        { try { Vector<int> vector = new IntegersVectorHeir(new int[] { 1, 2 }); } catch (ArgumentNullException) { Assert.Fail(); } }
        [TestMethod]
        public void BaseConstructorValidateForEmptiness_PassEmptyIEnumerable_ArgumentException()
        { try { Vector<int> vector = new IntegersVectorHeir(Enumerable.Empty<int>()); } catch (ArgumentException) { return; } Assert.Fail(); }
        [TestMethod]
        public void BaseConstructorValidateForEmptiness_PassNotEmptyIEnumerable_ReturnCorrectly()
        { try { Vector<int> vector = new IntegersVectorHeir(new int[3].AsEnumerable()); } catch (ArgumentException) { Assert.Fail(); } }
        [TestMethod]
        public void Equals_PassIdenticalVectors_ReturnTrue()
            => Assert.IsTrue(testVector.Equals(new IntegersVectorHeir(testVector)));
        [TestMethod]
        public void Equals_PassOneVectorTwice_ReturnTrue()
            => Assert.IsTrue(testVector.Equals(testVector));
        [TestMethod]
        public void Equals_WithCorrectVectorAndNull_ReturnFalse()
            => Assert.IsFalse(testVector.Equals(null));
        [TestMethod]
        public void Equals_PassDifferentVectorsWithOneSize_ReturnFalse()
            => Assert.IsFalse(testVector.Equals(new IntegersVectorHeir(new int[] { 4, 5 })));
        [TestMethod]
        public void Equals_PassDifferentVectorsWithDifferentSizes_ReturnFalse()
            => Assert.IsFalse(testVector.Equals(new IntegersVectorHeir(new int[] { 1, 2, 4, 5 })));
        [TestMethod]
        public void ToString_WithCorrectVector_ReturnInCurlyBracketsVectorComponentsSeparatedBySemicolonAsString()
            => Assert.AreEqual($"{{{string.Join("; ", testVectorComponents)}}}", testVector.ToString());
    }
}