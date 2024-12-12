package ru.nstu.numerical_methods.course_project.solution_area;

import ru.nstu.numerical_methods.course_project.slae.DenseMatrix;
import ru.nstu.numerical_methods.course_project.slae.Matrix;
import ru.nstu.numerical_methods.course_project.slae.Vector;

import java.util.function.BiFunction;

class FiniteElement {
    SolutionArea solutionArea;
    int subAreaIndex;

    double lambdaAverage;
    double gammaAverage;
    Vector functionValues = new Vector(NODES_COUNT);

    int xStartIndex;
    int yStartIndex;

    static final int EDGE_NODES_COUNT = 2;
    static final int NODES_COUNT = 4;

    FiniteElement(SolutionArea solutionArea, int xStartIndex, int yStartIndex) {
        this.solutionArea = solutionArea;

        this.xStartIndex = xStartIndex;
        this.yStartIndex = yStartIndex;

        subAreaIndex = getSubareaIndex();

        if (isSignificant()) {
            lambdaAverage = getNodesFunctionAverage(solutionArea.area.equation.lambda.get(subAreaIndex));
            gammaAverage = getNodesFunctionAverage(solutionArea.area.equation.gamma.get(subAreaIndex));
            functionValues = getFunctionValues(solutionArea.area.equation.rightFunctions.get(subAreaIndex));
        }
    }

    public boolean isSignificant() {
        return subAreaIndex >= 0;
    }

    private int getSubareaIndex() {
        double[] gridXValues = solutionArea.grid.xValues;
        double[] gridYValues = solutionArea.grid.yValues;

        double[] areaXValues = solutionArea.area.xValues;
        double[] areaYValues = solutionArea.area.yValues;

        for (int i = 0; i < solutionArea.area.subareas.length; ++i) {
            Subarea subarea = solutionArea.area.subareas[i];

            if (Double.compare(gridXValues[xStartIndex], areaXValues[subarea.xStartIndex]) >= 0
                    && Double.compare(gridXValues[xStartIndex + 1], areaXValues[subarea.xEndIndex]) <= 0
                    && Double.compare(gridYValues[yStartIndex], areaYValues[subarea.yStartIndex]) >= 0
                    && Double.compare(gridYValues[yStartIndex + 1], areaYValues[subarea.yEndIndex]) <= 0) {
                return subarea.index;
            }
        }

        return -1;
    }

    private double getNodesFunctionAverage(BiFunction<Double, Double, Double> function) {
        double average = 0;

        for (int i = 0; i < EDGE_NODES_COUNT; ++i) {
            for (int j = 0; j < EDGE_NODES_COUNT; ++j) {
                average += function.apply(
                        solutionArea.grid.xValues[xStartIndex + j],
                        solutionArea.grid.yValues[yStartIndex + i]
                );
            }
        }

        return average / NODES_COUNT;
    }

    private Vector getFunctionValues(BiFunction<Double, Double, Double> function) {
        double[] functionValues = new double[NODES_COUNT];

        for (int i = 0, index = 0; i < EDGE_NODES_COUNT; ++i) {
            for (int j = 0; j < EDGE_NODES_COUNT; ++j) {
                functionValues[index] = function.apply(
                        solutionArea.grid.xValues[xStartIndex + j],
                        solutionArea.grid.yValues[yStartIndex + i]
                );
                ++index;
            }
        }

        return new Vector(functionValues);
    }

    Matrix getLocalMatrixCoefficients() {
        Matrix matrix = generateStiffnessMatrix();
        matrix.add(generateWeightMatrix(gammaAverage));
        return matrix;
    }

    Vector getLocalConstantTermsVector() {
        final double gamma = 1;
        return generateWeightMatrix(gamma).multiplyByVector(functionValues);
    }

    private Matrix generateStiffnessMatrix() {
        double heightX = solutionArea.grid.xValues[xStartIndex + 1] - solutionArea.grid.xValues[xStartIndex];
        double heightY = solutionArea.grid.yValues[yStartIndex + 1] - solutionArea.grid.yValues[yStartIndex];

        double squaredHeightX = heightX * heightX;
        double squaredHeightY = heightY * heightY;

        final double commonMultiplierConstant = 6;
        double commonMultiplier = lambdaAverage / (commonMultiplierConstant * heightX * heightY);

        final double[][] leftStiffnessMatrixCoefficients = new double[][]{
                new double[]{2, -2, 1, -1},
                new double[]{-2, 2, -1, 1},
                new double[]{1, -1, 2, -2},
                new double[]{-1, 1, -2, 2}
        };

        final double[][] rightStiffnessMatrixCoefficients = new double[][]{
                new double[]{2, 1, -2, -1},
                new double[]{1, 2, -1, -2},
                new double[]{-2, -1, 2, 1},
                new double[]{-1, -2, 1, 2}
        };

        final int stiffnessMatrixDimension = leftStiffnessMatrixCoefficients.length;
        double[][] components = new double[stiffnessMatrixDimension][stiffnessMatrixDimension];

        for (int i = 0; i < stiffnessMatrixDimension; ++i) {
            for (int j = 0; j < stiffnessMatrixDimension; ++j) {
                components[i][j] = (squaredHeightY * leftStiffnessMatrixCoefficients[i][j]
                        + squaredHeightX * rightStiffnessMatrixCoefficients[i][j])
                        * commonMultiplier;
            }
        }

        return new DenseMatrix(components);
    }

    private Matrix generateWeightMatrix(double gamma) {
        final double commonMultiplierConstant = 36;
        double commonMultiplier = gamma
                * (solutionArea.grid.xValues[xStartIndex + 1] - solutionArea.grid.xValues[xStartIndex])
                * (solutionArea.grid.yValues[yStartIndex + 1] - solutionArea.grid.yValues[yStartIndex])
                / commonMultiplierConstant;

        final double[][] weightMatrixCoefficients = new double[][]{
                new double[]{4, 2, 2, 1},
                new double[]{2, 4, 1, 2},
                new double[]{2, 1, 4, 2},
                new double[]{1, 2, 2, 4}
        };

        final int weightMatrixDimension = weightMatrixCoefficients.length;
        double[][] components = new double[weightMatrixDimension][weightMatrixDimension];

        for (int i = 0; i < weightMatrixDimension; ++i) {
            for (int j = 0; j < weightMatrixDimension; ++j) {
                components[i][j] = commonMultiplier * weightMatrixCoefficients[i][j];
            }
        }

        return new DenseMatrix(components);
    }

    @Override
    public String toString() {
        String separator = System.lineSeparator();
        return String.format("sub: %2d", subAreaIndex) + separator +
                "yStart: " + yStartIndex + separator +
                "xStart: " + xStartIndex;
    }
}