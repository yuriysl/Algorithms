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

			public PrefixTreeNode<TKeyItem, TValue> Current
			{
				get { return _iterators.Peek().Current; }
			}

			object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}

			public void Dispose()
			{
			}
		}

		#endregion

		#region Fields

		List<PrefixTreeNode<TKeyItem, TValue>> _subTrees;
		PrefixTreeNode<TKeyItem, TValue> _parent;
		public bool AWordEndsHere;

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

		public IEnumerator<PrefixTreeNode<TKeyItem, TValue>> GetEnumerator()
		{
			return new PreOrderEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}

	public class PrefixTree<TKey, TKeyItem, TValue>
		where TKey : IEnumerable<TKeyItem>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Fields

		PrefixTreeNode<TKeyItem, TValue> _root;
		int _count;
		private List<List<TKeyItem>> _subsequentItems;

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
			_subsequentItems = new List<List<TKeyItem>>();
		}

		#endregion

		#region Methods

		public virtual PrefixTreeNode<TKeyItem, TValue> NewNode(TKeyItem keyItem, TValue value, PrefixTreeNode<TKeyItem, TValue> parent, bool wordends)
		{
			return new PrefixTreeNode<TKeyItem, TValue>(keyItem, value, parent, wordends);
		}

		public virtual PrefixTreeNode<TKeyItem, TValue> Add(TKey key, TValue value)
		{
			return Add(_root, key.ToList(), value, 0);
		}

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

		public List<List<TKeyItem>> GetMatches(TKey key)
		{
			return GetMatches(key.ToList());
		}

		private List<List<TKeyItem>> GetMatches(List<TKeyItem> queryItems)
		{
			var previous = new List<TKeyItem>();
			PrefixTreeNode<TKeyItem, TValue> currentNode = _root;
			for (int counter = 0; counter < queryItems.Count; counter++)
			{
				if (counter < queryItems.Count - 1)
					previous.Add(queryItems[counter]);

				var child = currentNode.GetSubTree(queryItems[counter]);
				if (child == null)
					break;
				currentNode = child;
			}

			_subsequentItems.Clear();

			GenerateSubsequentItems(currentNode, previous);

			return _subsequentItems;
		}

		private void GenerateSubsequentItems(PrefixTreeNode<TKeyItem, TValue> node, List<TKeyItem> subsequentItems)
		{
			if (node == null)
				return;

			if (node != _root)
				subsequentItems.Add(node.Key);

			if (node.AWordEndsHere)
				_subsequentItems.Add(subsequentItems);

			foreach (var subnode in node.SubTrees)
			{
				var cloneSubsequentItems = new List<TKeyItem>();
				foreach (var item in subsequentItems)
					cloneSubsequentItems.Add(item);
				GenerateSubsequentItems(subnode, cloneSubsequentItems);
			}
		}

		#endregion
	}
}
