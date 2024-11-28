package ru.nstu.numerical_methods.course_project.main;

import ru.nstu.numerical_methods.course_project.slae.matrix.Slae;
import ru.nstu.numerical_methods.course_project.slae.matrix.Vector;
import ru.nstu.numerical_methods.course_project.solution_area.*;

import java.io.File;
import java.util.List;
import java.util.function.BiFunction;

public class Main {
    public static void main(String[] args) {
        final String directoryPath = "NumericalMethods/CourseProject/src/ru/nstu/numerical_methods/course_project/resources/";

        List<BiFunction<Double, Double, Double>> lambdaFunctions = List.of(
                (x, y) -> 1.,
                (x, y) -> 10.,
                (x, y) -> 10.
        );

        List<BiFunction<Double, Double, Double>> gammaFunctions = List.of(
                (x, y) -> 2.,
                (x, y) -> 1.,
                (x, y) -> 0.
        );

        List<BiFunction<Double, Double, Double>> rightFunctions = List.of(
                (x, y) -> 2. * x,
                (x, y) -> 1.8 + 0.1 * x,
                (x, y) -> 0.
        );

        List<BiFunction<Double, Double, Double>> firstKind = List.of(
                (x, y) -> 2.,
                (x, y) -> 0.1 * x + 1.8
        );
        List<BiFunction<Double, Double, Double>> secondKind = List.of(
                (x, y) -> 1.,
                (x, y) -> 0.
        );
        List<BiFunction<Double, Double, Double>> thirdKind = List.of(
                (x, y) -> x,
                (x, y) -> 0.1 * x + 1.8,
                (x, y) -> -1.
        );
        List<Double> beta = List.of(1., 2., 0.5);

        Equation equation = new Equation(lambdaFunctions, gammaFunctions, rightFunctions);

        File areaDescription = new File(directoryPath + "AreaDescription.txt");
        File boundariesDescription = new File(directoryPath + "BoundariesDescription.txt");
        File intervalsDescription = new File(directoryPath + "IntervalsDescription.txt");

        Area area = new Area(areaDescription, equation);
        Grid grid = new Grid(area, intervalsDescription);
        BoundaryConditions conditions = new BoundaryConditions(boundariesDescription,
                firstKind, secondKind, thirdKind, beta);

        SolutionArea solutionArea = new SolutionArea(area, grid, conditions);
        Slae slae = solutionArea.assembleSlae();

        Vector result = slae.solve();

        System.out.printf(slae + "%n%n");
        System.out.println("Result: " + result);

        double[] xValues = grid.getXValues();
        double[] yValues = grid.getYValues();

        for (int i = 0, index = 0; i < yValues.length; ++i) {
            for (int j = 0; j < xValues.length; ++index, ++j) {
                System.out.println("In point  (" + xValues[j] + "; " + yValues[i] + ") = "
                        + String.format("%.15f", result.getComponent(index)));
            }
        }
    }
}