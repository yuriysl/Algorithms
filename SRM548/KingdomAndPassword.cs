using System;
using System.Collections.Generic;

namespace SRM548.KingdomAndTrees
{
/*
	Problem Statement for KingdomAndPassword


Problem Statement
	King Dengklek has a special room protected by a password. 
	The password is a positive integer that does not contain the digit zero. 
	You are given the old password as a long oldPassword. 
	King Dengklek has been using this password for a long time. 
	Now, he wants to create a new password. You are also given a int[] restrictedDigits. 
	The tradition in the kingdom insists that: 



The digits in the new password are a permutation of the digits in oldPassword.
For each i, the i-th most significant digit (0-based index) of the new password is not 
restrictedDigits[i].


Return the new password X. If there are multiple solutions, return the one for 
which |oldPassword - X| is minimized. If there are still two possible solutions, 
return the smaller one. If there is no valid new password at all, return -1 instead.
 
Definition
		
Class:	KingdomAndPassword
Method:	newPassword
Parameters:	long, int[]
Returns:	long
Method signature:	long newPassword(long oldPassword, int[] restrictedDigits)
(be sure your method is public)
	
 
Notes
-	|x| denotes the absolute value of x. For example, |3| = |-3| = 3.
-	The new password may be equal to the old password (see example 2).
 
Constraints
-	oldPassword will be between 1 and 10^16 - 1, inclusive.
-	oldPassword will not contain the digit zero.
-	restrictedDigits will contain the same number of elements as the number of digits in 
oldPassword.
-	Each element of restrictedDigits will be between 1 and 9, inclusive.
 
Examples
0)	
		
548
{5, 1, 8}
Returns: 485
1)	
		
7777
{4, 7, 4, 7}
Returns: -1
The only possible new password is 7777. Since digit 7 is restricted in two positions, 
this new password cannot be created.
2)	
		
58
{4, 7}
Returns: 58
3)	
		
172
{4, 7, 4}
Returns: 127
Both 127 and 217 are valid passwords. No other valid password is closer to 172 than 
these two. In this situation, King Dengklek will choose the smaller one.
4)	
		
241529363573463
{1, 4, 5, 7, 3, 9, 8, 1, 7, 6, 3, 2, 6, 4, 5}
Returns: 239676554423331
	*/
	public class KingdomAndPassword
	{
		readonly Dictionary<long, long> _diffs = new Dictionary<long, long>();

		public long newPassword(long oldPassword, int[] restrictedDigits)
		{
			int n = restrictedDigits.Length;
			long currPassword = oldPassword;

			int[] a = new int[n];
			for (int i = n - 1; i >= 0; i--, currPassword /= 10)
				a[i] = (int)(currPassword % 10);

			int[] r = new int[n];
			for (int i = 0; i < n; i++)
				r[i] = restrictedDigits[i];

			int[] sortA = new int[n];
			a.CopyTo(sortA, 0);
			Array.Sort(sortA);
//			Array.Reverse(sortA);
			var diff = CheckPermutations(new LinkedList<int>(sortA), a, r, 0, 0);
			return diff == long.MaxValue ? -1 : oldPassword + diff;
		}

		private long CheckPermutations(LinkedList<int> nodes, int[] a, int[] r, int level, long outerDiff)
		{
			long diff = long.MaxValue;
			int n = a.Length;
			var node = nodes.First;
			while (node != null)
			{
				if (node.Value == r[level])
				{
					node = node.Next;
					continue;
				}

				long nodeDiff = (node.Value - a[level]) * (long)Math.Pow(10, n - level - 1);
				long childDiff = 0;
				if (nodes.Count > 1)
				{
					var prevNode = node.Previous;
					nodes.Remove(node);

					long key = 0;
					foreach (var nd in nodes)
						key = key*10 + nd;

					if (!_diffs.TryGetValue(key, out childDiff))
					{
						childDiff = CheckPermutations(nodes, a, r, level + 1, outerDiff + nodeDiff);
						_diffs[key] = childDiff;
					}

					if (prevNode == null)
						nodes.AddFirst(node);
					else
						nodes.AddAfter(prevNode, node);
				}

				if (childDiff == long.MaxValue)
				{
					node = node.Next;
					continue;
				}

				if (diff == long.MaxValue)
					diff = nodeDiff + childDiff;
				else if (Math.Abs(outerDiff + nodeDiff + childDiff) < Math.Abs(outerDiff + diff))
					diff = nodeDiff + childDiff;
				else if(Math.Abs(outerDiff + nodeDiff + childDiff) == Math.Abs(outerDiff + diff))
					diff = Math.Min(nodeDiff + childDiff, diff);

				node = node.Next;
			}

			return diff;
		}
	}
}
