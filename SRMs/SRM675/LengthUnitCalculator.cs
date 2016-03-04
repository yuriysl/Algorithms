using System;
using System.Collections.Generic;
using System.Linq;

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
		class Node<T>
		{
			#region Fields

			bool _marked;
			T _key;
			readonly List<Edge<T>> _edges;

			#endregion

			#region Properties

			public bool Marked
			{
				get { return _marked; }
				set { _marked = value; }
			}

			public T Key
			{
				get { return _key; }
			}

			public List<Edge<T>> Edges
			{
				get { return _edges; }
			}

			#endregion

			#region Constructors

			public Node(T key)
			{
				_key = key;
				_edges = new List<Edge<T>>();
			}

			#endregion

			#region Methods

			public void AddEdge(double weigth, bool isRevers, Node<T> target)
			{
				_edges.Add(new Edge<T>(weigth, isRevers, this, target));
			}

			#endregion
		}

		class Edge<T>
		{
			#region Fields

			private double _weigth;
			private bool _isRevers;
			private Node<T> _source;
			private Node<T> _target;

			#endregion

			#region Properties

			public double Weigth
			{
				get { return _weigth; }
			}

			public bool IsRevers
			{
				get { return _isRevers; }
			}

			public Node<T> Source
			{
				get { return _source; }
			}

			public Node<T> Target
			{
				get { return _target; }
			}

			#endregion

			#region Constructors

			public Edge(double weigth, bool isRevers, Node<T> source, Node<T> target)
			{
				_weigth = weigth;
				_isRevers = isRevers;
				_source = source;
				_target = target;
			}

			#endregion
		}

		class DGraf<T>
		{
			#region Fields

			private List<Node<T>> _nodes;
			private Queue<Edge<T>> _edgePath;

			#endregion

			#region Properties

			public List<Node<T>> Nodes
			{
				get { return _nodes; }
			}

			#endregion

			#region Constructors

			public DGraf()
			{
				_nodes = new List<Node<T>>();
				_edgePath = new Queue<Edge<T>>();
			}

			#endregion

			#region Methods

			public void AddNode(Node<T> node)
			{
				_nodes.Add(node);
			}

			public IEnumerable<Node<T>> GetPath(Node<T> source, Node<T> target)
			{
				var edgePath = GetEdgePath(source, target).ToList();
				var nodePath = new List<Node<T>>();
				if (edgePath.Any())
				{
					nodePath.Add(source);
					nodePath.AddRange(edgePath.Select(edge => edge.Target));
				}
				return nodePath;
			}

			public IEnumerable<Edge<T>> GetEdgePath(Node<T> source, Node<T> target)
			{
				if(source == null || target == null)
					throw new ArgumentNullException();

				_edgePath.Clear();
				foreach (var node in _nodes)
					node.Marked = false;

				DeepFirstSearch(source);

				var edgePath = new Stack<Edge<T>>();
				if(!target.Marked)
					return edgePath;

				var currentEdge = _edgePath.FirstOrDefault(edge => edge.Target == target);
				while (currentEdge != null && currentEdge.Source != source)
				{
					edgePath.Push(currentEdge);
					currentEdge = _edgePath.FirstOrDefault(edge => edge.Target == currentEdge.Source);
				}
				if(currentEdge != null)
					edgePath.Push(currentEdge);

				return edgePath;
			}

			private void DeepFirstSearch(Node<T> source)
			{
				source.Marked = true;
				foreach (var edge in source.Edges)
				{
					if(edge.Target.Marked)
						continue;
					_edgePath.Enqueue(edge);
					DeepFirstSearch(edge.Target);
				}
			}

			#endregion
		}

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

			var dgraf = new DGraf<Units>();

			var ftNode = new Node<Units>(Units.ft);
			var inNode = new Node<Units>(Units.inch);
			var ydNode = new Node<Units>(Units.yd);
			var miNode = new Node<Units>(Units.mi);

			ftNode.AddEdge(12, false, inNode);
			inNode.AddEdge(12, true, ftNode);

			ydNode.AddEdge(3, false, ftNode);
			ftNode.AddEdge(3, true, ydNode);

			miNode.AddEdge(1760, false, ydNode);
			ydNode.AddEdge(1760, true, miNode);

			dgraf.AddNode(ftNode);
			dgraf.AddNode(inNode);
			dgraf.AddNode(ydNode);
			dgraf.AddNode(miNode);

			var source = dgraf.Nodes.FirstOrDefault(node => node.Key == from);
			var target = dgraf.Nodes.FirstOrDefault(node => node.Key == to);

			var edgePath = dgraf.GetEdgePath(source, target).ToList();

			if(source == target)
				return amount;
			if (!edgePath.Any())
				return 0;

			double res = amount;
			double directWeigth = 1;
			double reversWeigth = 1;
			foreach (var pathEdge in edgePath)
			{
				if(!pathEdge.IsRevers)
					directWeigth *= pathEdge.Weigth;
				else
					reversWeigth *= pathEdge.Weigth;
			}
			return res * directWeigth * 1 / reversWeigth;
		}
	}
}
