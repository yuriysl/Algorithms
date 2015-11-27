using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public enum NodeColor
	{
		Red = 0,
		Black = 1
	}

	public class RBNode<TKey, TValue> : BinaryTreeNode<TKey, TValue>
	{
		#region Fields

		NodeColor _color;
		int _size;

		#endregion

		#region Properties

		public NodeColor Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public int Size
		{
			get { return _size; }
			set { _size = value; }
		}

		#endregion

		#region Constructors

		public RBNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent, int size)
			: this(key, value, parent, NodeColor.Red, size)
		{
		}

		public RBNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent, NodeColor color, int size)
			: base(key, value, parent)
		{
			_color = color;
			_size = size;
		}

		#endregion
	}

	public class RBBinaryTree<TKey, TValue> : BinaryTree<TKey, TValue> where TKey : IComparable<TKey>
	{
		#region Methods

		public override BinaryTreeNode<TKey, TValue> NewNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent)
		{
			return new RBNode<TKey, TValue>(key, value, parent, 0);
		}

		public void LeftRotate(RBNode<TKey, TValue> node)
		{
			var y = (RBNode<TKey, TValue>)node.Right;
			node.Right = y.Left;

			if (y.Left != null)
				y.Left.Parent = node;

			y.Parent = node.Parent;

			if (node.Parent == null)
				this.Root = y;
			else if (node.Parent.Left == node)
				node.Parent.Left = y;
			else
				node.Parent.Right = y;

			y.Left = node;
			y.Size = node.Size;
			var left = (RBNode<TKey, TValue>)node.Left;
			var right = (RBNode<TKey, TValue>)node.Right;
			node.Size = (left == null ? 0 : left.Size) + (right == null ? 0 : right.Size) + 1;
			node.Parent = y;
		}

		public void RightRotate(RBNode<TKey, TValue> node)
		{
			var y = (RBNode<TKey, TValue>)node.Left;
			node.Left = y.Right;

			if (y.Right != null)
				y.Right.Parent = node;

			y.Parent = node.Parent;

			if (node.Parent == null)
				this.Root = y;
			else if (node.Parent.Right == node)
				node.Parent.Right = y;
			else
				node.Parent.Left = y;

			y.Right = node;
			y.Size = node.Size;
			var left = (RBNode<TKey, TValue>)node.Left;
			var right = (RBNode<TKey, TValue>)node.Right;
			node.Size = (left == null ? 0 : left.Size) + (right == null ? 0 : right.Size) + 1;
			node.Parent = y;
		}

		public RBNode<TKey, TValue> Select(RBNode<TKey, TValue> node, int index)
		{
			if (node == null)
				return null;
			var left = (RBNode<TKey, TValue>)node.Left;
			int r = (left == null ? 0 : left.Size) + 1;
			if (index == r)
				return node;
			else if (index < r)
				return Select(left, index);

			var right = (RBNode<TKey, TValue>)node.Right;
			return Select(right, index - r);
		}

		public RBNode<TKey, TValue> SelectTail(RBNode<TKey, TValue> node, int index)
		{
			if (node == null)
				return null;
			var currentNode = node;
			var left = (RBNode<TKey, TValue>)currentNode.Left;
			int r = (left == null ? 0 : left.Size) + 1;
			while (index != r)
			{
				if (index < r)
					currentNode = (RBNode<TKey, TValue>)currentNode.Left;
				else
				{
					currentNode = (RBNode<TKey, TValue>)currentNode.Right;
					index -= r;
				}
				if (currentNode == null)
					return null;

				left = (RBNode<TKey, TValue>)currentNode.Left;
				r = (left == null ? 0 : left.Size) + 1;
			}
			return currentNode;
		}

		public int Rank(RBNode<TKey, TValue> node)
		{
			if (node == null)
				return 0;
			var currentNode = node;
			var left = (RBNode<TKey, TValue>)currentNode.Left;
			int r = (left == null ? 0 : left.Size) + 1;
			while (currentNode != Root)
			{
				if (currentNode.Parent.Right == currentNode)
				{
					left = (RBNode<TKey, TValue>)currentNode.Parent.Left;
					r += (left == null ? 0 : left.Size) + 1;
				}
				currentNode = (RBNode<TKey, TValue>)currentNode.Parent;
				
			}
			return r;
		}

		public override BinaryTreeNode<TKey, TValue> Add(TKey key, TValue value)
		{
			var newNode = base.Add(key, value);

			AddFixUp(ref newNode);

			return newNode;
		}

		public override void DoAddNode(BinaryTreeNode<TKey, TValue> newNode)
		{
			if (newNode == null)
				return;
			var newRBNode = (RBNode<TKey, TValue>)newNode;
			newRBNode.Size++;
		}

		private void AddFixUp(ref BinaryTreeNode<TKey, TValue> node)
		{
			while (node.Parent != null && ((RBNode<TKey, TValue>)node.Parent).Color == NodeColor.Red)
			{
				if (node.Parent == node.Parent.Parent.Left)
				{
					var y = (RBNode<TKey, TValue>)node.Parent.Parent.Right;
					if (y != null && y.Color == NodeColor.Red)
					{
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						y.Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent.Parent).Color = NodeColor.Red;
						node = node.Parent.Parent;
					}
					else if (node == node.Parent.Right)
					{
						node = node.Parent;
						LeftRotate((RBNode<TKey, TValue>)node);
					}
					else
					{
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent.Parent).Color = NodeColor.Red;
						RightRotate((RBNode<TKey, TValue>)node.Parent.Parent);
					}
				}
				else
				{
					var y = (RBNode<TKey, TValue>)node.Parent.Parent.Left;
					if (y != null && y.Color == NodeColor.Red)
					{
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						y.Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent.Parent).Color = NodeColor.Red;
						node = node.Parent.Parent;
					}
					else if (node == node.Parent.Left)
					{
						node = node.Parent;
						RightRotate((RBNode<TKey, TValue>)node);
					}
					else
					{
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent.Parent).Color = NodeColor.Red;
						LeftRotate((RBNode<TKey, TValue>)node.Parent.Parent);
					}
				}
			}

			((RBNode<TKey, TValue>)Root).Color = NodeColor.Black;
		}

		public override BinaryTreeNode<TKey, TValue> Remove(TKey key, out BinaryTreeNode<TKey, TValue> replacedNode)
		{
			var removedNode = base.Remove(key, out replacedNode);
			if (((RBNode<TKey, TValue>)removedNode).Color == NodeColor.Black)
				RemoveFixUp(replacedNode);
			return removedNode;
		}

		public override void DoRemoveNode(BinaryTreeNode<TKey, TValue> node)
		{
			if (node == null)
				return;
			var rbNode = (RBNode<TKey, TValue>)node;
			rbNode.Size--;
		}

		private void RemoveFixUp(BinaryTreeNode<TKey, TValue> node)
		{
			while (node != Root && ((RBNode<TKey, TValue>)node).Color == NodeColor.Black)
			{
				if (node == node.Parent.Left)
				{
					var w = node.Parent.Right;
					if (((RBNode<TKey, TValue>)w).Color == NodeColor.Red)
					{
						((RBNode<TKey, TValue>)w).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Red;
						LeftRotate((RBNode<TKey, TValue>)node.Parent);
						w = node.Parent.Right;
					}
					if (((RBNode<TKey, TValue>)w.Left).Color == NodeColor.Black && ((RBNode<TKey, TValue>)w.Right).Color == NodeColor.Black)
					{
						((RBNode<TKey, TValue>)w).Color = NodeColor.Red;
						node = node.Parent;
					}
					else if (((RBNode<TKey, TValue>)w.Right).Color == NodeColor.Black)
					{
						((RBNode<TKey, TValue>)w.Left).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)w).Color = NodeColor.Red;
						RightRotate((RBNode<TKey, TValue>)w);
						w = node.Parent.Right;
					}
					else
					{
						((RBNode<TKey, TValue>)w).Color = ((RBNode<TKey, TValue>)node.Parent).Color;
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)w.Right).Color = NodeColor.Black;
						LeftRotate((RBNode<TKey, TValue>)node.Parent);
						node = Root;
					}
				}
				else
				{
					var w = node.Parent.Left;
					if (((RBNode<TKey, TValue>)w).Color == NodeColor.Red)
					{
						((RBNode<TKey, TValue>)w).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Red;
						RightRotate((RBNode<TKey, TValue>)node.Parent);
						w = node.Parent.Left;
					}
					if (((RBNode<TKey, TValue>)w.Right).Color == NodeColor.Black && ((RBNode<TKey, TValue>)w.Left).Color == NodeColor.Black)
					{
						((RBNode<TKey, TValue>)w).Color = NodeColor.Red;
						node = node.Parent;
					}
					else if (((RBNode<TKey, TValue>)w.Left).Color == NodeColor.Black)
					{
						((RBNode<TKey, TValue>)w.Right).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)w).Color = NodeColor.Red;
						LeftRotate((RBNode<TKey, TValue>)w);
						w = node.Parent.Left;
					}
					else
					{
						((RBNode<TKey, TValue>)w).Color = ((RBNode<TKey, TValue>)node.Parent).Color;
						((RBNode<TKey, TValue>)node.Parent).Color = NodeColor.Black;
						((RBNode<TKey, TValue>)w.Left).Color = NodeColor.Black;
						RightRotate((RBNode<TKey, TValue>)node.Parent);
						node = Root;
					}
				}
			}

			if(node != null)
				((RBNode<TKey, TValue>)node).Color = NodeColor.Black;
		}

		#endregion
	}
}
