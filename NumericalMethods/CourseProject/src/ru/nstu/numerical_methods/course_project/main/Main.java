package ru.nstu.numerical_methods.course_project.main;

import ru.nstu.numerical_methods.course_project.slae.Slae;
import ru.nstu.numerical_methods.course_project.slae.Vector;
import ru.nstu.numerical_methods.course_project.solution_area.*;

import java.io.File;
import java.util.List;
import java.util.function.BiFunction;

public class Main {
    public static void main(String[] args) {
        final String directoryPath = "NumericalMethods/CourseProject/src/ru/nstu/numerical_methods/course_project/resources/";
        List<BiFunction<Double, Double, Double>> lambdaFunctions = List.of(
                (x, y) -> 1.,
                (x, y) -> 1.,
                (x, y) -> 1.
        );

        List<BiFunction<Double, Double, Double>> gammaFunctions = List.of(
                (x, y) -> 1.,
                (x, y) -> 1.,
                (x, y) -> 1.
        );

        List<BiFunction<Double, Double, Double>> rightFunctions = List.of(
                (x, y) -> 2 * Math.sin(x),
                (x, y) -> 2 * Math.sin(x),
                (x, y) -> 2 * Math.sin(x)
        );

        List<BiFunction<Double, Double, Double>> firstKind = List.of(
                (x, y) -> Math.sin(x)
        );
        List<BiFunction<Double, Double, Double>> secondKind = List.of(
                (x, y) -> Math.cos(6),
                (x, y) -> 0.
        );
        List<BiFunction<Double, Double, Double>> thirdKind = List.of(
                (x, y) -> Math.sin(x),
                (x, y) -> Math.sin(x),
                (x, y) -> Math.sin(1) - 2 * Math.cos(x)
        );
        List<Double> beta = List.of(1., 2., 0.5);

        File areaDescription = new File(directoryPath + "AreaDescription.txt");
        File boundariesDescription = new File(directoryPath + "BoundariesDescription.txt");
        File intervalsDescription = new File(directoryPath + "IntervalsDescription.txt");

        Equation equation = new Equation(lambdaFunctions, gammaFunctions, rightFunctions);

        Area area = new Area(areaDescription, equation);
        Grid grid = new Grid(area, intervalsDescription);

        BoundaryConditions conditions = new BoundaryConditions(boundariesDescription,
                firstKind, secondKind, thirdKind, beta);

        SolutionArea solutionArea = new SolutionArea(area, grid, conditions);
        Slae slae = solutionArea.assembleSlae();
        Vector result = slae.solve();
    }
}