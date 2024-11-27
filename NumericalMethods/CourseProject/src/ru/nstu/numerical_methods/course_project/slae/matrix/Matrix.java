package ru.nstu.numerical_methods.course_project.slae.matrix;

public interface Matrix {
    int getRowsCount();

    int getColumnsCount();

    double getComponent(int rowIndex, int columnIndex);

    void setComponent(int rowIndex, int columnIndex, double component);

    void resetRow(int index);

    void resetColumn(int index);

    DenseMatrix assemble();

    void increaseComponent(int rowIndex, int columnIndex, double component);

    Vector multiplyByVector(Vector vector);

    void add(Matrix matrix);

    Vector getRow(int index);
}