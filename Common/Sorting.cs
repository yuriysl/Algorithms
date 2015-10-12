using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Sorting
	{
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
	}
}
