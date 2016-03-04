using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Common
{
	public class BinaryTreeNode<TKey, TValue> : BaseNode<TKey, TValue>
	{
		#region Fields

		BinaryTreeNode<TKey, TValue> _left;
		BinaryTreeNode<TKey, TValue> _right;
		BinaryTreeNode<TKey, TValue> _parent;

		#endregion

		#region Properties

		public BinaryTreeNode<TKey, TValue> Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public BinaryTreeNode<TKey, TValue> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		public BinaryTreeNode<TKey, TValue> Right
		{
			get { return _right; }
			set { _right = value; }
		}

		#endregion

		#region Constructors

		public BinaryTreeNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent)
			: this(0, key, value, parent)
		{
		}

		private BinaryTreeNode(int index, TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent)
			: base(index, key, value)
		{
			_parent = parent;
		}

		#endregion
	}

	public class BinaryTree<TKey, TValue> : IEnumerable<BinaryTreeNode<TKey, TValue>> where TKey : IComparable<TKey>
	{
		#region Nested

		private class InOrderEnumerator : IEnumerator<BinaryTreeNode<TKey, TValue>>
		{
			private readonly BinaryTree<TKey, TValue> _binaryTree;
			private BinaryTreeNode<TKey, TValue> _current;
			private bool _initialized;

			public InOrderEnumerator(BinaryTree<TKey, TValue> binaryTree)
			{
				_binaryTree = binaryTree;
			}

			public bool MoveNext()
			{
				if (!_initialized)
				{
					First();
					return _current != null;
				}

				if (_current == null)
					return false;

				if (_current.Right != null)
				{
					_current = _binaryTree.GetMinNode(_current.Right);
					return true;
				}

				while (_current.Parent != null && _current.Parent.Right == _current)
					_current = _current.Parent;

				_current = _current.Parent;
				return _current != null;
			}

			private void First()
			{
				if (_binaryTree.Root != null)
					_current = _binaryTree.GetMinNode(_binaryTree.Root);
				_initialized = true;
			}

			public void Reset()
			{
				_current = null;
				_initialized = false;
			}

			public BinaryTreeNode<TKey, TValue> Current => _current;

			object IEnumerator.Current => Current;

			public void Dispose()
			{
			}
		}

		private class InOrderStackEnumerator : IEnumerator<BinaryTreeNode<TKey, TValue>>
		{
			private readonly BinaryTree<TKey, TValue> _binaryTree;
			private readonly Stack<BinaryTreeNode<TKey, TValue>> _currentPath;
			private bool _initialized;

			public InOrderStackEnumerator(BinaryTree<TKey, TValue> binaryTree)
			{
				_binaryTree = binaryTree;
				_currentPath = new Stack<BinaryTreeNode<TKey, TValue>>();
			}

			public bool MoveNext()
			{
				if (!_initialized)
				{
					First();
					return Current != null;
				}

				if (Current == null)
					return false;

				var current = _currentPath.Pop().Right;
				while (current != null)
				{
					_currentPath.Push(current);
					current = current.Left;
				}

				return Current != null;
			}

			private void First()
			{
				_currentPath.Clear();
				if (_binaryTree.Root != null)
				{
					var current = _binaryTree.Root;
					while (current != null)
					{
						_currentPath.Push(current);
						current = current.Left;
					}
				}
				_initialized = true;
			}

			public void Reset()
			{
				_currentPath.Clear();
				_initialized = false;
			}

			public BinaryTreeNode<TKey, TValue> Current => _currentPath.Count == 0 ? null : _currentPath.Peek();

			object IEnumerator.Current => Current;

			public void Dispose()
			{
			}
		}

		#endregion

		#region Fields

		BinaryTreeNode<TKey, TValue> _root;
		int _count;

		#endregion

		#region Properties

		public BinaryTreeNode<TKey, TValue> Root
		{
			get { return _root; }
			set { _root = value; }
		}

		public BinaryTreeNode<TKey, TValue> Min => GetMinNode(_root);

		public BinaryTreeNode<TKey, TValue> Max => GetMaxNode(_root);

		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		#endregion

		#region Methods

		protected virtual BinaryTreeNode<TKey, TValue> NewNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent) => 
			new BinaryTreeNode<TKey, TValue>(key, value, parent);

		public virtual BinaryTreeNode<TKey, TValue> Add(TKey key, TValue value)
		{
			BinaryTreeNode<TKey, TValue> newNode;
			Add(ref _root, null, key, value, out newNode);
			return newNode;
		}

		protected virtual void DoAddNode(BinaryTreeNode<TKey, TValue> newNode)
		{
		}

		private void Add(ref BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> parent, TKey key, TValue value, out BinaryTreeNode<TKey, TValue> newNode)
		{
			if (node == null)
			{
				node = NewNode(key, value, parent);
				newNode = node;
				_count++;
				DoAddNode(node);
				return;
			}

			if (((IComparable<TKey>)node.Key).CompareTo(key) > 0)
			{
				var leftNode = node.Left;
				DoAddNode(node);
				Add(ref leftNode, node, key, value, out newNode);
				node.Left = leftNode;
				newNode = leftNode;
			}
			else if (((IComparable<TKey>)node.Key).CompareTo(key) < 0)
			{
				var rightNode = node.Right;
				DoAddNode(node);
				Add(ref rightNode, node, key, value, out newNode);
				node.Right = rightNode;
				newNode = rightNode;
			}
			else
			{
				int rndR = NodeHelper<TKey, TValue>.Rnd.Next(2);
				var childNode = rndR == 0 ? node.Left : node.Right;
				DoAddNode(node);
				Add(ref childNode, node, key, value, out newNode);
				node.Right = childNode;
				newNode = childNode;
			}
		}

		public bool Contains(TKey key) => Contains(_root, key);

		private bool Contains(BinaryTreeNode<TKey, TValue> node, TKey key)
		{
			if (node == null)
				return false;
			if (((IComparable<TKey>)node.Key).CompareTo(key) == 0)
				return true;
			if (((IComparable<TKey>)node.Key).CompareTo(key) > 0)
				return Contains(node.Left, key);
			if (((IComparable<TKey>)node.Key).CompareTo(key) < 0)
				return Contains(node.Right, key);
			return false;
		}

		private BinaryTreeNode<TKey, TValue> GetMinNode(BinaryTreeNode<TKey, TValue> node)
		{
			var currentNode = node;
			while (currentNode.Left != null)
				currentNode = currentNode.Left;
			return currentNode;
		}

		private BinaryTreeNode<TKey, TValue> GetMaxNode(BinaryTreeNode<TKey, TValue> node)
		{
			var currentNode = node;
			while (currentNode.Right != null)
				currentNode = currentNode.Right;
			return currentNode;
		}

		public virtual BinaryTreeNode<TKey, TValue> Remove(TKey key, out BinaryTreeNode<TKey, TValue> replacedNode)
		{
			BinaryTreeNode<TKey, TValue> removedNode;
			Remove(_root, key, out removedNode, out replacedNode);
			return removedNode;
		}

		protected virtual void DoRemoveNode(BinaryTreeNode<TKey, TValue> node)
		{
		}

		private void Remove(BinaryTreeNode<TKey, TValue> node, TKey key, out BinaryTreeNode<TKey, TValue> removedNode, out BinaryTreeNode<TKey, TValue> replacedNode)
		{
			if (node == null)
			{
				removedNode = null;
				replacedNode = null;
				return;
			}
			if (((IComparable<TKey>)node.Key).CompareTo(key) > 0)
			{
				DoRemoveNode(node);
				Remove(node.Left, key, out removedNode, out replacedNode);
			}
			else if (((IComparable<TKey>)node.Key).CompareTo(key) < 0)
			{
				DoRemoveNode(node);
				Remove(node.Right, key, out removedNode, out replacedNode);
			}
			else
			{
				if (node.Left != null && node.Right != null)
				{
					var successor = GetMinNode(node.Right);
					node.Value = successor.Value;
					Remove(successor, successor.Key, out removedNode, out replacedNode);
				}
				else if (node.Left != null)
				{
					removedNode = node;
					replacedNode = node.Left;
					ReplaceInParent(node, node.Left);
					_count--;
				}
				else if (node.Right != null)
				{
					removedNode = node;
					replacedNode = node.Right;
					ReplaceInParent(node, node.Right);
					_count--;
				}
				else
				{
					removedNode = node;
					replacedNode = null;
					ReplaceInParent(node, null);
					_count--;
				}
			}
		}

		private void ReplaceInParent(BinaryTreeNode<TKey, TValue> node, BinaryTreeNode<TKey, TValue> newNode)
		{
			if (node.Parent != null)
			{
				if (node == node.Parent.Left)
					node.Parent.Left = newNode;
				else
					node.Parent.Right = newNode;
			}
			else
				_root = newNode;

			if (newNode != null)
				newNode.Parent = node.Parent;
		}

		#endregion

		#region IEnumerable

		public IEnumerator<BinaryTreeNode<TKey, TValue>> GetEnumerator() => new InOrderEnumerator(this);

		public IEnumerator<BinaryTreeNode<TKey, TValue>> GetInOrderStackEnumerator() => new InOrderStackEnumerator(this);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}
}
