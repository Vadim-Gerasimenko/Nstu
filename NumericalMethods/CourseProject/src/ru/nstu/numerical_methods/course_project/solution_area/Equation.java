package ru.nstu.numerical_methods.course_project.solution_area;

import java.util.List;
import java.util.function.BiFunction;

public class Equation {
    List<BiFunction<Double, Double, Double>> lambda;
    List<BiFunction<Double, Double, Double>> gamma;
    List<BiFunction<Double, Double, Double>> rightFunctions;

    public Equation(List<BiFunction<Double, Double, Double>> lambda,
                    List<BiFunction<Double, Double, Double>> gamma,
                    List<BiFunction<Double, Double, Double>> rightFunctions) {
        this.lambda = lambda;
        this.gamma = gamma;
        this.rightFunctions = rightFunctions;
    }
}