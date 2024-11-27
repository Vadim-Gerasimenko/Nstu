package ru.nstu.numerical_methods.course_project.solution_area;

import ru.nstu.numerical_methods.course_project.slae.matrix.Slae;
import ru.nstu.numerical_methods.course_project.slae.matrix.DenseMatrix;
import ru.nstu.numerical_methods.course_project.slae.matrix.Matrix;
import ru.nstu.numerical_methods.course_project.slae.matrix.Vector;

import java.util.Arrays;
import java.util.List;
import java.util.function.BiFunction;

import static ru.nstu.numerical_methods.course_project.solution_area.FiniteElement.EDGE_NODES_COUNT;
import static ru.nstu.numerical_methods.course_project.solution_area.FiniteElement.NODES_COUNT;

public class SolutionArea {
    Area area;
    Grid grid;

    BoundaryConditions conditions;

    public SolutionArea(Area area, Grid grid, BoundaryConditions conditions) {
        this.area = area;
        this.grid = grid;
        this.conditions = conditions;
    }

    public Slae assembleSlae() {
        int nodesCount = grid.xValues.length * grid.yValues.length;

        double[][] matrixComponents = new double[nodesCount][nodesCount];
        double[] vectorComponents = new double[nodesCount];

        FiniteElement[] elements = generateFiniteElements();

        for (FiniteElement element : elements) {
            Matrix localMatrix = element.getLocalMatrixCoefficients();
            Vector localVector = element.getLocalConstantTermsVector();

            Node[] nodes = new Node[NODES_COUNT];

            for (int i = 0, index = 0; i < EDGE_NODES_COUNT; ++i) {
                for (int j = 0; j < EDGE_NODES_COUNT; ++index, ++j) {
                    int xIndex = element.xStartIndex + j;
                    int yIndex = element.yStartIndex + i;

                    nodes[index] = new Node(xIndex, yIndex, getNodeGlobalIndex(xIndex, yIndex));
                }
            }

            for (int i = 0; i < NODES_COUNT; ++i) {
                vectorComponents[nodes[i].globalIndex] += localVector.getComponent(i);

                for (int j = 0; j < NODES_COUNT; ++j) {
                    matrixComponents[nodes[i].globalIndex][nodes[j].globalIndex] += localMatrix.getComponent(i, j);
                }
            }
        }

        Matrix matrix = new DenseMatrix(matrixComponents);
        Vector vector = new Vector(vectorComponents);

        setConditions(matrix, vector);
        return new Slae(matrix, vector);
    }

    private int getNodeGlobalIndex(int xIndex, int yIndex) {
        return yIndex * grid.xValues.length + xIndex;
    }

    private void setConditions(Matrix matrix, Vector vector) {
        setSecondConditions(vector);
        setThirdConditions(matrix, vector);
        setFirstConditions(matrix, vector);
    }

    private void setFirstConditions(Matrix matrix, Vector vector) {
        List<List<Integer>> edgesType1 = conditions.edges.get(BoundaryConditions.FIRST_CONDITION_NUMBER);

        for (List<Integer> edge : edgesType1) {
            int edgeIndex = edge.getFirst();
            BiFunction<Double, Double, Double> function = conditions.firstKind.get(edgeIndex);

            int xStart = edge.get(BoundaryConditions.X_START_POSITION);
            int xEnd = edge.get(BoundaryConditions.X_START_POSITION + 1);

            int yStart = edge.get(BoundaryConditions.Y_START_POSITION);
            int yEnd = edge.get(BoundaryConditions.Y_START_POSITION + 1);

            if (xEnd == xStart) {
                for (int i = yStart + 1; i <= yEnd; ++i) {
                    setFirstConditionOnElement(xStart, xEnd, i - 1, i, matrix, vector, function);
                }
            } else {
                for (int i = xStart + 1; i <= xEnd; ++i) {
                    setFirstConditionOnElement(i - 1, i, yStart, yEnd, matrix, vector, function);
                }
            }
        }
    }

