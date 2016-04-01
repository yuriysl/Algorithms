using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.DynamicProgramming
{
	public class BitonicTour
	{
		double[,] distances;

		public double GetMinTour(double[] x, double[] y, out int[,] r)
		{
			int n = x.Length;
			distances = new double[n, n];
			r = new int[n, n];

			distances[0, 1] = GetDistance(x[0], y[0], x[1], y[1]);

			for (int j = 2; j < n; j++)
			{
				for (int i = 0; i <= j - 2; i++)
				{
					distances[i, j] = distances[i, j - 1] + GetDistance(x[j - 1], y[j - 1], x[j], y[j]);
					r[i, j] = j - 1;
				}
				distances[j - 1, j] = double.PositiveInfinity;
				for (int k = 0; k <= j - 2; k++)
				{
					var subPath = distances[k, j - 1] + GetDistance(x[k], y[k], x[j], y[j]);
					if(subPath < distances[j - 1, j])
					{
						distances[j - 1, j] = subPath;
						r[j - 1, j] = k;
					}
				}
			}

			distances[n - 1, n - 1] = distances[n - 2, n - 1] + GetDistance(x[n - 2], y[n - 2], x[n - 1], y[n - 1]);
			return Math.Round(distances[n - 1, n - 1], 2);
		}

		private double GetDistance(double x1, double y1, double x2, double y2)
		{
			return Math.Round(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)), 3);
		}
	}
}
