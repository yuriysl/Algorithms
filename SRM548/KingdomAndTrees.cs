using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRM548.KingdomAndTrees
{
	public class KingdomAndTrees
	{
		public int miLnevel(int[] heights)
		{
			int n = heights.Length;
			long[] h = new long[n];
			for (int i = 0; i < n; ++i) h[i] = heights[i];
			long left = -1;
			long right = (long)1e10;
			while (right - left > 1)
			{
				long middle = (left + right) / 2;
				long min = 0;
				bool ok = true;
				for (int i = 0; i < n; ++i)
				{
					min = Math.Max(min + 1, h[i] - middle);
					if (min > h[i] + middle)
						ok = false;
				}
				if (ok)
					right = middle;
				else
					left = middle;
			}
			return (int)right;
		}
	}
}
