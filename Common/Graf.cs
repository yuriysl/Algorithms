﻿using System;
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

		int _index;
		double _hWeightChange;
		bool _marked;
		Vertex<T> _parent;
		VertexColor _color;
		double _discoveryTime;
		double _finishingTime;
		T _key;
		object _mstNode;
		readonly List<Edge<T>> _edges;

		#endregion

		#region Properties

		public T Key
		{
			get { return _key; }
		}

		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}

		public double HWeightChange
		{
			get { return _hWeightChange; }
			set { _hWeightChange = value; }
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

		public double DiscoveryTime
		{
			get { return _discoveryTime; }
			set { _discoveryTime = value; }
		}

		public double FinishingTime
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

		public void AddEdge(Edge<T> newEdge)
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
		private Guid _id;
		private bool _marked;

		#endregion

		#region Properties

		public Guid ID
		{
			get { return _id; }
		}

		public bool Marked
		{
			get { return _marked; }
			set { _marked = value; }
		}

		public double Weigth
		{
			get { return _weigth; }
			set { _weigth = value; }
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
			_id = Guid.NewGuid();
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
		double _time;

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
				_vertexes[i].DiscoveryTime = double.PositiveInfinity;
				_vertexes[i].FinishingTime = double.PositiveInfinity;
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
				_vertexes[i].DiscoveryTime = double.PositiveInfinity;
				_vertexes[i].FinishingTime = double.PositiveInfinity;
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
					targetVertex.AddEdge(newEdge);
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
				_vertexes[i].DiscoveryTime = double.PositiveInfinity;
				_vertexes[i].FinishingTime = double.PositiveInfinity;
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

		public Dictionary<T, Set<Vertex<T>>> GetStronglyConnectedComponentsWithUnions()
		{
			var components = new Dictionary<T, Set<Vertex<T>>>();

			for (int i = 0; i < _vertexes.Count; i++)
			{
				var newSet = new Set<Vertex<T>>(_vertexes[i]);
				_vertexes[i].MstNode = newSet.Head;
				components[_vertexes[i].Key] = newSet;
			}
			for (int i = 0; i < _vertexes.Count; i++)
			{
				for (int j = 0; j < _vertexes[i].Edges.Count; j++)
				{
					var adjacentVertex = _vertexes[i].Edges[j].Other(_vertexes[i]);
					var vertexHead = ((SetNode<Vertex<T>>)(_vertexes[i].MstNode)).Head;
					var adjacentVertexHead = ((SetNode<Vertex<T>>)(adjacentVertex.MstNode)).Head;
					if (vertexHead != adjacentVertexHead)
					{
						var vertexSet = components[vertexHead.Value.Key];
						var adjacentVertexSet = components[adjacentVertexHead.Value.Key];

						components.Remove(vertexHead.Value.Key);
						components.Remove(adjacentVertexHead.Value.Key);

						var unionSet = vertexSet.Union(adjacentVertexSet);
						components[unionSet.Head.Value.Key] = unionSet;
					}
				}
			}

			return components;
		}

		public Graf<T> GetMSTGrafPrim(Vertex<T> root)
		{
			var vertexes = new Dictionary<T, Vertex<T>>();
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
			var binaryHeap = new BinaryHeap<double, Tuple<Edge<T>, Vertex<T>>>(nodes, node => node.Value.Item2.MstNode = node);
			(((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).BuildMin();

			while(binaryHeap.HeapSize > 0)
			{
				var node = (((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).ExtractMin();
				var edge = node.Value.Item1;
				var vertex = node.Value.Item2;
				vertex.Marked = true;

				var nextVertex = new Vertex<T>(vertex.Key);
				if (edge != null)
				{
					Vertex<T> sourceVertex = vertexes[edge.Other(vertex).Key];
					var newEdge = new Edge<T>(edge.Weigth, sourceVertex, nextVertex);
					nextVertex.AddEdge(newEdge);
					sourceVertex.AddEdge(newEdge);
				}
				mstGraf.Vertexes.Add(nextVertex);
				vertexes.Add(nextVertex.Key, nextVertex);

				for (int i = 0; i < vertex.Edges.Count; i++)
				{
					var adjacentVertex = vertex.Edges[i].Other(vertex);
					var adjacentNode = (BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>)(adjacentVertex.MstNode);
					if (!adjacentVertex.Marked && vertex.Edges[i].Weigth < adjacentNode.Key)
					{
						adjacentVertex.Parent = vertex;
						adjacentNode.Value = new Tuple<Edge<T>, Vertex<T>>(vertex.Edges[i], adjacentNode.Value.Item2);
						(((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).DecreaseKey(adjacentNode, vertex.Edges[i].Weigth);
					}
				}
			}
			return mstGraf;
		}

		public Graf<T> GetMSTGrafKruskal()
		{
			var vertexes = new Dictionary<T, Vertex<T>>();
			var edges = new Dictionary<Guid, BaseNode<double, Edge<T>>>();
			var mstGraf = new Graf<T>();

			var components = new Dictionary<T, Set<Vertex<T>>>();

			for (int i = 0; i < _vertexes.Count; i++)
			{
				var newSet = new Set<Vertex<T>>(_vertexes[i]);
				_vertexes[i].MstNode = newSet.Head;
				components[_vertexes[i].Key] = newSet;
				for (int j = 0; j < _vertexes[i].Edges.Count; j++)
				{
					if(!edges.ContainsKey(_vertexes[i].Edges[j].ID))
						edges.Add(_vertexes[i].Edges[j].ID, new BaseNode<double, Edge<T>>(0, _vertexes[i].Edges[j].Weigth, _vertexes[i].Edges[j]));
				}
			}

			var sorting = new Sorting();
			var edgesList = edges.Values.ToList();
			sorting.InsertionSort(edgesList);

			for (int j = 0; j < edgesList.Count; j++)
			{
				var vertex = edgesList[j].Value.Source;
				var adjacentVertex = edgesList[j].Value.Target;
				var vertexHead = ((SetNode<Vertex<T>>)(vertex.MstNode)).Head;
				var adjacentVertexHead = ((SetNode<Vertex<T>>)(adjacentVertex.MstNode)).Head;
				if (vertexHead != adjacentVertexHead)
				{
					var vertexSet = components[vertexHead.Value.Key];
					var adjacentVertexSet = components[adjacentVertexHead.Value.Key];

					components.Remove(vertexHead.Value.Key);
					components.Remove(adjacentVertexHead.Value.Key);

					var unionSet = vertexSet.Union(adjacentVertexSet);
					components[unionSet.Head.Value.Key] = unionSet;

					Vertex<T> mstVertex;
					if (!vertexes.TryGetValue(vertex.Key, out mstVertex))
					{
						mstVertex = new Vertex<T>(vertex.Key);
						vertexes.Add(mstVertex.Key, mstVertex);
						mstGraf.AddVertex(mstVertex);
					}
					Vertex<T> mstAdjacentVertex;
					if (!vertexes.TryGetValue(adjacentVertex.Key, out mstAdjacentVertex))
					{
						mstAdjacentVertex = new Vertex<T>(adjacentVertex.Key);
						vertexes.Add(mstAdjacentVertex.Key, mstAdjacentVertex);
						mstGraf.AddVertex(mstAdjacentVertex);
					}
					var newEdge = new Edge<T>(edgesList[j].Value.Weigth, mstVertex, mstAdjacentVertex);
					mstVertex.AddEdge(newEdge);
					mstAdjacentVertex.AddEdge(newEdge);
				}
			}
			return mstGraf;
		}

		public bool BellmanFordShortestPaths(Vertex<T> s)
		{
			InitializeSingleSource(s);

			for (int i = 0; i < _vertexes.Count - 1; i++)
			{
				for (int j = 0; j < _vertexes.Count; j++)
				{
					var u = _vertexes[j];
					for (int k = 0; k < u.Edges.Count; k++)
					{
						var w = u.Edges[k];
						var v = w.Other(u);
						Relax(u, v, w);
					}
				}
			}
			for (int j = 0; j < _vertexes.Count; j++)
			{
				var u = _vertexes[j];
				for (int k = 0; k < u.Edges.Count; k++)
				{
					var w = u.Edges[k];
					var v = w.Other(u);
					if (v.DiscoveryTime > u.DiscoveryTime + w.Weigth)
						return false;
				}
			}
			return true;
		}

		public void TopologicalShortestPaths(Vertex<T> s)
		{
			DFS();
			InitializeSingleSource(s);

			var topologicalSortList = _topologicalSort.ToList();
			for (int j = 0; j < topologicalSortList.Count; j++)
			{
				var u = topologicalSortList[j];
				for (int k = 0; k < u.Edges.Count; k++)
				{
					var w = u.Edges[k];
					var v = w.Other(u);
					Relax(u, v, w);
				}
			}
		}

		public void DejkstraShortestPaths(Vertex<T> s)
		{
			InitializeSingleSource(s);

			var vertexes = new Dictionary<T, Vertex<T>>();

			var nodes = new List<BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>>();
			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Marked = false;
				_vertexes[i].Parent = null;
				var node = new BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>(i, _vertexes[i].DiscoveryTime, new Tuple<Edge<T>, Vertex<T>>(null, _vertexes[i]));
				_vertexes[i].MstNode = node;
				nodes.Add(node);
			}
			var binaryHeap = new BinaryHeap<double, Tuple<Edge<T>, Vertex<T>>>(nodes, node => node.Value.Item2.MstNode = node);
			(((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).BuildMin();

			while (binaryHeap.HeapSize > 0)
			{
				var node = (((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).ExtractMin();
				var edge = node.Value.Item1;
				var u = node.Value.Item2;
				u.Marked = true;

				vertexes.Add(u.Key, u);

				for (int i = 0; i < u.Edges.Count; i++)
				{
					var w = u.Edges[i];
					var v = w.Other(u);
					if (v.Marked)
						continue;
					var adjacentNode = (BinaryHeapNode<double, Tuple<Edge<T>, Vertex<T>>>)(v.MstNode);
					var oldDiscoveryTime = v.DiscoveryTime;
					Relax(u, v, w);
					if (oldDiscoveryTime != v.DiscoveryTime)
					{
						adjacentNode.Value = new Tuple<Edge<T>, Vertex<T>>(u.Edges[i], adjacentNode.Value.Item2);
						(((IMinHeap<double, Tuple<Edge<T>, Vertex<T>>>)binaryHeap)).DecreaseKey(adjacentNode, v.DiscoveryTime);
					}
				}
			}
		}

		private void InitializeSingleSource(Vertex<T> root)
		{
			for (int i = 0; i < _vertexes.Count; i++)
			{
				_vertexes[i].Marked = false;
				_vertexes[i].Parent = null;
				_vertexes[i].DiscoveryTime = double.PositiveInfinity;
				_vertexes[i].FinishingTime = double.PositiveInfinity;
			}
			root.DiscoveryTime = 0;
		}

		private void Relax(Vertex<T> u, Vertex<T> v, Edge<T> w)
		{
			if(v.DiscoveryTime > u.DiscoveryTime + w.Weigth)
			{
				v.DiscoveryTime = u.DiscoveryTime + w.Weigth;
				v.Parent = u;
			}
		}

		public double[,] GetAllPairsShortestsPaths(double[,] w)
		{
			int n = w.GetLength(0);
			var lPrime = w;
			for (int m = 2; m <= n - 1; m++)
			{
				lPrime = ExtendsShortestsPaths(lPrime, w);
			}
			return lPrime;
		}

		public Tuple<double[,], int?[,]> GetAllPairsShortestsPathsFloydWarshall(double[,] w)
		{
			int n = w.GetLength(0);
			var d = new double[n + 1][,];
			var p = new int?[n + 1][,];
			d[0] = w;
			p[0] = new int?[n, n];
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (i == j)
						p[0][i, j] = null;
					else
						p[0][i, j] = i;
				}
			}

			for (int k = 0; k < n; k++)
			{
				d[k + 1] = new double[n, n];
				p[k + 1] = new int?[n, n];
				for (int i = 0; i < n; i++)
				{
					for (int j = 0; j < n; j++)
					{
						d[k + 1][i, j] = Math.Min(d[k][i, j], d[k][i, k] + d[k][k, j]);
						if (d[k][i, j] <= d[k][i, k] + d[k][k, j])
							p[k + 1][i, j] = p[k][i, j];
						else
							p[k + 1][i, j] = p[k][k, j];
					}
				}
			}
			return new Tuple<double[,], int?[,]>(d.Last(), p.Last());
		}

		public double[,] GetAllPairsShortestsPathsJohnson()
		{
			var sVertex = new Vertex<T>(default(T));
			sVertex.Index = -1;
			var grafPrime = GetGrafPrime(sVertex);

			if (!grafPrime.BellmanFordShortestPaths(sVertex))
				return null;

			UpdateWeights(grafPrime);

			int n = _vertexes.Count;
			var d = new double[n, n];
			for (int i = 0; i < n; i++)
			{
				_vertexes[i].Index = i;
			}
			for (int i = 0; i < n; i++)
			{
				var vertex = _vertexes[i];
				DejkstraShortestPaths(vertex);
				for (int j = 0; j < n; j++)
				{
					d[i, j] = _vertexes[j].DiscoveryTime + _vertexes[j].HWeightChange - _vertexes[i].HWeightChange;
				}
			}
			return d;
		}

		private Graf<T> GetGrafPrime(Vertex<T> sVertex)
		{
			var grafPrime = new Graf<T>();
			
			grafPrime.AddVertex(sVertex);

			for (int i = 0; i < _vertexes.Count; i++)
			{
				sVertex.AddDirectedEdge(0, _vertexes[i]);
				grafPrime.AddVertex(_vertexes[i]);
			}
			return grafPrime;
		}

		private void UpdateWeights(Graf<T> grafPrime)
		{
			int n = grafPrime.Vertexes.Count;
			for (int i = 0; i < n; i++)
				grafPrime.Vertexes[i].HWeightChange = grafPrime.Vertexes[i].DiscoveryTime;

			for (int i = 0; i < n; i++)
			{
				var vertex = grafPrime.Vertexes[i];
				for (int j = 0; j < vertex.Edges.Count; j++)
				{
					var adjacentVertex = vertex.Edges[j].Other(vertex);
					vertex.Edges[j].Weigth = vertex.Edges[j].Weigth + vertex.HWeightChange - adjacentVertex.HWeightChange;
				}
			}
		}

		public double[,] GetWeights()
		{
			int n = _vertexes.Count;
			var weights = new double[n, n];
			for (int i = 0; i < n; i++)
			{
				_vertexes[i].Index = i;
			}
			for (int i = 0; i < n; i++)
			{
				var vertex = _vertexes[i];
				for (int j = 0; j < n; j++)
					weights[vertex.Index, j] = i == j ? 0 : double.PositiveInfinity;

				for (int j = 0; j < vertex.Edges.Count; j++)
				{
					var adjacentVertex = vertex.Edges[j].Other(vertex);
					weights[vertex.Index, adjacentVertex.Index] = vertex.Edges[j].Weigth;
				}
			}
			return weights;
		}

		private double[,] ExtendsShortestsPaths(double[,] l, double[,] w)
		{
			int n = w.GetLength(0);
			var lPrime = new double[n, n];
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					lPrime[i, j] = double.PositiveInfinity;
					for (int k = 0; k < n; k++)
					{
						lPrime[i, j] = Math.Min(lPrime[i, j], l[i, k] + l[k, j]);
					}
				}
			}
			return lPrime;
		}

		#endregion
	}
}
