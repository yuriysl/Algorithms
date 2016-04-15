using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Common
{
	public class Vertex<T>
	{
		public enum VertexColor
		{
			White = 0,
			Gray = 1,
			Black = 2
		}

		#region Fields

		bool _marked;
		Vertex<T> _parent;
		VertexColor _color;
		long? _discoveryTime;
		long? _finishingTime;
		T _key;
		object _mstNode;
		readonly List<Edge<T>> _edges;

		#endregion

		#region Properties

		public T Key
		{
			get { return _key; }
		}

		public bool Marked
		{
			get { return _marked; }
			set { _marked = value; }
		}

		public object MstNode
		{
			get { return _mstNode; }
			set { _mstNode = value; }
		}

		public Vertex<T> Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public VertexColor Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public long? DiscoveryTime
		{
			get { return _discoveryTime; }
			set { _discoveryTime = value; }
		}

		public long? FinishingTime
		{
			get { return _finishingTime; }
			set { _finishingTime = value; }
		}

		public List<Edge<T>> Edges
		{
			get { return _edges; }
		}

		#endregion

		#region Constructors

		public Vertex(T key)
		{
			_key = key;
			_edges = new List<Edge<T>>();
		}

		#endregion

		#region Methods

		public void AddDirectedEdge(double weigth, Vertex<T> target)
		{
			var newEdge = new Edge<T>(weigth, this, target);
			_edges.Add(newEdge);
		}

		public void AddUnDirectedEdge(double weigth, Vertex<T> other)
		{
			var newEdge = new Edge<T>(weigth, this, other);
			_edges.Add(newEdge);
			other.Edges.Add(newEdge);
		}

		public void AddDirectedEdge(Edge<T> newEdge)
		{
			_edges.Add(newEdge);
		}

		#endregion
	}

	public class Edge<T>
	{
		public enum EdgeType
		{
			Tree = 0,
			Back = 1,
			Forward = 2,
			Cross = 3
		}

		#region Fields

		private double _weigth;
		private Vertex<T> _source;
		private Vertex<T> _target;
		private EdgeType _type;

		#endregion

		#region Properties

		public double Weigth
		{
			get { return _weigth; }
		}

		public Vertex<T> Source
		{
			get { return _source; }
		}

		public Vertex<T> Target
		{
			get { return _target; }
		}

		public EdgeType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		#endregion

		#region Constructors

		public Edge(double weigth, Vertex<T> source, Vertex<T> target)
		{
			_weigth = weigth;
			_source = source;
			_target = target;
		}

		#endregion

		#region Methods

		public Vertex<T> Other(Vertex<T> node)
		{
			if (_source == node)
				return _target;
			if (_target == node)
				return _source;
			return null;
		}

		#endregion
	}

	public class Graf<T>
	{
		#region Fields

		private List<Vertex<T>> _vertexes;
		private System.Collections.Generic.Stack<Vertex<T>> _topologicalSort;
		long _time;

		#endregion

		#region Properties

		public List<Vertex<T>> Vertexes
		{
			get { return _vertexes; }
		}

		#endregion

		#region Constructors

		public Graf()
		{
			_vertexes = new List<Vertex<T>>();
		}

		#endregion

		#region Methods

		public void AddVertex(Vertex<T> vertex)
		{
			_vertexes.Add(vertex);
		}

		public IEnumerable<Vertex<T>> GetBreadthPath(Vertex<T> source, Vertex<T> target)
		{
			BFS(source);
			return GetDiscoveredBreadthPath(source, target);
		}

		private IEnumerable<Vertex<T>> GetDiscoveredBreadthPath(Vertex<T> source, Vertex<T> target)
		{
			var vertexPath = new List<Vertex<T>>();
			if (source == target)
				vertexPath.Add(source);
			else
			{
				if (target.Parent == null)
					return null;

				var prefixPath = GetBreadthPath(source, target.Parent);
				if (prefixPath == null)
					return null;

				vertexPath.AddRange(prefixPath);
				vertexPath.Add(target);
			}
			return vertexPath;
		}

		private void BFS(Vertex<T> source)
		{
			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Color = Vertex<T>.VertexColor.White;
				_vertexes[i].Parent = null;
				_vertexes[i].DiscoveryTime = null;
				_vertexes[i].FinishingTime = null;
			}
			source.Color = Vertex<T>.VertexColor.Gray;
			source.DiscoveryTime = 0;
			var vertexesToDiscover = new System.Collections.Generic.Queue<Vertex<T>>();

			vertexesToDiscover.Enqueue(source);
			while(vertexesToDiscover.Count > 0)
			{
				var currentVertex = vertexesToDiscover.Dequeue();
				for (int i = 0; i < currentVertex.Edges.Count; i++)
				{
					var adjacentVertex = currentVertex.Edges[i].Other(currentVertex);
					if (adjacentVertex.Color != Vertex<T>.VertexColor.White)
						continue;
					adjacentVertex.Color = Vertex<T>.VertexColor.Gray;
					adjacentVertex.Parent = currentVertex;
					adjacentVertex.DiscoveryTime = currentVertex.DiscoveryTime + 1;
					vertexesToDiscover.Enqueue(adjacentVertex);
				}
				currentVertex.Color = Vertex<T>.VertexColor.Black;
			}
		}

		public void DFS()
		{
			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Color = Vertex<T>.VertexColor.White;
				_vertexes[i].Parent = null;
				_vertexes[i].DiscoveryTime = null;
				_vertexes[i].FinishingTime = null;
			}

			_time = 0;
			if (_topologicalSort == null)
				_topologicalSort = new System.Collections.Generic.Stack<Vertex<T>>();
			else
				_topologicalSort.Clear();

			for (int i = 0; i < _vertexes.Count; i++)
			{
				if (_vertexes[i].Color != Vertex<T>.VertexColor.White)
					continue;
				DFSVisit(_vertexes[i]);
			}
		}

		private void DFSVisit(Vertex<T> vertex)
		{
			vertex.Color = Vertex<T>.VertexColor.Gray;
			_time++;
			vertex.DiscoveryTime = _time;

			for (int i = 0; i < vertex.Edges.Count; i++)
			{
				var adjacentVertex = vertex.Edges[i].Other(vertex);
				if (adjacentVertex.Color != Vertex<T>.VertexColor.White)
				{
					if (adjacentVertex.Color == Vertex<T>.VertexColor.Gray)
					{
						vertex.Edges[i].Type = Edge<T>.EdgeType.Back;
						_topologicalSort = null;
					}
					else
						vertex.Edges[i].Type = vertex.DiscoveryTime < adjacentVertex.DiscoveryTime ? Edge<T>.EdgeType.Forward : Edge<T>.EdgeType.Cross;
					continue;
				}

				adjacentVertex.Parent = vertex;
				vertex.Edges[i].Type = Edge<T>.EdgeType.Tree;
				DFSVisit(adjacentVertex);
			}

			vertex.Color = Vertex<T>.VertexColor.Black;
			_time++;
			vertex.FinishingTime = _time;
			if (_topologicalSort != null)
				_topologicalSort.Push(vertex);
		}

		public IEnumerable<Vertex<T>> GetTopologicalSort()
		{
			return _topologicalSort;
		}

		public Graf<T> GetGrafT()
		{
			var vertexes = new Dictionary<T, Vertex<T>>();

			var grafT = new Graf<T>();
			for (int i = 0; i < _vertexes.Count; i++)
			{
				Vertex<T> sourceVertex;
				if (!vertexes.TryGetValue(_vertexes[i].Key, out sourceVertex))
				{
					sourceVertex = new Vertex<T>(_vertexes[i].Key);
					vertexes.Add(sourceVertex.Key, sourceVertex);
				}

				for (int j = 0; j < _vertexes[i].Edges.Count; j++)
				{
					var adjacentVertex = _vertexes[i].Edges[j].Other(_vertexes[i]);

					Vertex<T> targetVertex;
					if (!vertexes.TryGetValue(adjacentVertex.Key, out targetVertex))
					{
						targetVertex = new Vertex<T>(adjacentVertex.Key);
						vertexes.Add(targetVertex.Key, targetVertex);
					}
					var newEdge = new Edge<T>(_vertexes[i].Edges[j].Weigth, targetVertex, sourceVertex);
					targetVertex.AddDirectedEdge(newEdge);
				}
			}

			grafT.Vertexes.AddRange(vertexes.Values);
			return grafT;
		}

		public List<List<Vertex<T>>> GetStronglyConnectedComponents(List<Vertex<T>> vertexes)
		{
			var components = new List<List<Vertex<T>>>();

			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Color = Vertex<T>.VertexColor.White;
				_vertexes[i].Parent = null;
				_vertexes[i].DiscoveryTime = null;
				_vertexes[i].FinishingTime = null;
			}

			_time = 0;
			if (_topologicalSort == null)
				_topologicalSort = new System.Collections.Generic.Stack<Vertex<T>>();
			else
				_topologicalSort.Clear();

			var currentComponent = new List<Vertex<T>>();
			for (int i = 0; i < vertexes.Count; i++)
			{
				var currentVertex = _vertexes.FirstOrDefault(vertex => vertex.Key.Equals(vertexes[i].Key));
				if (currentVertex.Color != Vertex<T>.VertexColor.White)
				{
					currentComponent.Add(currentVertex);
					continue;
				}
				if (i > 0)
					components.Add(currentComponent);
				currentComponent = new List<Vertex<T>>();
				currentComponent.Add(currentVertex);
				DFSVisit(currentVertex);
				if (i == vertexes.Count - 1)
					components.Add(currentComponent);
			}

			return components;
		}

		public Graf<T> GetMSTGrafPrim(Vertex<T> root)
		{
			var mstGraf = new Graf<T>();

			var nodes = new List<BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>>();
			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Marked = false;
				_vertexes[i].Parent = null;
				var node = new BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>(i, _vertexes[i].Equals(root) ? 0 : double.PositiveInfinity, new Tuple<Edge<T>, Vertex<T>>(null, _vertexes[i]));
				_vertexes[i].MstNode = node;
				nodes.Add(node);
			}
			var binaryHeap = new BinaryHeap<double, Tuple<Edge<T>, Vertex<T>>>(nodes);
			(((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).BuildMin();

			while(binaryHeap.HeapSize > 0)
			{
				var node = (((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).ExtractMin();
				var edge = node.Value.Item1;
				var vertex = node.Value.Item2;
				vertex.Marked = true;

				var nextVertex = new Vertex<T>(vertex.Key);
				if(edge != null)
					nextVertex.AddUnDirectedEdge(edge.Weigth, edge.Other(vertex));
				mstGraf.Vertexes.Add(nextVertex);
				

				for (int i = 0; i < vertex.Edges.Count; i++)
				{
					var adjacentVertex = vertex.Edges[i].Other(vertex);
					if (!adjacentVertex.Marked && vertex.Edges[i].Weigth < ((adjacentVertex.MstNode)))
					{
						adjacentVertex.Parent = vertex;
						(((IMinHeap<double, Vertex<T>>)binaryHeap)).DecreaseKey(node, vertex.Edges[i].Weigth);
					}
				}
			}


			return mstGraf;
		}

			#endregion
		}
}
