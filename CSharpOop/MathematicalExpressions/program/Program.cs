using MathematicalExpressions;
using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.vectors;
using static MathematicalExpressions.operations.types.functions.power.PowerFunctions;
using static MathematicalExpressions.operations.types.functions.trigonometric.TrigonomentricFunctions;

var x = new Variable("x");
var y = new Variable("y");
var c = new Constant(3);

var expression1 = (x - 4) * (3 * x + y * y) / 5;

Console.WriteLine("Expression: " + expression1);
Console.WriteLine("Expression variables: " + getVariablesToString(expression1.Variables));
Console.WriteLine("Expression is constant: " + expression1.IsConstant);
Console.WriteLine("Expression is polynomial: " + expression1.IsPolynomial);
Console.WriteLine("Expression polynomial degree: " + expression1.PolynomialDegree);
Console.WriteLine("Expression calculating result: " + expression1.Compute(new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 }));
Console.WriteLine();

var expression2 = (5 - 3 * c) * Sqrt(16 + c * c);

Console.WriteLine("Expression: " + expression2);
Console.WriteLine("Expression variables: " + getVariablesToString(expression2.Variables));
Console.WriteLine("Expression is constant: " + expression2.IsConstant);
Console.WriteLine("Expression is polynomial: " + expression2.IsPolynomial);
Console.WriteLine("Expression polynomial degree: " + expression2.PolynomialDegree);
Console.WriteLine("Expression calculating result: " + expression2.Compute(new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 }));

expression2 = 3 * x * x + x + 5 + 2 / x;

Console.WriteLine("Expression: " + expression2);
Console.WriteLine("Expression variables: " + getVariablesToString(expression2.Variables));
Console.WriteLine("Expression is constant: " + expression2.IsConstant);
Console.WriteLine("Expression is polynomial: " + expression2.IsPolynomial);
Console.WriteLine("Expression polynomial degree: " + expression2.PolynomialDegree);
Console.WriteLine("Expression calculating result: " + expression2.Compute(new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 }));

var vector1 = new ExpressionsVector([x, 4]);
var vector2 = new ExpressionsVector([expression2, expression1]);
var vector3 = new ExpressionsVector([y]);
var vector4 = new ExpressionsVector([x]);
var vector5 = new ExpressionsVector([7, x + y, Sin(-y) + 4]);

Console.WriteLine(vector1.Norm.Compute(new Dictionary<string, double> { ["x"] = 3, ["y"] = 2 }));
Console.WriteLine(vector2);
Console.WriteLine((vector1 + vector2).Norm);
Console.WriteLine(vector1 - vector2);
Console.WriteLine(vector1 * vector2);
Console.WriteLine(+vector1);
Console.WriteLine(-vector1);
Console.WriteLine((-vector1 + vector1).Norm.Compute((new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 })));
Console.WriteLine(c * vector2);
Console.WriteLine(vector2 * y);
Console.WriteLine(c + x - vector2);
Console.WriteLine((Expression)(vector3 + vector4));
Console.WriteLine(Math.Sqrt(-16));
Console.WriteLine(Math.Tan(Math.PI/2));
Console.WriteLine(Math.Tan(0));
Console.WriteLine(1 / Math.Tan(0));
Console.WriteLine(Sqrt((Constant)7 * 7 + (x + y) * (x + y) + (Sin(-y) + 4) * (Sin(-y) + 4)));
Console.WriteLine(vector5.Norm);
Console.WriteLine(vector4.Equals(new ExpressionsVector([x])));
vector3.Subtract(vector1);
Console.WriteLine(vector3);
static string getVariablesToString(IEnumerable<string> variables) => $"[{string.Join(", ", variables)}]";