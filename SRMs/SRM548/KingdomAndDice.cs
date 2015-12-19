using System;
using System.Collections.Generic;

namespace Algorithms.SRMs.SRM548
{
	/*
	
Problem Statement
    
King Dengklek has two N-sided dice. The dice are not necessarily equal. 
Each of the dice is fair: when rolled, each side will come up with probability 1/N. 
Some time ago, each side of each die was labeled by a positive integer between 1 and X, inclusive. 
The labels were all unique. In other words, exactly 2*N distinct integers were used to label 
the sides of the two dice.  King Dengklek has been playing with the first die for a long time. 
Therefore, some of its labels were scratched off. The corresponding sides of the die are now 
empty. The second die still has all of its labels.  The current labels on the first die are 
given in the int[] firstDie: if the i-th side has no label, firstDie[i] is 0, otherwise 
firstDie[i] is the label. The current labels on the second die are given in secondDie: 
the i-th side of the second die has the label secondDie[i].  
In King Dengklek's favorite game, he takes one of the dice, his opponent takes the other, 
and they each roll the die they have. The one who throws a larger number is the winner. 
King Dengklek wants to fill in the missing labels on the first die. His goal is to fill 
them in such a way that his favorite game becomes as fair as possible.  When filling in the 
missing labels, King Dengklek wants to preserve the two properties mentioned above: 
first, each integer between 1 and X, inclusive, may only occur at most once on the two dice. 
Second, no other labels may be used. However, there is an exception to the second rule: 
King Dengklek is also allowed to use the label 0. Moreover, he may even use this label 
multiple times.  You are given the int[]s firstDie and secondDie, and the int X. 
For a particular way to fill in the missing labels, let P be the probability that the player 
with the first die wins in the king's game. Find the labeling that minimizes the value 
|P - 0.5| and return the corresponding value of P. If there are two possible solutions, 
return the smaller one.
Definition
    
Class:
KingdomAndDice
Method:
newFairness
Parameters:
int[], int[], int
Returns:
double
Method signature:
double newFairness(int[] firstDie, int[] secondDie, int X)
(be sure your method is public)
Limits
    
Time limit (s):
2.000
Memory limit (MB):
64
Notes
-
Your return value must have a relative or an absolute error of less than 1e-9.
-
|x| denotes the the absolute value of x. For example, |3| = |-3| = 3.
Constraints
-
firstDie and secondDie will contain the same number of elements, between 2 and 50, inclusive.
-
X will be between 2*N and 1,000,000,000, inclusive, where N is the number of elements in 
firstDie.
-
Each element of firstDie will be between 0 and X, inclusive.
-
Each element of secondDie will be between 1 and X, inclusive.
-
Each integer between 1 and X, inclusive, will occur at most once in firstDie and secondDie 
together.
Examples
0)

    
{0, 2, 7, 0}
{6, 3, 8, 10}
12
Returns: 0.4375
One possible solution is to relabel the first die into {4, 2, 7, 11}. 
The probability of winning against the second die will be 7/16.
1)

    
{0, 2, 7, 0}
{6, 3, 8, 10}
10
Returns: 0.375
One possible solution is to relabel the first die into {9, 2, 7, 5}. 
The probability of winning against the second die will be 3/8.
2)

    
{0, 0}
{5, 8}
47
Returns: 0.5
One possible solution is to relabel the first die into {10, 0}.
3)

    
{19, 50, 4}
{26, 100, 37}
1000
Returns: 0.2222222222222222
The first die does not have any missing labels.
4)

    
{6371, 0, 6256, 1852, 0, 0, 6317, 3004, 5218, 9012}
{1557, 6318, 1560, 4519, 2012, 6316, 6315, 1559, 8215, 1561}
10000
Returns: 0.49
	*/
	public class KingdomAndDice
	{
		private Dictionary<int, int> _data;
		private int[] _diffs;
		private byte[] _bytes;

