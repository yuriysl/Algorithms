using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Common
{
	public class SetNode<T>
	{
		T _value;
		SetNode<T> _head;
		SetNode<T> _next;

		public T Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public SetNode<T> Head
		{
			get { return _head; }
			set { _head = value; }
		}

		public SetNode<T> Next
		{
			get { return _next; }
			set { _next = value; }
		}

		public SetNode(T value)
		{
			_value = value;
		}
	}

	public class Set<T>
	{
		#region Fields

		SetNode<T> _head;
		SetNode<T> _tail;
		int _count;

		#endregion

		#region Properties

		public SetNode<T> Head
		{
			get { return _head; }
			set { _head = value; }
		}

		public SetNode<T> Tail
		{
			get { return _tail; }
			set { _tail = value; }
		}

		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		#endregion

		#region Constructors

		public Set()
		{
		}

		public Set(T value)
		{
			_head = new SetNode<T>(value);
			_head.Head = _head;
			_tail = _head;
			_count = 1;
		}

		#endregion

		#region Methods

		public Set<T> Union(Set<T> other)
		{
			var unionSet = new Set<T>();

			if (other.Head == null)
			{
				unionSet.Head = _head;
				unionSet.Tail = _tail;
				unionSet.Count = _count;
				return unionSet;
			}
			else if (_head == null)
			{
				unionSet.Head = other.Head;
				unionSet.Tail = other.Tail;
				unionSet.Count = other.Count;
				return unionSet;
			}
			else if(_count >= other.Count)
			{
				var updatedNode = other.Head;
				while (updatedNode != null)
				{
					updatedNode.Head = _head;
					updatedNode = updatedNode.Next;
				}

				unionSet.Head = _head;
				_tail.Next = other.Head;
				unionSet.Tail = other.Tail;
				unionSet.Count = _count + other.Count;
				return unionSet;
			}
			else
			{
				var updatedNode = _head;
				while (updatedNode != null)
				{
					updatedNode.Head = other.Head;
					updatedNode = updatedNode.Next;
				}

				unionSet.Head = other.Head;
				other.Tail.Next = _head;
				unionSet.Tail = _tail;
				unionSet.Count = _count + other.Count;
				return unionSet;
			}
		}

		#endregion
	}
}
