using System.Collections.Generic;

namespace Algorithms.Common
{
	public class Permutation
	{
		public IEnumerable<BaseNode<TKey, TValue>[]> GetTranspositions<TKey, TValue>(BaseNode<TKey, TValue>[] a, int p)
		{
			int n = a.Length;
			int m = n - p;
			if (m == 1)
				yield return a;
			else if (m == 2)
			{
				yield return a;
				NodeHelper<TKey, TValue>.SwapWithIndex(a[p], a[p + 1]);
				yield return a;
				NodeHelper<TKey, TValue>.SwapWithIndex(a[p], a[p + 1]);
			}
			else
			{
				for (int i = p; i < n; i++)
				{
					if (i != p)
						NodeHelper<TKey, TValue>.SwapWithIndex(a[p], a[i]);

					foreach (var transposition in GetTranspositions(a, p + 1))
						yield return transposition;

					if (i != p)
						NodeHelper<TKey, TValue>.SwapWithIndex(a[p], a[i]);
				}
			}
		}
	}
}
