using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Common
{
	public class Sorting
	{
		/// <summary>
		/// O(n^2)
		/// </summary>
		public void InsertionSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int? p = null, int? r = null)
			where TKey : IComparable<TKey>
		{
			int left = p ?? 0;
			int right = r ?? (a.Count - 1);
			for (int i = left + 1; i <= right; ++i)
			{
				var key = a[i].Key;
				var value = a[i].Value;
				var index = a[i].Index;
				int j = i - 1;
				while (j >= left && a[j].Key.CompareTo(key) > 0)
				{
					a[j + 1].Key = a[j].Key;
					a[j + 1].Value = a[j].Value;
					a[j + 1].Index = a[j].Index;
					j--;
				}
				a[j + 1].Key = key;
				a[j + 1].Value = value;
				a[j + 1].Index = index;
			}
		}

		public List<BaseNode<TKey, TValue>> GetInsertionSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			InsertionSort(a);
			return a;
		}

		public int[] Add(int[] a, int[] b)
		{
			int n = a.Length;
			var c = new int[n + 1];
			int carry = 0;
			for (int i = 0; i < n; i++)
			{
				c[i] = (carry + a[i] + b[i]) % 2; ;
				carry = (carry + a[i] + b[i]) / 2;
			}
			c[n] = carry;
			return c;
		}

		/// <summary>
		/// O(n^2)
		/// </summary>
		public void SelectionSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			for (int i = 0; i < n - 1; ++i)
			{
				int index = i;
				for (int j = i + 1; j < n; j++)
				{
					if (a[j].Key.CompareTo(a[index].Key) < 0)
						index = j;
				}
				NodeHelper<TKey, TValue>.SwapWithIndex(a[i], a[index]);
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void MergeSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			if (p >= r)
				return;
			int q = (p + r) / 2;
			MergeSort(a, p, q);
			MergeSort(a, q + 1, r);
			Merge(a, p, q, r);
		}

		private void Merge<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int q, int r)
			where TKey : IComparable<TKey>
		{
			int n1 = q - p + 1;
			int n2 = r - q;
			var left = new List<BaseNode<TKey, TValue>>();
			var right = new List<BaseNode<TKey, TValue>>();
			for (int i1 = 0; i1 < n1; i1++)
				left.Add(a[p + i1]);
			for (int i1 = 0; i1 < n2; i1++)
				right.Add(a[q + i1 + 1]);

			int i = 0, j = 0;

			for (int k = p; k <= r; ++k)
			{
				if (j == n2 || i < n1 && left[i].Key.CompareTo(right[j].Key) <= 0)
				{
					a[k] = left[i];
					a[k].Index = k;
					i++;
				}
				else
				{
					a[k] = right[j];
					a[k].Index = k;
					j++;
				}
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void QuickSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			if (p >= r)
				return;
			int q = QuickPartition(a, p, r);
			QuickSort(a, p, q - 1);
			QuickSort(a, q + 1, r);
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void QuickSortTail<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			while (p < r)
			{
				int q = QuickRandomizedPartition(a, p, r);
				QuickSortTail(a, p, q - 1);
				p = q + 1;
			}
		}

		public int QuickPartition<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r, int? xI = null) 
			where TKey : IComparable<TKey>
		{
			NodeHelper<TKey, TValue>.SwapWithIndex(a[xI.HasValue ? xI.Value : r], a[r]);
			var x = a[r];
			int i = p - 1;
			int xN = 0;
			for (int j = p; j < r; j++)
			{
				if(a[j].Key.CompareTo(x.Key) <= 0)
				{
					if (a[j].Key.CompareTo(x.Key) == 0)
						xN++;
					i++;
					NodeHelper<TKey, TValue>.SwapWithIndex(a[i], a[j]);
				}
			}
			NodeHelper<TKey, TValue>.SwapWithIndex(a[i + 1], a[r]);
			return xN + 1 == (r - p + 1) ? (p + r) / 2 : i + 1;
		}

		public int QuickRandomizedPartition<TKey, TValue>(List<BaseNode<TKey, TValue>> a, int p, int r)
			where TKey : IComparable<TKey>
		{
			int rndR = NodeHelper<TKey, TValue>.Rnd.Next(p, r);
			NodeHelper<TKey, TValue>.SwapWithIndex(a[rndR], a[r]);

			var x = a[r];
			int i = p - 1;
			int xN = 0;
			for (int j = p; j < r; j++)
			{
				if (a[j].Key.CompareTo(x.Key) <= 0)
				{
					if (a[j].Key.CompareTo(x.Key) == 0)
						xN++;
					i++;
					NodeHelper<TKey, TValue>.SwapWithIndex(a[i], a[j]);
				}
			}
			NodeHelper<TKey, TValue>.SwapWithIndex(a[i + 1], a[r]);
			return xN + 1 == (r - p + 1) ? (p + r) / 2 : i + 1;
		}

		/// <summary>
		/// O(n^2)
		/// </summary>
		public void BubbleSort<TKey, TValue>(List<BaseNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			for (int i = 0; i < n; i++)
			{
				for (int j = n - 1; j > i; --j)
				{
					if (a[j].Key.CompareTo(a[j - 1].Key) < 0)
						NodeHelper<TKey, TValue>.SwapWithIndex(a[j], a[j - 1]);
				}
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void HeapSortMax<TKey, TValue>(List<BinaryHeapNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMaxHeap<TKey, TValue>)binaryHeap)).BuildMax();
			for (int i = n - 1; i >= 0; i--)
			{
				NodeHelper<TKey, TValue>.Swap(a[i], a[0]);
				binaryHeap.HeapSize--;
				(((IMaxHeap<TKey, TValue>)binaryHeap)).MaxHeapify(a[0]);
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void HeapSortMaxTail<TKey, TValue>(List<BinaryHeapNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMaxHeap<TKey, TValue>)binaryHeap)).BuildMaxTail();
			for (int i = n - 1; i >= 0; i--)
			{
				NodeHelper<TKey, TValue>.Swap(a[i], a[0]);
				binaryHeap.HeapSize--;
				(((IMaxHeap<TKey, TValue>)binaryHeap)).MaxHeapifyTail(a[0]);
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void HeapSortMin<TKey, TValue>(List<BinaryHeapNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMinHeap<TKey, TValue>)binaryHeap)).BuildMin();
			for (int i = n - 1; i > 0; i--)
			{
				NodeHelper<TKey, TValue>.Swap(a[i], a[0]);
				binaryHeap.HeapSize--;
				(((IMinHeap<TKey, TValue>)binaryHeap)).MinHeapify(a[0]);
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void HeapSortMinTail<TKey, TValue>(List<BinaryHeapNode<TKey, TValue>> a)
			where TKey : IComparable<TKey>
		{
			int n = a.Count;
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMinHeap<TKey, TValue>)binaryHeap)).BuildMinTail();
			for (int i = n - 1; i > 0; i--)
			{
				NodeHelper<TKey, TValue>.Swap(a[i], a[0]);
				binaryHeap.HeapSize--;
				(((IMinHeap<TKey, TValue>)binaryHeap)).MinHeapifyTail(a[0]);
			}
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public BaseNode<int, TValue>[] CountingSort<TValue>(List<BaseNode<int, TValue>> a, int? r = null, int left = 0, int right = 9)
		{
			int n = a.Count;
			int index;
			int k = right - left + 1;
			var b = new BaseNode<int, TValue>[n];
			if (n == 0)
				return b;
			int[] c = new int[k];

			for (int i = 0; i < k; i++)
				c[i] = 0;

			for (int j = 0; j < n; j++)
			{
				index = (r.HasValue ? ((a[j].Key / (int)Math.Pow(10, r.Value)) % 10) : a[j].Key) - left;
				c[index]++;
			}

			for (int i = 1; i < k; i++)
				c[i] += c[i - 1];

			for (int j = n - 1; j >= 0; j--)
			{
				index = (r.HasValue ? ((a[j].Key / (int)Math.Pow(10, r.Value)) % 10) : a[j].Key) - left;
				a[j].Index = c[index] - 1;
				b[c[index] - 1] = a[j];
				c[index]--;
			}
			return b;
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public int[] CountingSortForIntervalChecking<TValue>(List<BaseNode<int, TValue>> a, int? r = null, int left = 0, int right = 9)
		{
			int n = a.Count;
			int index;
			int k = right - left + 1;
			int[] c = new int[k];
			if (n == 0)
				return c;

			for (int i = 0; i < k; i++)
				c[i] = 0;

			for (int j = 0; j < n; j++)
			{
				index = (r.HasValue ? ((a[j].Key / (int)Math.Pow(10, r.Value)) % 10) : a[j].Key) - left;
				c[index]++;
			}

			for (int i = 1; i < k; i++)
				c[i] += c[i - 1];

			return c;
		}

		/// <summary>
		/// O(1)
		/// </summary>
		public int GetCountInInterval(int[] items, int a, int b, int left = 0, int right = 9)
		{
			int lower = Math.Min(items.Length - 1, a - 1);
			int upper = Math.Min(items.Length - 1, b);
			return Math.Max(0, ((upper - left) < 0 ? 0 : items[upper - left]) - ((lower - left) < 0 ? 0 : items[lower - left]));
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public BaseNode<string, TValue>[] CountingSort<TValue>(List<BaseNode<string, TValue>> a, int? r = null, int left = 0, int right = 255)
		{
			int n = a.Count;
			byte index;
			int k = right - left + 1;
			var b = new BaseNode<string, TValue>[n];
			if (n == 0)
				return b;
			int[] c = new int[k];

			for (int i = 0; i < k; i++)
				c[i] = 0;

			for (int j = 0; j < n; j++)
			{
				index = (byte)((r.HasValue ? Convert.ToByte(a[j].Key[a[j].Key.Length - r.Value - 1]) : byte.Parse(a[j].Key)) - left);
				c[index]++;
			}

			for (int i = 1; i < k; i++)
				c[i] += c[i - 1];

			for (int j = n - 1; j >= 0; j--)
			{
				index = (byte)((r.HasValue ? Convert.ToByte(a[j].Key[a[j].Key.Length - r.Value -1]) : byte.Parse(a[j].Key)) - left);
				a[j].Index = c[index] - 1;
				b[c[index] - 1] = a[j];
				c[index]--;
			}
			return b;
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public BaseNode<int, TValue>[] RadixSort<TValue>(List<BaseNode<int, TValue>> a, int d)
		{
			BaseNode<int, TValue>[] res = null;
			for (int i = 0; i < d; i++)
				res = CountingSort(res == null ? a : res.ToList(), i);
			return res ?? new BaseNode<int, TValue>[0];
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public BaseNode<string, TValue>[] RadixSort<TValue>(List<BaseNode<string, TValue>> a, int d, int left = 0, int right = 255)
		{
			BaseNode<string, TValue>[] res = null;
			for (int i = 0; i < d; i++)
				res = CountingSort(res == null ? a : res.ToList(), i, left, right);
			return res ?? new BaseNode<string, TValue>[0];
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public List<BaseNode<int, TValue>> BucketSort<TValue>(List<BaseNode<int, TValue>> a, int k = 16, int left = 0, int right = 255)
		{
			int n = a.Count;
			var b = new List<BaseNode<int, TValue>>();
			var interval = (int)Math.Ceiling((right - left + 1) / (double)k);
			var buckets = new List<BaseNode<int, TValue>>[k];
			for (int i = 0; i < buckets.Length; i++)
				buckets[i] = new List<BaseNode<int, TValue>>();
			for (int i = 0; i < n; i++)
			{
				int index = (a[i].Key - left - 1) / interval;
				buckets[index].Add(a[i]);
			}
			for (int i = 0; i < buckets.Length; i++)
			{
				var res = GetInsertionSort(buckets[i]);
				b.AddRange(res);
			}
			for (int i = 0; i < n; i++)
				b[i].Index = i;

			return b;
		}
	}
}
