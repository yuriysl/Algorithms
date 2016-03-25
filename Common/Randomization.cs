using System;

namespace Algorithms.Common
{
	public class Randomization
	{
		Random rnd = null;
		const double ProbabilityDistribution = 0.42;

		/// <summary>
		/// O(n)
		/// </summary>
		public void RandomizationInPlace<T>(T[] a, int left, int right)
			where T : IComparable<T>
		{
			rnd = new Random();
			for (int i = left; i <= right - 1; i++)
			{
				int newI = rnd.Next(i, right + 1);
				T tmp = a[i];
				a[i] = a[newI];
				a[newI] = tmp;
			}
		}

		public int GetUniformDistribution()
		{
			int newVal, newValInverse;
			do
			{
				newVal = GetBiesedRandom();
				newValInverse = GetBiesedRandom();
			} while (newVal == 0 && newValInverse == 0 || newVal == 1 && newValInverse == 1);
			return newVal == 0 && newValInverse == 1 ? 0 : 1;
		}

		int GetBiesedRandom()
		{
			if(rnd == null)
				rnd = new Random();
			var newVal = rnd.NextDouble();
			return newVal < ProbabilityDistribution ? 0 : 1;
		}
	}
}
