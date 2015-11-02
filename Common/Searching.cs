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

		public Tuple<T, T> SelectMinMax<T>(T[] a, int p, int r)
			where T : IComparable<T>
		{
			if (p > r || r >= a.Length || p < 0 || r < 0)
				return null;
			if (p == r)
				return new Tuple<T, T>(a[p], a[p]);

			int n = r - p + 1;
			T min = a[0];
			T max = n % 2 == 1 ? a[0] : a[1];
			int start = n % 2 == 1 ? 1 : 2;
			for (int i = start; i < n; i = i + 2)
			{
				if (a[i].CompareTo(a[i + 1]) < 0)
				{
					if (min.CompareTo(a[i]) > 0)
						min = a[i];
					if (max.CompareTo(a[i + 1]) < 0)
						max = a[i + 1];
				}
				else
				{
					if (min.CompareTo(a[i + 1]) > 0)
						min = a[i + 1];
					if (max.CompareTo(a[i]) < 0)
						max = a[i];
				}
			}
			return new Tuple<T, T>(min, max);
		}
	}
}
