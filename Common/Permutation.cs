using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Permutation
	{
		public IEnumerable<BaseNode<TKey, TValue>[]> GetPremutations<TKey, TValue>(BaseNode<TKey, TValue>[] a, int n)
		{
			int m = a.Length;
			if (n == 2)
			{
				yield return a;
				NodeHelper<TKey, TValue>.SwapWithIndex(a[m - 2], a[m - 1]);
				yield return a;
			}
			else
			{
				var p = GetPremutations(a, n - 1);
				foreach (var pItem in p)
				{
					for (int i = m - n; i < m; i++)
					{
						NodeHelper<TKey, TValue>.SwapWithIndex(a[m - n], a[i]);
						yield return pItem;
						NodeHelper<TKey, TValue>.SwapWithIndex(a[m - n], a[i]);
					}
				}
			}
		}
	}
}
