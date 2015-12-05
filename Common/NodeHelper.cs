using System;

namespace Algorithms.Common
{
	public static class NodeHelper<TKey, TValue>
	{
		public static readonly Random Rnd = new Random();

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
