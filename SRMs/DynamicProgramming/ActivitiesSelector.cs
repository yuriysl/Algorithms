using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.DynamicProgramming
{
	public class ActivitiesSelector
	{
		int[] _starts;
		int[] _finishes;
		int[,] _counts;
		int[,] _splits;
		Stack<int> _path;
		Queue<int> _pathGreedy;

		public List<int> GetMaxActivitiesSelectorRecursive(int[] s, int[] f)
		{
			int n = s.Length;
			_starts = new int[n + 2];
			_finishes = new int[n + 2];
			for (int i = 0; i < n; i++)
			{
				_starts[i + 1] = s[i];
				_finishes[i + 1] = f[i];
			}
			_starts[n + 1] = int.MaxValue;

			_counts = new int[n + 2, n + 2];
			_splits = new int[n + 2, n + 2];

			CalculateMaxActivitiesSelectionRecursive(0, n + 1);

			_path = new Stack<int>();
			CalculatePath(0, n + 1);
			return _path.ToList();
		}

		private void CalculateMaxActivitiesSelectionRecursive(int i, int j)
		{
			if (_counts[i, j] > 0)
				return;

			if (!IsCompatible(i, j))
				return;

			if (j - i == 1)
				return;

			for (int k = i + 1; k < j; k++)
			{
				if (!IsCompatible(i, k) || !IsCompatible(k, j))
					continue;

				CalculateMaxActivitiesSelectionRecursive(i, k);
				CalculateMaxActivitiesSelectionRecursive(k, j);
				int res = _counts[i, k] + _counts[k, j] + 1;
				if(res > _counts[i, j])
				{
					_counts[i, j] = res;
					_splits[i, j] = k;
				}
			}
		}

		public List<int> GetMaxActivitiesSelectorGreedyTail(int[] s, int[] f)
		{
			int n = s.Length;
			_starts = new int[n + 2];
			_finishes = new int[n + 2];
			for (int i = 0; i < n; i++)
			{
				_starts[i + 1] = s[i];
				_finishes[i + 1] = f[i];
			}
			_starts[n + 1] = int.MaxValue;

			_counts = new int[n + 2, n + 2];
			_splits = new int[n + 2, n + 2];
			_pathGreedy = new Queue<int>();

			CalculateMaxActivitiesSelectionGreedyTail(0, n + 1);

			return _pathGreedy.ToList();
		}

		private void CalculateMaxActivitiesSelectionGreedyTail(int i, int j)
		{
			if (!IsCompatible(i, j))
				return;

			int mPrev = i;
			int mCurr = i + 1;
			while(mCurr < j)
			{
				if (!IsCompatible(mPrev, mCurr))
				{
					mCurr++;
					continue;
				}

				_counts[mPrev, mCurr + 1] = 1;
				_splits[mPrev, mCurr + 1] = mCurr;
				_pathGreedy.Enqueue(mCurr);
				mPrev = mCurr;
				mCurr++;
			}
		}

		private bool IsCompatible(int i, int j)
		{
			return _starts[i] >= _finishes[j] || _starts[j] >= _finishes[i];
		}

		private void CalculatePath(int i, int j)
		{
			if (j - i == 1)
				return;

			int splitter = _splits[i, j];

			if (splitter == 0)
				return;

			CalculatePath(i, splitter);
			_path.Push(splitter);
			CalculatePath(splitter, j);
		}
	}
}