		public double newFairness(int[] firstDie, int[] secondDie, int X)
		{
			if (firstDie.Length != secondDie.Length)
				throw new ArgumentException();
			int n = firstDie.Length;
			int emptySides = 0;
			int chances = 0;
			_diffs = new int[n];
			_bytes = new byte[n];
			for (int i = 1; i < n; i++)
			{
				var key = secondDie[i];
				int j = i - 1;
				while (j >= 0 && secondDie[j] > key)
				{
					secondDie[j + 1] = secondDie[j];
					j--;
				}
				secondDie[j + 1] = key;
			}
			for (int i = 0; i < n; i++)
			{
				if (firstDie[i] == 0)
				{
					emptySides++;
					continue;
				}
				for (int j = 0; j < n; j++)
				{
					if (firstDie[i] < secondDie[j])
					{
						if (j > 0)
						{
							_bytes[j - 1]++;
							chances += j << 1;
						}
						break;
					}
					if (firstDie[i] > secondDie[j] && j == n - 1 && firstDie[i] <= X)
					{
						_bytes[j]++;
						chances += (j + 1) << 1;
					}
				}
			}
			for (int i = 0; i < n; i++)
			{
				int left = secondDie[i];
				int right = i < n - 1 ? secondDie[i + 1] : X + 1;
				if (right - left > _bytes[i] + 1)
				{
					_diffs[i] = Math.Min(right - left - _bytes[i] - 1, emptySides);
				}
			}
			int capacity = Math.Max(emptySides * emptySides * emptySides * (int)Math.Floor(Math.Sqrt(emptySides)), 256);
			_data = new Dictionary<int, int>(capacity);
			int additionalChances = 0;
			for (int i = 0; i < n; i++)
			{
				if (_diffs[i] > 0)
				{
					additionalChances = GetAdditionalChances(i, emptySides, (n*n) - chances, secondDie);
					break;
				}
			}
			return (chances + additionalChances) / 2 / ((double) (n * n));
		}

		private int GetAdditionalChances(int sideIndex, int emptySides, int chances, int[] secondDie)
		{
			int n = secondDie.Length;
			int diff = 0;
			int upper = Math.Min(_diffs[sideIndex], emptySides);
			if (emptySides == 0 || chances < 0)
				return 0;

			for (int i = 0; i <= upper; i++)
			{
				int probeChance = ((sideIndex + 1) * i) << 1;

				if (Math.Abs(probeChance - chances) < Math.Abs(diff - chances))
					diff = probeChance;
				else if (Math.Abs(probeChance - chances) == Math.Abs(diff - chances))
				{
					if (probeChance < diff)
						diff = probeChance;
				}

				if (probeChance > chances && Math.Abs(diff - chances) <= probeChance - chances)
					break;
				if(i > 0)
					_bytes[sideIndex]++;

				int additionalChances = 0;
				int nextSideIndex = 0;
				for (int j = sideIndex + 1; j < n; ++j)
				{
					if (_diffs[j] > 0 && emptySides - i > 0)
					{
						nextSideIndex = j;
						break;
					}
				}
				if (nextSideIndex > 0)
				{
					int key = (chances - probeChance) * 10000 +(emptySides - i) * 100 + nextSideIndex;
					if (_data.ContainsKey(key))
					{
						var value = _data[key];
						additionalChances = value;
					}
					else
					{
						int maxChances = 0;
						int maxDiffs = emptySides - i;
						for (int j = n - 1; j >= nextSideIndex; --j)
						{
							if (_diffs[j] > 0)
							{
								int curMaxChances = Math.Max(Math.Min(_diffs[j], maxDiffs), 0);
								maxChances += ((j + 1) * curMaxChances) << 1;
								maxDiffs -= curMaxChances;
							}
						}
						if (diff > 0 && maxChances + probeChance < chances &&
							Math.Abs(diff - chances) < Math.Abs(maxChances + probeChance - chances))
						{
							if (i > 0)
								_bytes[sideIndex]--;
							continue;
						}
						additionalChances = GetAdditionalChances(nextSideIndex, emptySides - i, chances - probeChance, secondDie);
						_data[key] = additionalChances;
					}
				}

				if (Math.Abs(probeChance + additionalChances - chances) < Math.Abs(diff - chances))
					diff = probeChance + additionalChances;
				else if (Math.Abs(probeChance + additionalChances - chances) == Math.Abs(diff - chances))
				{
					if (probeChance + additionalChances < diff)
						diff = probeChance + additionalChances;
				}

				if (i > 0)
					_bytes[sideIndex]--;
			}

			return diff;
		}
	}
}
