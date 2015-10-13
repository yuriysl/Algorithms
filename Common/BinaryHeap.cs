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

		public BinaryHeapNode(int index)
		{
			_index = index;
		}

		public BinaryHeapNode(int index, TKey key)
			: this(index)
		{
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

		public BinaryHeap()
		{
			_nodes = new List<BinaryHeapNode<TKey, TValue>>();
		}

		#endregion

		#region Properties

		public int Length
		{
			get { return _nodes.Count; }
		}

		public int HeapSize
		{
			get { return _heapSize; }
		}

		#endregion

		#region Methods

		public void MaxHeapify(BinaryHeapNode<TKey, TValue> node)
		{
			int left = node.Left;
			int right = node.Right;

			int largest = (left < HeapSize && _nodes[left].Key.CompareTo(node.Key) > 0) ? left : node.Index;
			if (right < HeapSize && _nodes[right].Key.CompareTo(_nodes[largest].Key) > 0)
				largest = right;

			if(largest != node.Index)
			{
				Swap(node, _nodes[largest]);
				MaxHeapify(_nodes[largest]);
			}
		}

		private void Swap(BinaryHeapNode<TKey, TValue> left, BinaryHeapNode<TKey, TValue> right)
		{
			TKey tmpKey = left.Key;
			left.Key = right.Key;
			right.Key = tmpKey;

			TValue tmpValue = left.Value;
			left.Value = right.Value;
			right.Value = tmpValue;
		}

		#endregion
	}
}
