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

			for (int len = 1; len <= n; len++)
			{
				for (int i = 1; i <= n - len + 1; i++)
				{
					int j = i + len - 1;
					for (int k = 1; k <= len; k++)
					{
						var cost = u[i + k - 2];
						var weight = w[i + k - 2];
						int subCost = 0;
						int subWeight = 0;
						if (k > 1)
						{
							subCost += costs[i, i + k - 2];
							subWeight += weights[i, i + k - 2];
						}
						if (k < len)
						{
							subCost += costs[i + k, j];
							subWeight += weights[i + k, j];
						}
						if (subWeight <= wK && subCost > costs[i, j])
						{
							costs[i, j] = subCost;
							weights[i, j] = subWeight;
						}
						if (weight + subWeight <= wK && cost + subCost > costs[i, j])
						{
							costs[i, j] = cost + subCost;
							weights[i, j] = weight + subWeight;
						}
					}
				}
			}
		}

		public double GetMaxCostFractional(int[] u, int[] w, int wK)
		{
			int n = u.Length;
			double[,] costs = new double[n + 1, n + 1];
			double[,] weights = new double[n + 1, n + 1];
			int[] optimals = new int[n + 1];

			CalculateMaxCostFractional(u, w, wK, costs, weights);

			return costs[1, n];
		}

		public void CalculateMaxCostFractional(int[] u, int[] w, int wK, double[,] costs, double[,] weights)
		{
			int n = u.Length;

			for (int len = 1; len <= n; len++)
			{
				for (int i = 1; i <= n - len + 1; i++)
				{
					int j = i + len - 1;
					for (int k = 1; k <= len; k++)
					{
						var cost = u[i + k - 2];
						double weight = w[i + k - 2];
						double subCost = 0;
						double subWeight = 0;
						if (k > 1)
						{
							subCost += costs[i, i + k - 2];
							subWeight += weights[i, i + k - 2];
						}
						if (k < len)
						{
							subCost += costs[i + k, j];
							subWeight += weights[i + k, j];
						}
						if (subCost > costs[i, j])
						{
							if (subWeight <= wK)
							{
								costs[i, j] = subCost;
								weights[i, j] = subWeight;
							}
						}
						if (cost + subCost > costs[i, j])
						{
							if (weight + subWeight <= wK)
							{
								costs[i, j] = cost + subCost;
								weights[i, j] = weight + subWeight;
							}
							else if(wK >= subWeight)
							{
								var ratio = (wK - subWeight) / weight;
								var newCost = cost * ratio + subCost;
								if (newCost > costs[i, j])
								{
									costs[i, j] = newCost;
									weights[i, j] = weight * ratio + subWeight;
								}
							}
						}
					}
				}
			}
		}
	}
}