    private void setSecondConditions(Vector vector) {
        List<List<Integer>> edgesType2 = conditions.edges.get(BoundaryConditions.SECOND_CONDITION_NUMBER);

        for (List<Integer> edge : edgesType2) {
            BiFunction<Double, Double, Double> thetaFunction = conditions.secondKind.get(edge.getFirst());

            int xStart = edge.get(BoundaryConditions.X_START_POSITION);
            int xEnd = edge.get(BoundaryConditions.X_START_POSITION + 1);

            int yStart = edge.get(BoundaryConditions.Y_START_POSITION);
            int yEnd = edge.get(BoundaryConditions.Y_START_POSITION + 1);

            if (xEnd == xStart) {
                for (int i = yStart + 1; i <= yEnd; ++i) {
                    int yElementStart = i - 1;
                    setSecondConditionOnElement(xStart, xEnd, yElementStart, i,
                            grid.yValues[i] - grid.yValues[yElementStart], vector, thetaFunction);
                }
            } else {
                for (int i = xStart + 1; i <= xEnd; ++i) {
                    int xElementStart = i - 1;
                    setSecondConditionOnElement(xElementStart, i, yStart, yEnd,
                            grid.xValues[i] - grid.xValues[xElementStart], vector, thetaFunction);
                }
            }
        }
    }

    private void setThirdConditions(Matrix matrix, Vector vector) {
        List<List<Integer>> edgesType3 = conditions.edges.get(BoundaryConditions.THIRD_CONDITION_NUMBER);

        for (List<Integer> edge : edgesType3) {
            int edgeIndex = edge.getFirst();

            BiFunction<Double, Double, Double> betaFunction = conditions.thirdKind.get(edgeIndex);
            double beta = conditions.beta.get(edgeIndex);

            int xStart = edge.get(BoundaryConditions.X_START_POSITION);
            int xEnd = edge.get(BoundaryConditions.X_START_POSITION + 1);

            int yStart = edge.get(BoundaryConditions.Y_START_POSITION);
            int yEnd = edge.get(BoundaryConditions.Y_START_POSITION + 1);

            if (xEnd == xStart) {
                for (int i = yStart + 1; i <= yEnd; ++i) {
                    int yElementStart = i - 1;
                    double commonMultiplier = beta * (grid.yValues[i] - grid.yValues[yElementStart])
                            / BoundaryConditions.COMMON_LOCAL_MATRIX_DIVISOR;

                    setThirdConditionOnElement(xStart, xEnd, yElementStart, i, commonMultiplier,
                            matrix, vector, betaFunction);
                }
            } else {
                for (int i = xStart + 1; i <= xEnd; ++i) {
                    int xElementStart = i - 1;
                    double commonMultiplier = beta * (grid.xValues[i] - grid.xValues[xElementStart])
                            / BoundaryConditions.COMMON_LOCAL_MATRIX_DIVISOR;

                    setThirdConditionOnElement(xElementStart, i, yStart, yEnd, commonMultiplier,
                            matrix, vector, betaFunction);
                }
            }
        }
    }

    private void setFirstConditionOnElement(int xStart, int xEnd, int yStart, int yEnd,
                                            Matrix matrix, Vector vector,
                                            BiFunction<Double, Double, Double> function) {
        int[] nodesGlobalIndexes = new int[EDGE_NODES_COUNT];

        nodesGlobalIndexes[0] = getNodeGlobalIndex(xStart, yStart);
        nodesGlobalIndexes[1] = getNodeGlobalIndex(xEnd, yEnd);

        matrix.resetRow(nodesGlobalIndexes[0]);
        matrix.resetRow(nodesGlobalIndexes[1]);

        //matrix.resetColumn(nodesGlobalIndexes[0]);
        //matrix.resetColumn(nodesGlobalIndexes[1]);

        matrix.setComponent(nodesGlobalIndexes[0], nodesGlobalIndexes[0], 1);
        matrix.setComponent(nodesGlobalIndexes[1], nodesGlobalIndexes[1], 1);

        vector.setComponent(nodesGlobalIndexes[0], function.apply(grid.xValues[xStart], grid.yValues[yStart]));
        vector.setComponent(nodesGlobalIndexes[1], function.apply(grid.xValues[xEnd], grid.yValues[yEnd]));
    }

