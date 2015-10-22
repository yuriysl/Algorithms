using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class BinaryHeapNode<TKey, TValue>
	{
		int _index;
		TKey _key;
		TValue _value;

		public int Parent { get { return (_index + 1) / 2 - 1; }}
		public int Left { get { return _index * 2 + 1; }}
		public int Right { get { return _index * 2 + 2; }}
		public int Index{ get { return _index; } }
		public TKey Key { get { return _key; } set { _key = value; } }
		public TValue Value { get { return _value; } set { _value = value; } }

		public BinaryHeapNode(int index, TKey key)
		{
			_index = index;
			_key = key;
		}

		public BinaryHeapNode(int index, TKey key, TValue value)
			: this(index, key)
		{
			_value = value;
		}
	}

	public class BinaryHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		#region Fields

		List<BinaryHeapNode<TKey, TValue>> _nodes;
		int _heapSize;

		#endregion

		#region Constructors

		public BinaryHeap(List<BinaryHeapNode<TKey, TValue>> nodes)
		{
			_nodes = nodes;
		}

		#endregion

		#region Properties

		public BinaryHeapNode<TKey, TValue> this[int index]
		{
			get { return _nodes[index]; }
		}

		public int Length
		{
			get { return _nodes.Count; }
		}

		public int HeapSize
		{
			get { return _heapSize; }
			set { _heapSize = value; }
		}

		#endregion

		#region Methods

		public void BuildMax()
		{
			int n = _nodes.Count;
			_heapSize = n;
			for(int i = n / 2 - 1; i >= 0; i--)
			{
				MaxHeapify(_nodes[i]);
			}
		}

		public void BuildMaxTail()
		{
			int n = _nodes.Count;
			_heapSize = n;
			for (int i = n / 2 - 1; i >= 0; i--)
				MaxHeapifyTail(_nodes[i]);
		}

		public void BuildMin()
		{
			int n = _nodes.Count;
			_heapSize = n;
			for (int i = n / 2 - 1; i >= 0; i--)
			{
				MinHeapify(_nodes[i]);
			}
		}

		public void BuildMinTail()
		{
			int n = _nodes.Count;
			_heapSize = n;
			for (int i = n / 2 - 1; i >= 0; i--)
			{
				MinHeapifyTail(_nodes[i]);
			}
		}

		public void MaxHeapify(BinaryHeapNode<TKey, TValue> node)
		{
			int left = node.Left;
			int right = node.Right;
			int largest = node.Index;

			if (left < HeapSize && _nodes[left].Key.CompareTo(node.Key) > 0)
				largest = left;
			if (right < HeapSize && _nodes[right].Key.CompareTo(_nodes[largest].Key) > 0)
				largest = right;

			if(largest != node.Index)
			{
				Swap(node, _nodes[largest]);
				MaxHeapify(_nodes[largest]);
			}
		}

		public void MaxHeapifyTail(BinaryHeapNode<TKey, TValue> node)
		{
			int left = node.Left;
			int right = node.Right;
			int largest = -1;
			var currentNode = node;

			while (largest != currentNode.Index)
			{
				if (largest > -1)
					currentNode = _nodes[largest];
				largest = currentNode.Index;
				if (left < HeapSize && _nodes[left].Key.CompareTo(currentNode.Key) > 0)
					largest = left;
				if (right < HeapSize && _nodes[right].Key.CompareTo(_nodes[largest].Key) > 0)
					largest = right;

				if (largest != currentNode.Index)
					Swap(currentNode, _nodes[largest]);

				left = _nodes[largest].Left;
				right = _nodes[largest].Right;
			}
		}

		public void MinHeapify(BinaryHeapNode<TKey, TValue> node)
		{
			int left = node.Left;
			int right = node.Right;
			int lowest = node.Index;

			if (left < HeapSize && _nodes[left].Key.CompareTo(node.Key) < 0)
				lowest = left;
			if (right < HeapSize && _nodes[right].Key.CompareTo(_nodes[lowest].Key) < 0)
				lowest = right;

			if (lowest != node.Index)
			{
				Swap(node, _nodes[lowest]);
				MinHeapify(_nodes[lowest]);
			}
		}

		public void MinHeapifyTail(BinaryHeapNode<TKey, TValue> node)
		{
			int left = node.Left;
			int right = node.Right;
			int lowest = -1;
			var currentNode = node;

			while (lowest != currentNode.Index)
			{
				if (lowest > -1)
					currentNode = _nodes[lowest];
				lowest = currentNode.Index;
				if (left < HeapSize && _nodes[left].Key.CompareTo(currentNode.Key) < 0)
					lowest = left;
				if (right < HeapSize && _nodes[right].Key.CompareTo(_nodes[lowest].Key) < 0)
					lowest = right;

				if (lowest != currentNode.Index)
					Swap(currentNode, _nodes[lowest]);

				left = _nodes[lowest].Left;
				right = _nodes[lowest].Right;
			}
		}

		static public void Swap(BinaryHeapNode<TKey, TValue> left, BinaryHeapNode<TKey, TValue> right)
		{
			TKey tmpKey = left.Key;
			left.Key = right.Key;
			right.Key = tmpKey;

			TValue tmpValue = left.Value;
			left.Value = right.Value;
			right.Value = tmpValue;
		}

		private string ToStringTree()
		{
			var treeBuilder = new StringBuilder();
			int h = (int)Math.Floor(Math.Log(_heapSize, 2));
			int levelN = (int)Math.Floor(_heapSize / Math.Pow(2, h));
			int currLevelN = 0;
			for (int i = 0; i < _heapSize; i++)
			{
				currLevelN++;
				treeBuilder.Append(string.Format("[{0}]", _nodes[i].Key.ToString().PadLeft(2)));
				if(currLevelN == levelN)
				{
					h--;
					levelN = (int)Math.Floor(_heapSize / Math.Pow(2, h));
					currLevelN = 0;
					treeBuilder.AppendLine();
				}
			}
			return treeBuilder.ToString();
		}

		#endregion
	}
}
