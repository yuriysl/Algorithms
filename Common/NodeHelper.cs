using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class NodeHelper<TKey, TValue>
	{
		public static Random Rnd = new Random();

		static public void Swap(BaseNode<TKey, TValue> left, BaseNode<TKey, TValue> right)
		{
			TKey tmpKey = left.Key;
			left.Key = right.Key;
			right.Key = tmpKey;

			TValue tmpValue = left.Value;
			left.Value = right.Value;
			right.Value = tmpValue;
		}

		static public void SwapWithIndex(BaseNode<TKey, TValue> left, BaseNode<TKey, TValue> right)
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
