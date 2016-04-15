using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Common;

namespace Algorithms.SRMs.SRM675
{
	/*
	Problem Statement
    
Your task is to write a calculator that will convert between four different length units: inches (in), feet (ft), yards (yd), and miles (mi).  
The conversions between these units:
1 ft = 12 in
1 yd = 3 ft
1 mi = 1760 yd
You are given an int amount and strings fromUnit and toUnit. Compute and return the amount of toUnits that corresponds to amount fromUnits. 
(For example, if amount=41, fromUnit="mi", and toUnit="in", you are supposed to compute the number of inches in 41 miles.) Note that the result will not necessarily be an integer.
Definition
    
Class:
LengthUnitCalculator
Method:
calc
Parameters:
int, string, string
Returns:
double
Method signature:
double calc(int amount, string fromUnit, string toUnit)
(be sure your method is public)
Limits
    
Time limit (s):
2.000
Memory limit (MB):
256
Notes
-
Pay attention to the unusual time limit.
Constraints
-
amount will be between 1 and 1,000, inclusive.
-
fromUnit will be one of {"in", "ft", "yd", "mi"}.
-
toUnit will be one of {"in", "ft", "yd", "mi"}.
Examples
0)

    
1
"mi"
"ft"
Returns: 5280.0
We are asked to convert 1 mile into feet. From the information in the statement we know that 1 mi = 1760 yd = (1760 * 3) ft = 5280 ft.
1)

    
1
"ft"
"mi"
Returns: 1.893939393939394E-4
Here we have 1 ft = 1/5280 mi, which is approximately 0.000189394 miles.
2)

    
123
"ft"
"yd"
Returns: 41.0

3)

    
1000
"mi"
"in"
Returns: 6.336E7

4)

    
1
"in"
"mi"
Returns: 1.5782828282828283E-5

5)

    
47
"mi"
"mi"
Returns: 47.0
	*/
	public class LengthUnitCalculator
	{
		enum Units
		{
			inch = 0,
			ft = 1,
			yd = 2,
			mi = 3

		}

		public double calc(int amount, string fromUnit, string toUnit)
		{
			var from = (Units) Enum.Parse(typeof (Units), fromUnit.Replace("in", "inch"));
			var to = (Units) Enum.Parse(typeof (Units), toUnit.Replace("in", "inch"));

			var dgraf = new Graf<Units>();

			var ftVertex = new Vertex<Units>(Units.ft);
			var inVertex = new Vertex<Units>(Units.inch);
			var ydVertex = new Vertex<Units>(Units.yd);
			var miVertex = new Vertex<Units>(Units.mi);

			ftVertex.AddDirectedEdge(12, inVertex);
			inVertex.AddDirectedEdge(1D / 12, ftVertex);

			ydVertex.AddDirectedEdge(3, ftVertex);
			ftVertex.AddDirectedEdge(1D / 3, ydVertex);

			miVertex.AddDirectedEdge(1760, ydVertex);
			ydVertex.AddDirectedEdge(1D / 1760, miVertex);

			dgraf.AddVertex(ftVertex);
			dgraf.AddVertex(inVertex);
			dgraf.AddVertex(ydVertex);
			dgraf.AddVertex(miVertex);

			var source = dgraf.Vertexes.FirstOrDefault(vertex => vertex.Key == from);
			var target = dgraf.Vertexes.FirstOrDefault(vertex => vertex.Key == to);

			var vertexPath = dgraf.GetBreadthPath(source, target).ToList();

			var edgePath = new System.Collections.Generic.Stack<Edge<Units>>();
			if (target.Color != Vertex<Units>.VertexColor.Black)
				return 0;

			var currentVertex = target;
			while (currentVertex != null && currentVertex != source && currentVertex.Parent != null)
			{
				var currentEdge = currentVertex.Parent.Edges.FirstOrDefault(edge => edge.Target == currentVertex);
				edgePath.Push(currentEdge);
				currentVertex = currentVertex.Parent;
			}

			if (source == target)
				return amount;
			if (!edgePath.Any())
				return 0;

			double res = amount;
			double weigth = 1;
			foreach (var pathEdge in edgePath)
					weigth *= pathEdge.Weigth;
			return res * weigth;
		}
	}
}
