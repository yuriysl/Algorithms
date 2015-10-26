using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Searching
	{
		public int BinarySearch<T>(T[] a, int p, int r, T key)
			where T : IComparable<T>
		{
			if (p > r)
				return -1;
			if (p == r)
				return a[r].CompareTo(key) == 0 ? r : -1;
			int q = (p + r) / 2;
			if (a[q].CompareTo(key) == 0)
				return q;
			if (a[q].CompareTo(key) < 0)
				return BinarySearch(a, q + 1, r, key);

			return BinarySearch(a, p, q, key);
		}

		public int BinarySearchTail<T>(T[] a, int p, int r, T key)
			where T : IComparable<T>
		{
			while (p < r)
			{
				int q = (p + r) / 2;
				if (a[q].CompareTo(key) == 0)
					return q;
				if (a[q].CompareTo(key) < 0)
					p = q + 1;
				else
					r = q;
			}
			if (p > r)
				return -1;
			return a[r].CompareTo(key) == 0 ? r : -1;
		}
	}
}
