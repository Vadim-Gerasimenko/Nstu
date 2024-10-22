using MathematicalExpressions.operations.operands;
using static MathematicalExpressions.operations.types.functions.power.PowerFunctions;

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

static string getVariablesToString(IEnumerable<string> variables) => $"[{string.Join(", ", variables)}]";