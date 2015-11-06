using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class PrefixTreeNode<TKeyItem, TValue> : BaseNode<TKeyItem, TValue>
		where TKeyItem : IComparable<TKeyItem>
	{
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

		public PrefixTreeNode<TKeyItem, TValue> GetChild(TKeyItem keyItem)
		{
			if (SubTrees.Count != 0)
				foreach (var node in SubTrees)
					if (node.Key.CompareTo(keyItem) == 0)
						return node;
			return null;
		}
	}

	public class PrefixTree<TKey, TKeyItem, TValue> : IEnumerable<PrefixTreeNode<TKeyItem, TValue>> 
		where TKey : IEnumerable<TKeyItem>
		where TKeyItem : IComparable<TKeyItem>
	{
		#region Nested

		public class PreOrderEnumerator : IEnumerator<PrefixTreeNode<TKeyItem, TValue>>
		{
			private readonly PrefixTree<TKey, TKeyItem, TValue> _prefixTree;
			private PrefixTreeNode<TKeyItem, TValue> _current;
			private bool _initialized;

			public PreOrderEnumerator(PrefixTree<TKey, TKeyItem, TValue> prefixTree)
			{
				_prefixTree = prefixTree;
			}

			public bool MoveNext()
			{
				if (!_initialized)
				{
					First();
					return _current != null;
				}

				
				return _current != null;
			}

			private void First()
			{
				
				_initialized = true;
			}

			public void Reset()
			{
				_current = null;
				_initialized = false;
			}

			public PrefixTreeNode<TKeyItem, TValue> Current
			{
				get { return _current; }
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

		private PrefixTreeNode<TKeyItem, TValue> Add(PrefixTreeNode<TKeyItem, TValue> node, List<TKeyItem> keyItems, TValue value, int matched)
		{
			if (keyItems.Count == 0)
				return null;
			if (matched == keyItems.Count)
				return node;
			foreach (var subTree in node.SubTrees)
			{
				if (subTree.Key.CompareTo(keyItems[matched]) == 0)
				{
					matched++;
					return Add(subTree, keyItems, value, matched);
				}
			}

			var newNode = NewNode(keyItems[matched], value, node, false);
			node.SubTrees.Add(newNode);
			matched++;

			if (matched == keyItems.Count)
			{
				newNode.AWordEndsHere = true;
				return newNode;
			}
			else
				return Add(newNode, keyItems, value, matched);
		}

		public bool Contains(TKey key)
		{
			return Contains(_root, key.ToList());
		}

		public bool Contains(PrefixTreeNode<TKeyItem, TValue> node, List<TKeyItem> keyItems)
		{
			var currentNode = node;
			PrefixTreeNode<TKeyItem, TValue> child = null;
			for (int counter = 0; counter < keyItems.Count; counter++)
			{
				child = currentNode.GetChild(keyItems[counter]);
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
			PrefixTreeNode<TKeyItem, TValue> child = null;
			for (int counter = 0; counter < queryItems.Count; counter++)
			{
				if (counter < queryItems.Count - 1)
					previous.Add(queryItems[counter]);

				child = currentNode.GetChild(queryItems[counter]);
				if (child == null)
					break;
				currentNode = child;
			}

			_subsequentItems.Clear();

			GenerateSubsequentItems(currentNode, previous);

			return _subsequentItems;
		}

		private void GenerateSubsequentItems(PrefixTreeNode<TKeyItem, TValue> node,
														List<TKeyItem> subsequentItems)
		{
			if (node == null)
				return;

			var cloneSubsequentItems = new List<TKeyItem>();
			foreach (var item in subsequentItems)
			{
				cloneSubsequentItems.Add(item);
			}

			if (node.AWordEndsHere)
			{
				_subsequentItems.Add(cloneSubsequentItems);
				//return;
			}

			foreach (var subnode in node.SubTrees)
				GenerateSubsequentItems(subnode, cloneSubsequentItems);
		}

		#endregion

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
}
