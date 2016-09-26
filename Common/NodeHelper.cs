using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Algorithms.Common
{
	public static class NodeHelper<TKey, TValue>
	{
		public static readonly Random Rnd = new Random();

		public static void SwapInTable(BaseNode<TKey, TValue>[,] table, int iLeft, int jLeft, int iRight, int jRight)
		{
			if (table[iLeft, jLeft] == null && table[iRight, jRight] == null)
				return;
			if (table[iLeft, jLeft] == null)
			{
				table[iLeft, jLeft] = table[iRight, jRight];
				table[iRight, jRight] = null;
				return;
			}
			if (table[iRight, jRight] == null)
			{
				table[iRight, jRight] = table[iLeft, jLeft];
				table[iLeft, jLeft] = null;
				return;
			}
			Swap(table[iLeft, jLeft], table[iRight, jRight]);
		}

		public static void Swap(BaseNode<TKey, TValue> left, BaseNode<TKey, TValue> right)
		{
			TKey tmpKey = left.Key;
			left.Key = right.Key;
			right.Key = tmpKey;

			TValue tmpValue = left.Value;
			left.Value = right.Value;
			right.Value = tmpValue;
		}

		public static void SwapWithIndex(BaseNode<TKey, TValue> left, BaseNode<TKey, TValue> right)
		{
			TKey tmpKey = left.Key;
			left.Key = right.Key;
			right.Key = tmpKey;

			TValue tmpValue = left.Value;
			left.Value = right.Value;
			right.Value = tmpValue;

			int tmpIndex = left.Index;
			left.Index = right.Index;
			right.Index = tmpIndex;
		}
	}
}
