package ru.nstu.numerical_methods.course_project.slae.matrix;

public class Slae {
    private final Matrix systemCoefficients;
    private final Vector constantTerms;

    public Slae(Matrix systemCoefficients, Vector constantTerms) {
        this.systemCoefficients = systemCoefficients;
        this.constantTerms = constantTerms;
    }

    public Matrix getSystemCoefficients() {
        return systemCoefficients;
    }

    public Vector getConstantTerms() {
        return constantTerms;
    }

    public static Vector solveByConjugateGradientMethod(Matrix A, Vector b) {
        int size = b.getSize();

        Vector result = new Vector(size);
        Vector residual = new Vector(b);
        Vector direction = new Vector(residual);

        double residualSquaredNorm = Vector.getDotProduct(residual, residual);

        for (int k = 0; k < size; ++k) {
            Vector Ap = A.multiplyByVector(direction);
            double alpha = residualSquaredNorm / Vector.getDotProduct(direction, Ap);

            for (int i = 0; i < size; i++) {
                result.components[i] += alpha * direction.components[i];
            }

            for (int i = 0; i < size; i++) {
                residual.components[i] -= alpha * Ap.components[i];
            }

            double newResidualSquaredNorm = Vector.getDotProduct(residual, residual);

            if (Double.valueOf(Math.sqrt(newResidualSquaredNorm) / b.getLength()).equals(0.)) {
                break;
            }

            double beta = newResidualSquaredNorm / residualSquaredNorm;

            for (int i = 0; i < size; i++) {
                direction.components[i] = residual.components[i] + beta * direction.components[i];
            }

            residualSquaredNorm = newResidualSquaredNorm;
        }

        return result;
    }

    public Vector solve() {
        return solveByLocallyOptimalScheme();
    }

    public Vector solveByConjugateGradientMethod() {
        return solveByConjugateGradientMethod(systemCoefficients, constantTerms);
    }

    public Vector solveByLocallyOptimalScheme() {
        return solveByLocallyOptimalScheme(systemCoefficients, constantTerms);
    }

    public static Vector solveByLocallyOptimalScheme(Matrix A, Vector b) {
        int size = b.getSize();

        Vector result = new Vector(size);
        Vector residual = new Vector(b);
        Vector direction = new Vector(residual);

        do {
            Vector auxiliaryVector = A.multiplyByVector(direction);
            double auxiliaryVectorSquaredNorm = Vector.getDotProduct(auxiliaryVector, auxiliaryVector);

            double alpha = Vector.getDotProduct(auxiliaryVector, residual)
                    / auxiliaryVectorSquaredNorm;

            for (int i = 0; i < size; i++) {
                result.components[i] += alpha * direction.components[i];
            }

            for (int i = 0; i < size; i++) {
                residual.components[i] -= alpha * auxiliaryVector.components[i];
            }

            Vector nextResult = A.multiplyByVector(residual);
            double beta = -Vector.getDotProduct(auxiliaryVector, nextResult)
                    / auxiliaryVectorSquaredNorm;

            for (int i = 0; i < size; i++) {
                direction.components[i] = residual.components[i] + beta * direction.components[i];
            }

            for (int i = 0; i < size; i++) {
                auxiliaryVector.components[i] = nextResult.components[i]
                        + beta * auxiliaryVector.components[i];
            }

        } while (!Double.valueOf(Vector.getDotProduct(residual, residual)).equals(0.));

        return result;
    }

    @Override
    public String toString() {
        String separator = System.lineSeparator();
        return "SLAE"
                + System.lineSeparator() + "Matrix: " + separator + systemCoefficients.assemble()
                + separator + "Vector: " + separator + constantTerms;
    }
}