    private void setSecondConditionOnElement(int xStart, int xEnd, int yStart, int yEnd,
                                             double elementEdgeLength, Vector vector,
                                             BiFunction<Double, Double, Double> function) {
        double[] theta = new double[EDGE_NODES_COUNT];
        int[] nodesGlobalIndexes = new int[EDGE_NODES_COUNT];

        nodesGlobalIndexes[0] = getNodeGlobalIndex(xStart, yStart);
        nodesGlobalIndexes[1] = getNodeGlobalIndex(xEnd, yEnd);

        theta[0] = elementEdgeLength
                * function.apply(grid.xValues[xStart], grid.yValues[yStart])
                / BoundaryConditions.COMMON_LOCAL_MATRIX_DIVISOR;
        theta[1] = elementEdgeLength
                * function.apply(grid.xValues[xEnd], grid.yValues[yEnd])
                / BoundaryConditions.COMMON_LOCAL_MATRIX_DIVISOR;

        Vector localVector = BoundaryConditions.LOCAL_MATRIX.multiplyByVector(new Vector(theta));

        for (int j = 0; j < EDGE_NODES_COUNT; ++j) {
            vector.increaseComponent(nodesGlobalIndexes[j], localVector.getComponent(j));
        }
    }

    private void setThirdConditionOnElement(int xStart, int xEnd, int yStart, int yEnd,
                                            double commonMultiplier, Matrix matrix, Vector vector,
                                            BiFunction<Double, Double, Double> function) {
        double[] vectorComponents = new double[EDGE_NODES_COUNT];
        int[] nodesGlobalIndexes = new int[EDGE_NODES_COUNT];

        nodesGlobalIndexes[0] = getNodeGlobalIndex(xStart, yStart);
        nodesGlobalIndexes[1] = getNodeGlobalIndex(xEnd, yEnd);

        vectorComponents[0] = commonMultiplier
                * function.apply(grid.xValues[xStart], grid.yValues[yStart]);
        vectorComponents[1] = commonMultiplier
                * function.apply(grid.xValues[xEnd], grid.yValues[yEnd]);

        Vector localVector = BoundaryConditions.LOCAL_MATRIX.multiplyByVector(new Vector(vectorComponents));

        for (int j = 0; j < EDGE_NODES_COUNT; ++j) {
            vector.increaseComponent(nodesGlobalIndexes[j], localVector.getComponent(j));
        }

        DenseMatrix localMatrix = new DenseMatrix(BoundaryConditions.LOCAL_MATRIX);
        localMatrix.multiplyByScalar(commonMultiplier);

        for (int j = 0; j < EDGE_NODES_COUNT; ++j) {
            for (int k = 0; k < EDGE_NODES_COUNT; ++k) {
                matrix.increaseComponent(nodesGlobalIndexes[j], nodesGlobalIndexes[k],
                        localMatrix.getComponent(j, k));
            }
        }
    }

    private FiniteElement[] generateFiniteElements() {
        FiniteElement[] elements = new FiniteElement[(grid.xValues.length - 1) * (grid.yValues.length - 1)];

        for (int i = 0, index = 0; i < grid.yValues.length - 1; ++i) {
            for (int j = 0; j < grid.xValues.length - 1; ++j) {
                elements[index] = new FiniteElement(this, j, i);
                ++index;
            }
        }

        return elements;
    }

    @Override
    public String toString() {
        return "SolutionArea X: " + Arrays.toString(grid.xValues) + System.lineSeparator()
                + "SolutionArea Y: " + Arrays.toString(grid.yValues);
    }
}