using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Common
{
	public class PrefixTreeNode<TKeyItem, TValue> : BaseNode<TKeyItem, TValue>, IEnumerable<PrefixTreeNode<TKeyItem, TValue>>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Nested

		public class PreOrderEnumerator : IEnumerator<PrefixTreeNode<TKeyItem, TValue>>
		{
			private readonly PrefixTreeNode<TKeyItem, TValue> _rootNode;
			private Stack<IEnumerator<PrefixTreeNode<TKeyItem, TValue>>> _iterators;
			private bool _initialized;

			public PreOrderEnumerator(PrefixTreeNode<TKeyItem, TValue> rootNode)
			{
				_rootNode = rootNode;
			}

			public bool MoveNext()
			{
				if (!_initialized)
				{
					First();
					return _iterators.Count > 0;
				}

				if(_iterators.Count > 0 && !_iterators.Peek().MoveNext())
				{
					_iterators.Pop();
					_iterators.Peek().MoveNext();
				}

				return _iterators.Count > 0;
			}

			private void First()
			{
				_iterators = new Stack<IEnumerator<PrefixTreeNode<TKeyItem, TValue>>>();
				var i = _rootNode.GetEnumerator();
				i.MoveNext();
				_iterators.Push(i);
				_initialized = true;
			}

			public void Reset()
			{
				_initialized = false;
			}

			public PrefixTreeNode<TKeyItem, TValue> Current => _iterators.Peek().Current;

			object IEnumerator.Current => Current;

			public void Dispose()
			{
			}
		}

		#endregion

		#region Fields

		List<PrefixTreeNode<TKeyItem, TValue>> _subTrees;
		PrefixTreeNode<TKeyItem, TValue> _parent;
		public bool AWordEndsHere;
		int _frequency;

		#endregion

		#region Properties

		public PrefixTreeNode<TKeyItem, TValue> Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public List<PrefixTreeNode<TKeyItem, TValue>> SubTrees
		{
			get
			{
				if(_subTrees == null)
					_subTrees = new List<PrefixTreeNode<TKeyItem, TValue>>();
				return _subTrees;
			}
			set { _subTrees = value; }
		}

		public int Frequency
		{
			get { return _frequency; }
			set { _frequency = value; }
		}

		#endregion

		#region Constructors

		public PrefixTreeNode(TKeyItem keyItem, TValue value, PrefixTreeNode<TKeyItem, TValue> parent, bool wordends)
			: this(0, keyItem, value, parent, wordends)
		{
		}

		public PrefixTreeNode(int index, TKeyItem keyItem, TValue value, PrefixTreeNode<TKeyItem, TValue> parent, bool wordends)
			: base(index, keyItem, value)
		{
			_parent = parent;
			AWordEndsHere = wordends;
		}

		#endregion

		public PrefixTreeNode<TKeyItem, TValue> GetSubTree(TKeyItem keyItem)
		{
			if (SubTrees.Count != 0)
				foreach (var node in SubTrees)
					if (node.Key.CompareTo(keyItem) == 0)
						return node;
			return null;
		}

		#region IEnumerable

		public IEnumerator<PrefixTreeNode<TKeyItem, TValue>> GetEnumerator() => new PreOrderEnumerator(this);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}

	public class PrefixTree<TKey, TKeyItem, TValue>
		where TKey : IEnumerable<TKeyItem>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Fields

		PrefixTreeNode<TKeyItem, TValue> _root;
		int _count;
		readonly List<List<PrefixTreeNode<TKeyItem, TValue>>> _subsequentItems;

		#endregion

		#region Properties

		public PrefixTreeNode<TKeyItem, TValue> Root
		{
			get { return _root; }
			set { _root = value; }
		}

		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		#endregion

		#region Constructors

		public PrefixTree()
		{
			_root = new PrefixTreeNode<TKeyItem, TValue>(default(TKeyItem), default(TValue), null, false);
			_subsequentItems = new List<List<PrefixTreeNode<TKeyItem, TValue>>>();
		}

		#endregion

		#region Methods

		public virtual PrefixTreeNode<TKeyItem, TValue> NewNode(TKeyItem keyItem, TValue value, PrefixTreeNode<TKeyItem, TValue> parent, bool wordends) => 
			new PrefixTreeNode<TKeyItem, TValue>(keyItem, value, parent, wordends);

		public virtual PrefixTreeNode<TKeyItem, TValue> Add(TKey key, TValue value) => Add(_root, key.ToList(), value, 0);

		private PrefixTreeNode<TKeyItem, TValue> Add(PrefixTreeNode<TKeyItem, TValue> node, List<TKeyItem> keyItems, TValue value, int matchedItems)
		{
			if (keyItems.Count == 0)
				return null;

			var currentNode = node;
			while (matchedItems < keyItems.Count)
			{
				bool isFound = false;
				foreach (var subTree in currentNode.SubTrees)
				{
					if (subTree.Key.CompareTo(keyItems[matchedItems]) == 0)
					{
						matchedItems++;
						isFound = true;
						currentNode = subTree;
						break;
					}
				}

				if (!isFound)
				{
					var newNode = NewNode(keyItems[matchedItems], default(TValue), currentNode, false);
					int count = currentNode.SubTrees.Count(subTree => subTree.Key.CompareTo(newNode.Key) <= 0);
					currentNode.SubTrees.Insert(count, newNode);
					matchedItems++;

					currentNode = newNode;
				}
			}

			currentNode.Value = value;
			currentNode.AWordEndsHere = true;
			return currentNode;
		}

		public bool Contains(TKey key)
		{
			return Contains(_root, key.ToList());
		}

		public bool Contains(PrefixTreeNode<TKeyItem, TValue> node, List<TKeyItem> keyItems)
		{
			var currentNode = node;
			for (int counter = 0; counter < keyItems.Count; counter++)
			{
				if(node != _root && counter == 0)
				{
					if (node.Key.CompareTo(keyItems[counter]) != 0)
						return false;
					continue;
				}
				var child = currentNode.GetSubTree(keyItems[counter]);
				if (child == null)
					return false;
				currentNode = child;
			}

			return true;
		}

		public List<List<PrefixTreeNode<TKeyItem, TValue>>> GetMatches(TKey key)
		{
			return GetMatches(key.ToList());
		}

		private List<List<PrefixTreeNode<TKeyItem, TValue>>> GetMatches(List<TKeyItem> queryItems)
		{
			var previous = new List<PrefixTreeNode<TKeyItem, TValue>>();
			PrefixTreeNode<TKeyItem, TValue> currentNode = _root;
			for (int counter = 0; counter < queryItems.Count; counter++)
			{
				if (counter < queryItems.Count && currentNode != _root)
					previous.Add(currentNode);

				var child = currentNode.GetSubTree(queryItems[counter]);
				if (child == null)
					break;
				currentNode = child;
			}

			_subsequentItems.Clear();

			GenerateSubsequentItems(currentNode, previous);

			return _subsequentItems;
		}

		private void GenerateSubsequentItems(PrefixTreeNode<TKeyItem, TValue> node, List<PrefixTreeNode<TKeyItem, TValue>> subsequentItems)
		{
			if (node == null)
				return;

			if (node != _root)
				subsequentItems.Add(node);

			if (node.AWordEndsHere)
				_subsequentItems.Add(subsequentItems);

			foreach (var subnode in node.SubTrees)
			{
				var cloneSubsequentItems = new List<PrefixTreeNode<TKeyItem, TValue>>();
				foreach (var item in subsequentItems)
					cloneSubsequentItems.Add(item);
				GenerateSubsequentItems(subnode, cloneSubsequentItems);
			}
		}

		#endregion

		public static PrefixTree<string, char, char> BuildHuffmanCode(char[] chars, int[] inputFreq)
		{
			int n = chars.Length;
			var binaryHeap = new BinaryHeap<int, PrefixTreeNode<char, char>>();
			for (int i = 0; i < n; i++)
			{
				((IMinHeap<int, PrefixTreeNode<char, char>>)binaryHeap).MinInsert(inputFreq[i], new PrefixTreeNode<char, char>(default(char), chars[i], null, true) { Frequency = inputFreq[i] });
			}
			var prefixTree = new PrefixTree<string, char, char>();
			for (int i = 0; i < n - 1; i++)
			{
				var x = ((IMinHeap<int, PrefixTreeNode<char, char>>)binaryHeap).ExtractMin().Value;
				x.Key = '0';
				var y = ((IMinHeap<int, PrefixTreeNode<char, char>>)binaryHeap).ExtractMin().Value;
				y.Key = '1';
				var z = new PrefixTreeNode<char, char>(default(char), default(char), null, false) { Frequency = x.Frequency + y.Frequency };
				z.SubTrees.Add(x);
				z.SubTrees.Add(y);
				((IMinHeap<int, PrefixTreeNode<char, char>>)binaryHeap).MinInsert(z.Frequency, z);
			}
			var root = ((IMinHeap<int, PrefixTreeNode<char, char>>)binaryHeap).ExtractMin().Value;
			prefixTree.Root = root;
			return prefixTree;
		}
	}
}
