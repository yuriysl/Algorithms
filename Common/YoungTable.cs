using System;
using System.Text;

namespace Algorithms.Common
{
	public class YoungTable<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		#region Fields

		private readonly int _hight;
		private readonly int _width;
		readonly BaseNode<TKey, TValue>[,] _table;

		#endregion

		#region Constructors

		public YoungTable(int m, int n)
		{
			_hight = m;
			_width = n;
			_table = new BaseNode<TKey, TValue>[m, n];
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			var tableBuilder = new StringBuilder();
			for (int i = 0; i < _hight; i++)
			{
				tableBuilder.AppendLine();
				for (int j = 0; j < _width; j++)
				{
					if (_table[i, j] != null)
					{
						var keyString = _table[i, j].Key.ToString();
						tableBuilder.AppendFormat("{0} |", keyString.PadLeft(6));
					}
					else
						tableBuilder.AppendFormat("{0} |", "  Null");
				}
			}
			return tableBuilder.ToString();
		}

		public void Insert(TKey key, TValue value)
		{
			Insert(new BaseNode<TKey, TValue>(0, key, value));
		}

		public void Insert(BaseNode<TKey, TValue> node)
		{
			int i = _hight - 1;
			int j = _width - 1;

			if (node == null)
				throw new ArgumentException("new node is null", nameof(node));

			if (_table[i, j] != null)
				throw new Exception("Table is full. Key cannot be added");

			_table[i, j] = node;

			while (i > 0 || j > 0)
			{
				int iNext = i;
				int jNext = j;
				var key = _table[i, j].Key;
				var largest = _table[i, j];

				if (i > 0 && (_table[i - 1, j] == null || _table[i - 1, j].Key.CompareTo(key) > 0))
				{
					iNext = i - 1;
					jNext = j;
					largest = _table[iNext, jNext];
				}

				if (j > 0 && largest != null && (_table[i, j - 1] == null || _table[i, j - 1].Key.CompareTo(largest.Key) > 0))
				{
					iNext = i;
					jNext = j - 1;
				}

				if(i == iNext && j == jNext)
					break;

				NodeHelper<TKey, TValue>.SwapInTable(_table, i, j, iNext, jNext);
				i = iNext;
				j = jNext;
			}
		}

		public BaseNode<TKey, TValue> ExtractMin()
		{
			int i = 0;
			int j = 0;
			var node = _table[i, j];
			if (node == null)
				return null;

			_table[i, j] = null;
			while (i < _hight - 1 || j < _width - 1)
			{
				int iNext = i;
				int jNext = j;
				var right = (j < _width - 1) ? _table[i, j + 1] : null;
				var down = (i < _hight - 1) ? _table[i + 1, j] : null;
				if(right == null && down == null)
					break;

				if (j < _width - 1 && down == null)
				{
					iNext = i;
					jNext = j + 1;
				}
				else if (i < _hight - 1 && right == null)
				{
					iNext = i + 1;
					jNext = j;
				}
				else if (i < _hight - 1 && j < _width - 1 && right != null && down != null)
				{
					if (right.Key.CompareTo(down.Key) <= 0)
					{
						iNext = i;
						jNext = j + 1;
					}
					else
					{
						iNext = i + 1;
						jNext = j;
					}
				}

				if (i == iNext && j == jNext)
					break;

				NodeHelper<TKey, TValue>.SwapInTable(_table, i, j, iNext, jNext);
				i = iNext;
				j = jNext;
			}
			return node;
		}

		#endregion
	}
}
