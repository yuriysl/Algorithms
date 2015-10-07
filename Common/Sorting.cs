using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Sorting
	{
		public void InsertionSort<T>(T[] a)
			where T : IComparable<T>
		{
			int n = a.Length;
			for (int j = 1; j < n; j++)
			{
				T key = a[j];
				int i = j - 1;
				while (i >= 0 && key.CompareTo(a[i]) < 0)
				{
					a[i + 1] = a[i];
					i--;
				}
				a[i + 1] = key;
			}
		}
	}
}
