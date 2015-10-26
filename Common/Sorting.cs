using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Sorting
	{
		private static Random Rnd = new Random();
		/// <summary>
		/// O(n^2)
		/// </summary>
		public void InsertionSort<T>(T[] a)
			where T : IComparable<T>
		{
			int n = a.Length;
			for (int i = 1; i < n; i++)
			{
				T key = a[i];
				int j = i - 1;
				while (j >= 0 && a[j].CompareTo(key) > 0)
				{
					a[j + 1] = a[j];
					j--;
				}
				a[j + 1] = key;
			}
		}

		/// <summary>
		/// O(n^2)
		/// </summary>
		public void SelectionSort<T>(T[] a)
			where T : IComparable<T>
		{
			int n = a.Length;
			for (int i = 0; i < n - 1; i++)
			{
				int index = i;
				for (int j = i + 1; j < n; j++)
				{
					if (a[j].CompareTo(a[index]) < 0)
						index = j;
				}
				T tmp = a[i];
				a[i] = a[index];
				a[index] = tmp;
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void MergeSort<T>(T[] a, int p, int r)
			where T : IComparable<T>
		{
			if (p >= r)
				return;
			int q = (p + r) / 2;
			MergeSort(a, p, q);
			MergeSort(a, q + 1, r);
			Merge(a, p, q, r);
		}

		private void Merge<T>(T[] a, int p, int q, int r)
			where T : IComparable<T>
		{
			int n1 = q - p + 1;
			int n2 = r - q;
			T[] left = new T[n1];
			T[] right = new T[n2];
			for (int i1 = 0; i1 < n1; i1++)
				left[i1] = a[p + i1];
			for (int i1 = 0; i1 < n2; i1++)
				right[i1] = a[q + i1 + 1];

			int i = 0, j = 0;

			for (int k = p; k <= r; k++)
			{
				if (j == n2 || i < n1 && left[i].CompareTo(right[j]) <= 0)
				{
					a[k] = left[i];
					i++;
				}
				else
				{
					a[k] = right[j];
					j++;
				}
			}
		}

		/// <summary>
		/// O(nlg(n))
		/// </summary>
		public void QuickSort<T>(T[] a, int p, int r)
			where T : IComparable<T>
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
		public void QuickSortTail<T>(T[] a, int p, int r)
			where T : IComparable<T>
		{
			while (p < r)
			{
				int q = QuickPartition(a, p, r);
				QuickSortTail(a, p, q - 1);
				p = q + 1;
			}
		}

		private int QuickPartition<T>(T[] a, int p, int r) 
			where T : IComparable<T>
		{
			int rndR = Rnd.Next(p, r);
			Swap(a, rndR, r);

			T x = a[r];
			int i = p - 1;
			int xN = 0;
			for (int j = p; j < r; j++)
			{
				if(a[j].CompareTo(x) <= 0)
				{
					if (a[j].CompareTo(x) == 0)
						xN++;
					i++;
					Swap(a, i, j);
				}
			}
			Swap(a, i + 1, r);
			return xN + 1 == (r - p + 1) ? (p + r) / 2 : i + 1;
		}

		/// <summary>
		/// O(n^2)
		/// </summary>
		public void BubbleSort<T>(T[] a)
			where T : IComparable<T>
		{
			int n = a.Length;
			for (int i = 0; i < n; i++)
			{
				for (int j = n - 1; j > i; j--)
				{
					if (a[j].CompareTo(a[j - 1]) < 0)
					{
						T tmp = a[j];
						a[j] = a[j - 1];
						a[j - 1] = tmp;
					}
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
			var nodes = new List<BinaryHeapNode<TKey, TValue>>();
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMaxHeap<TKey, TValue>)binaryHeap)).BuildMax();
			for (int i = n - 1; i >= 0; i--)
			{
				BinaryHeap<TKey, TValue>.Swap(a[i], a[0]);
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
			var nodes = new List<BinaryHeapNode<TKey, TValue>>();
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMaxHeap<TKey, TValue>)binaryHeap)).BuildMaxTail();
			for (int i = n - 1; i >= 0; i--)
			{
				BinaryHeap<TKey, TValue>.Swap(a[i], a[0]);
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
			var nodes = new List<BinaryHeapNode<TKey, TValue>>();
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMinHeap<TKey, TValue>)binaryHeap)).BuildMin();
			for (int i = n - 1; i > 0; i--)
			{
				BinaryHeap<TKey, TValue>.Swap(a[i], a[0]);
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
			var nodes = new List<BinaryHeapNode<TKey, TValue>>();
			var binaryHeap = new BinaryHeap<TKey, TValue>(a);
			(((IMinHeap<TKey, TValue>)binaryHeap)).BuildMinTail();
			for (int i = n - 1; i > 0; i--)
			{
				BinaryHeap<TKey, TValue>.Swap(a[i], a[0]);
				binaryHeap.HeapSize--;
				(((IMinHeap<TKey, TValue>)binaryHeap)).MinHeapifyTail(a[0]);
			}
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public int[] CountingSort(int[] a, int left, int right, int? r = null)
		{
			int n = a.Length;
			int index;
			int k = right - left + 1;
			int[] b = new int[n];
			if (n == 0)
				return b;
			int[] c = new int[k + 1];

			for (int i = 0; i <= k; i++)
				c[i] = 0;

			for (int j = 0; j < n; j++)
			{
				index = (r.HasValue ? ((a[j] / (int)Math.Pow(10, r.Value)) % 10) : a[j]) - left;
				c[index]++;
			}

			for (int i = 1; i <= k; i++)
				c[i] += c[i - 1];

			for (int j = n - 1; j >= 0; j--)
			{
				index = (r.HasValue ? ((a[j] / (int)Math.Pow(10, r.Value)) % 10) : a[j]) - left;
				b[c[index] - 1] = a[j];
				c[index]--;
			}
			return b;
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public string[] CountingSort(string[] a, int left, int right, int? r = null)
		{
			int n = a.Length;
			byte index;
			int k = right - left + 1;
			string[] b = new string[n];
			if (n == 0)
				return b;
			byte[] c = new byte[k + 1];

			for (int i = 0; i <= k; i++)
				c[i] = 0;

			for (int j = 0; j < n; j++)
			{
				index = (byte)((r.HasValue ? Convert.ToByte(a[j][a[j].Length - r.Value - 1]) : byte.Parse(a[j])) - left);
				c[index]++;
			}

			for (int i = 1; i <= k; i++)
				c[i] += c[i - 1];

			for (int j = n - 1; j >= 0; j--)
			{
				index = (byte)((r.HasValue ? Convert.ToByte(a[j][a[j].Length - r.Value -1]) : byte.Parse(a[j])) - left);
				b[c[index] - 1] = a[j];
				c[index]--;
			}
			return b;
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public int[] RadixSort(int[] a, int d)
		{
			int n = a.Length;
			for (int i = 0; i < d; i++)
			{
				a = CountingSort(a, 0, 9, i);
			}
			return a;
		}

		/// <summary>
		/// O(n)
		/// </summary>
		public string[] RadixSort(string[] a, int d)
		{
			int n = a.Length;
			for (int i = 0; i < d; i++)
			{
				a = CountingSort(a, '0', '9', i);
			}
			return a;
		}

		public static void Swap<T>(T[] a, int left, int right)
		{
			T tmp = a[left];
			a[left] = a[right];
			a[right] = tmp;
		}
	}
}
