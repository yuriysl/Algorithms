using System;

namespace Algorithms.Common
{
	public class Randomization
	{
		private Random _rnd;
		const double ProbabilityDistribution = 0.42;

		/// <summary>
		/// O(n)
		/// </summary>
		public void RandomizationInPlace<T>(T[] a, int left, int right)
			where T : IComparable<T>
		{
			_rnd = new Random();
			for (int i = left; i <= right - 1; i++)
			{
				int newI = _rnd.Next(i, right + 1);
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
			if(_rnd == null)
				_rnd = new Random();
			var newVal = _rnd.NextDouble();
			return newVal < ProbabilityDistribution ? 0 : 1;
		}
	}
}
