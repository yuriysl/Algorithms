using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.DynamicProgramming
{
	public class KnapSack
	{
		public int GetMaxCostDiscrete(int[] u, int[] w, int wK)
		{
			int n = u.Length;
			int[,] costs = new int[n + 1, n + 1];
			int[,] weights = new int[n + 1, n + 1];
			int[] optimals = new int[n + 1];

			CalculateMaxCostDiscrete(u, w, wK, costs, weights);

			return costs[1, n];
		}

		public void CalculateMaxCostDiscrete(int[] u, int[] w, int wK, int[,] costs, int[,] weights)
		{
			int n = u.Length;
			for (int i = 1; i <= n; i++)
			{
				if (w[i - 1] <= wK)
				{
					costs[i, i] = u[i - 1];
					weights[i, i] = w[i - 1];
				}
			}

			for (int len = 2; len <= n; len++)
			{
				for (int i = 1; i <= n - len + 1; i++)
				{
					int j = i + len - 1;
					for (int k = 1; k < len; k++)
					{
						var cost = costs[i, i + k - 1] + u[j - 1];
						var weight = weights[i, i + k - 1] + w[j - 1];
						if (weight <= wK && cost > costs[i, j])
						{
							costs[i, j] = cost;
							weights[i, j] = weight;
						}
					}
				}
			}

		}

		public double GetMaxCostFractional(int[] u, int[] w, int wK)
		{
			int n = u.Length;
			double[,] costs = new double[n, n];
			return costs[0, n - 1];
		}
	}
}
