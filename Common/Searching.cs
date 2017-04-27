using System;
using System.Collections.Generic;

namespace Algorithms.Common
{
	public class Searching
	{
		static readonly Sorting Sorting = new Sorting();

		public BaseNode<TKey, TValue> BinarySearch<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r, TKey key)
			where TKey : IComparable<TKey>
		{
			if (p > r)
				return null;
			if (p == r)
				return a[r].Key.CompareTo(key) == 0 ? a[r] : null;
			int q = (p + r) / 2;
			if (a[q].Key.CompareTo(key) == 0)
				return a[q];
			if (a[q].Key.CompareTo(key) < 0)
				return BinarySearch(a, q + 1, r, key);

			return BinarySearch(a, p, q, key);
		}

		public BaseNode<TKey, TValue> BinarySearchTail<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r, TKey key)
			where TKey : IComparable<TKey>
		{
			while (p < r)
			{
				int q = (p + r) / 2;
				if (a[q].Key.CompareTo(key) == 0)
					return a[q];
				if (a[q].Key.CompareTo(key) < 0)
					p = q + 1;
				else
					r = q;
			}
			if (p > r)
				return null;
			return a[r].Key.CompareTo(key) == 0 ? a[r] : null;
		}

		public Tuple<BaseNode<TKey, TValue>, BaseNode<TKey, TValue>> SelectMinMax<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			if (p > r || r >= a.Count || p < 0 || r < 0)
				return null;
			if (p == r)
				return new Tuple<BaseNode<TKey, TValue>, BaseNode<TKey, TValue>>(a[p], a[p]);

			int n = r - p + 1;
			var min = a[0];
			var max = a[0];
			if (n % 2 == 0)
			{
				max = a[1];
				if (max.Key.CompareTo(min.Key) < 0)
					NodeHelper<TKey, TValue>.Swap(max, min);
			}

			int start = n % 2 == 1 ? 1 : 2;
			for (int i = start; i < n; i = i + 2)
			{
				if (a[i].Key.CompareTo(a[i + 1].Key) < 0)
				{
					if (min.Key.CompareTo(a[i].Key) > 0)
						min = a[i];
					if (max.Key.CompareTo(a[i + 1].Key) < 0)
						max = a[i + 1];
				}
				else
				{
					if (min.Key.CompareTo(a[i + 1].Key) > 0)
						min = a[i + 1];
					if (max.Key.CompareTo(a[i].Key) < 0)
						max = a[i];
				}
			}
			return new Tuple<BaseNode<TKey, TValue>, BaseNode<TKey, TValue>>(min, max);
		}

		public BaseNode<TKey, TValue> RandomizedSelect<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r, int i)
			where TKey : IComparable<TKey>
		{
			if (p == r)
				return a[p];

			var qPivots = Sorting.QuickRandomizedPartition(a, p, r);

			int kMin = qPivots.Item1 - p + 1;
			int kMax = qPivots.Item2 - p + 1;
			if (i >= kMin && i <= kMax)
				return a[qPivots.Item1 + (i - kMin)];
			if (i < kMin)
				return RandomizedSelect(a, p, qPivots.Item1 - 1, i);

			return RandomizedSelect(a, qPivots.Item2 + 1, r, i - kMax);
		}

		public BaseNode<TKey, TValue> Select<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r, int i)
			where TKey : IComparable<TKey>
		{
			int n = r - p + 1;
			if (n == 1)
				return a[p];

			var input = new List<BaseNode<TKey, TValue>>();
			for (int j = p; j <= r; j++)
				input.Add(a[j]);

			int medianIndex = SelectMedian(input, p, r);
			var qPivots = Sorting.QuickPartition(a, p, r, medianIndex);

			int k = qPivots.Item1 - p + 1;
			if (i == k)
				return a[qPivots.Item1];
			if (i < k)
				return Select(a, p, qPivots.Item1 - 1, i);

			return Select(a, qPivots.Item2 + 1, r, i - k);
		}

		private int SelectMedian<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			int n = r - p + 1;
			if (n == 1)
				return a[p].Index;

			var b = new List<BaseNode<TKey, TValue>>();
			for (int j = 0; j < n; j = j + 5)
			{
				int j2 = Math.Min(j + 4, n - 1);
				Sorting.InsertionSort(a, j, j2);
				b.Add(a[(j2 + j) / 2]);
			}

			int medianIndex = SelectMedian(b, 0, b.Count - 1);
			return medianIndex;
		}
	}
}
