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

		#endregion

		#region Properties

		public NodeColor Color
		{
			get { return _color; }
			set { _color = value; }
		}

		#endregion

		#region Constructors

		public RBNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent)
			: this(key, value, parent, NodeColor.Red)
		{
		}

		public RBNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent, NodeColor color)
			: base(key, value, parent)
		{
			_color = color;
		}

		#endregion
	}

	public class RBBinaryTree<TKey, TValue> : BinaryTree<TKey, TValue> where TKey : IComparable<TKey>
	{
		#region Methods

		public override BinaryTreeNode<TKey, TValue> NewNode(TKey key, TValue value, BinaryTreeNode<TKey, TValue> parent)
		{
			return new RBNode<TKey, TValue>(key, value, parent);
		}

		public void LeftRotate(RBNode<TKey, TValue> node)
		{
			var y = node.Right;
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
			node.Parent = y;
		}

		public void RightRotate(RBNode<TKey, TValue> node)
		{
			var y = node.Left;
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
			node.Parent = y;
		}

		public override BinaryTreeNode<TKey, TValue> Add(TKey key, TValue value)
		{
			var newNode = base.Add(key, value);

			AddFixUp(ref newNode);

			return newNode;
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

			((RBNode<TKey, TValue>)node).Color = NodeColor.Black;
		}

		#endregion
	}
}
