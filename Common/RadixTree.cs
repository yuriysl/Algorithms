using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Common
{
	public class RadixSearchResult
	{
		public int Result;
		public int MatchedItems;
		public RadixSearchResult(int result, int matchedItems)
		{
			Result = result;
			MatchedItems = matchedItems;
		}
	}

	public class RadixTreeNode<TKey, TKeyItem, TValue> : BaseNode<TKey, TValue>, IEnumerable<RadixTreeNode<TKey, TKeyItem, TValue>>
		where TKey : IEnumerable<TKeyItem>, IComparable<TKey>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Nested

		public class PreOrderEnumerator : IEnumerator<RadixTreeNode<TKey, TKeyItem, TValue>>
		{
			private readonly RadixTreeNode<TKey, TKeyItem, TValue> _rootNode;
			private Stack<IEnumerator<RadixTreeNode<TKey, TKeyItem, TValue>>> _iterators;
			private bool _initialized;

			public PreOrderEnumerator(RadixTreeNode<TKey, TKeyItem, TValue> rootNode)
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

				if (_iterators.Count > 0 && !_iterators.Peek().MoveNext())
				{
					_iterators.Pop();
					_iterators.Peek().MoveNext();
				}

				return _iterators.Count > 0;
			}

			private void First()
			{
				_iterators = new Stack<IEnumerator<RadixTreeNode<TKey, TKeyItem, TValue>>>();
				var i = _rootNode.GetEnumerator();
				i.MoveNext();
				_iterators.Push(i);
				_initialized = true;
			}

			public void Reset()
			{
				_initialized = false;
			}

			public RadixTreeNode<TKey, TKeyItem, TValue> Current => _iterators.Peek().Current;

			object IEnumerator.Current => Current;

			public void Dispose()
			{
			}
		}

		#endregion

		#region Fields

		List<RadixTreeNode<TKey, TKeyItem, TValue>> _subTrees;
		RadixTreeNode<TKey, TKeyItem, TValue> _parent;
		List<TKeyItem> _keyItems;
		public bool AWordEndsHere;

		#endregion

		#region Properties

		public RadixTreeNode<TKey, TKeyItem, TValue> Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public List<RadixTreeNode<TKey, TKeyItem, TValue>> SubTrees
		{
			get
			{
				if (_subTrees == null)
					_subTrees = new List<RadixTreeNode<TKey, TKeyItem, TValue>>();
				return _subTrees;
			}
			set { _subTrees = value; }
		}

		public List<TKeyItem> KeyItems
		{
			get
			{
				if (_keyItems == null)
					_keyItems = new List<TKeyItem>();
				return _keyItems;
			}
			set { _keyItems = value; }
		}

		#endregion

		#region Constructors

		public RadixTreeNode(List<TKeyItem> keyItems, TValue value, RadixTreeNode<TKey, TKeyItem, TValue> parent)
			: this(0, keyItems, value, parent)
		{
		}

		public RadixTreeNode(int index, List<TKeyItem> keyItems, TValue value, RadixTreeNode<TKey, TKeyItem, TValue> parent)
			: base(index, default(TKey), value)
		{
			_parent = parent;
			_keyItems = keyItems;
		}

		#endregion

		public RadixTreeNode<TKey, TKeyItem, TValue> GetSubTree(TKeyItem keyItem)
		{
			if (SubTrees.Count != 0)
				foreach (var node in SubTrees)
					if (node.KeyItems[0].CompareTo(keyItem) == 0)
						return node;
			return null;
		}

		#region IEnumerable

		public IEnumerator<RadixTreeNode<TKey, TKeyItem, TValue>> GetEnumerator() => new PreOrderEnumerator(this);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}

	public class RadixTree<TKey, TKeyItem, TValue>
		where TKey : IEnumerable<TKeyItem>, IComparable<TKey>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Fields

		RadixTreeNode<TKey, TKeyItem, TValue> _root;
		int _count;
		private List<List<RadixTreeNode<TKey, TKeyItem, TValue>>> _subsequentItems;

		#endregion

		#region Properties

		public RadixTreeNode<TKey, TKeyItem, TValue> Root
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

		public RadixTree()
		{
			_root = new RadixTreeNode<TKey, TKeyItem, TValue>(null, default(TValue), null);
			_subsequentItems = new List<List<RadixTreeNode<TKey, TKeyItem, TValue>>>();
		}

		#endregion

		#region Methods

		public virtual RadixTreeNode<TKey, TKeyItem, TValue> NewNode(List<TKeyItem> keyItems, TValue value, RadixTreeNode<TKey, TKeyItem, TValue> parent) =>
			new RadixTreeNode<TKey, TKeyItem, TValue>(keyItems, value, parent);

		public virtual RadixTreeNode<TKey, TKeyItem, TValue> Add(TKey key, TValue value) => Add(_root, key.ToList(), value);

		private RadixTreeNode<TKey, TKeyItem, TValue> Add(RadixTreeNode<TKey, TKeyItem, TValue> node, List<TKeyItem> addItems, TValue value)
		{
			if (addItems.Count == 0)
				return null;

			var currentNode = node;
			int startIndex = 0;
			var keyItems = node.KeyItems;
			var searchResult = LookUp(keyItems, addItems, startIndex);

			while (searchResult.Result != 0)
			{
				startIndex += searchResult.MatchedItems;
				if (keyItems.Count != searchResult.MatchedItems)
				{
					var commonKeyItems = new List<TKeyItem>();
					var branchKeyItems = new List<TKeyItem>();
					for (int i = 0; i < keyItems.Count; i++)
					{
						if(i < searchResult.MatchedItems)
							commonKeyItems.Add(keyItems[i]);
						else
							branchKeyItems.Add(keyItems[i]);
					}
					currentNode.KeyItems = commonKeyItems;
					var newNode = NewNode(branchKeyItems, currentNode.Value, currentNode);
					newNode.Value = currentNode.Value;
					newNode.SubTrees.AddRange(currentNode.SubTrees);
					newNode.AWordEndsHere = true;
					currentNode.Value = default(TValue);
					currentNode.SubTrees.Clear();
					currentNode.AWordEndsHere = false;
					int count = currentNode.SubTrees.Count(subTree => LookUp(subTree.KeyItems, newNode.KeyItems, 0).Result <= 0);
					currentNode.SubTrees.Insert(count, newNode);
					break;
				}

				var child = currentNode.GetSubTree(addItems[startIndex]);
				if (child == null)
					break;

				currentNode = child;
				keyItems = currentNode.KeyItems;
				searchResult = LookUp(keyItems, addItems, startIndex);
			}

			if(startIndex < addItems.Count)
			{
				var newKeyItems = new List<TKeyItem>();
				for (int i = startIndex; i < addItems.Count; i++)
					newKeyItems.Add(addItems[i]);
				var newNode = NewNode(newKeyItems, default(TValue), currentNode);
				int count = currentNode.SubTrees.Count(subTree => LookUp(subTree.KeyItems, newNode.KeyItems, 0).Result <= 0);
				currentNode.SubTrees.Insert(count, newNode);
				currentNode = newNode;
			}

			currentNode.Value = value;
			currentNode.AWordEndsHere = true;
			return currentNode;
		}

		public bool Contains(TKey key) => Contains(_root, key.ToList());

		public bool Contains(RadixTreeNode<TKey, TKeyItem, TValue> node, List<TKeyItem> queryItems)
		{
			var currentNode = node;
			int startIndex = 0;
			var keyItems = node.KeyItems;
			var searchResult = LookUp(keyItems, queryItems, startIndex);
			if (searchResult.Result == 0)
				return true;

			while (searchResult.Result != 0)
			{
				startIndex += searchResult.MatchedItems;
				if (keyItems.Count != searchResult.MatchedItems)
					return false;

				var child = currentNode.GetSubTree(queryItems[startIndex]);
				if (child == null)
					return false;
				currentNode = child;
				keyItems = currentNode.KeyItems;
				searchResult = LookUp(keyItems, queryItems, startIndex);
				if (searchResult.Result == 0)
					return true;
			}

			return false;
		}

		private RadixSearchResult LookUp(List<TKeyItem> keyItems, List<TKeyItem> queryItems, int startIndex)
		{
			int p = keyItems.Count;
			int r = queryItems.Count - startIndex;
			int q = Math.Min(p, r);
			int matchedItems = 0;
			for (int i = 0; i < q; i++)
			{
				int compareItemResult = keyItems[i].CompareTo(queryItems[startIndex + i]);
				if (compareItemResult == 0)
					matchedItems++;
				else
					return new RadixSearchResult(compareItemResult, matchedItems);
			}
			return new RadixSearchResult(p.CompareTo(r), matchedItems);
		}

		public List<List<RadixTreeNode<TKey, TKeyItem, TValue>>> GetMatches(TKey key)
		{
			return GetMatches(key.ToList());
		}

		private List<List<RadixTreeNode<TKey, TKeyItem, TValue>>> GetMatches(List<TKeyItem> queryItems)
		{
			var previous = new List<RadixTreeNode<TKey, TKeyItem, TValue>>();
			RadixTreeNode<TKey, TKeyItem, TValue> currentNode = _root;
			int matchedItems = 0;
			for (int counter = 0; counter < queryItems.Count; counter += matchedItems)
			{
				if (counter < queryItems.Count && currentNode != _root)
					previous.Add(currentNode);

				var child = currentNode.GetSubTree(queryItems[counter]);
				if (child == null)
					break;
				var keyItems = child.KeyItems;
				var searchResult = LookUp(keyItems, queryItems, counter);
				if (searchResult.Result == 0)
				{
					currentNode = child;
					break;
				}
				if (keyItems.Count != searchResult.MatchedItems)
					break;
				currentNode = child;
				matchedItems = searchResult.MatchedItems;
			}

			_subsequentItems.Clear();

			GenerateSubsequentItems(currentNode, previous);

			return _subsequentItems;
		}

		private void GenerateSubsequentItems(RadixTreeNode<TKey, TKeyItem, TValue> node, List<RadixTreeNode<TKey, TKeyItem, TValue>> subsequentItems)
		{
			if (node == null)
				return;

			subsequentItems.Add(node);

			if (node.AWordEndsHere)
				_subsequentItems.Add(subsequentItems);

			foreach (var subnode in node.SubTrees)
			{
				var cloneSubsequentItems = new List<RadixTreeNode<TKey, TKeyItem, TValue>>();
				foreach (var item in subsequentItems)
					cloneSubsequentItems.Add(item);
				GenerateSubsequentItems(subnode, cloneSubsequentItems);
			}
		}

		public void Print(RadixTreeNode<string, char, object> node)
		{
			Console.WriteLine("Key:{0}", string.Join("", node.KeyItems));
			Console.Write("Count:{0}", node.SubTrees.Count);
			for (int i = 0; i < node.SubTrees.Count; i++)
			{
				Print(node.SubTrees[i]);
			}
		}

		#endregion
	}
}
