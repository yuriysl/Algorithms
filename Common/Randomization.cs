using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Randomization
	{
		/// <summary>
		/// O(n)
		/// </summary>
		public void RandomizationInPlace<T>(T[] a, int left, int right)
			where T : IComparable<T>
		{
			var rnd = new Random();
			for (int i = left; i <= right - 1; i++)
			{
				int newI = rnd.Next(i, right);
				T tmp = a[i];
				a[i] = a[newI];
				a[newI] = tmp;
			}
		}
	}
}
