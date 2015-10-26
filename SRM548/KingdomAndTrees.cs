using System;

namespace SRM548.KingdomAndTrees
{
/*
 Problem Statement for KingdomAndTrees


Problem Statement
		King Dengklek once planted N trees, conveniently numbered 0 through N-1, along the main highway in the Kingdom of Ducks. As time passed, the trees grew beautifully. Now, the height of the i-th tree is heights[i] units. 



King Dengklek now thinks that the highway would be even more beautiful if the tree heights were in strictly ascending order. More specifically, in the desired configuration the height of tree i must be strictly smaller than the height of tree i+1, for all possible i. To accomplish this, King Dengklek will cast his magic spell. If he casts magic spell of level X, he can increase or decrease the height of each tree by at most X units. He cannot decrease the height of a tree into below 1 unit. Also, the new height of each tree in units must again be an integer. 



Of course, a magic spell of a high level consumes a lot of energy. Return the smallest possible non-negative integer X such that King Dengklek can achieve his goal by casting his magic spell of level X.
 
Definition
		
Class:	KingdomAndTrees
Method:	minLevel
Parameters:	int[]
Returns:	int
Method signature:	int minLevel(int[] heights)
(be sure your method is public)
	
 
Constraints
-	heights will contain between 2 and 50 elements, inclusive.
-	Each elements of heights will be between 1 and 1,000,000,000, inclusive.
 
Examples
0)	
		
{9, 5, 11}
Returns: 3
One possible solution that uses magic spell of level 3:
Decrease the height of the first tree by 2 units.
Increase the height of the second tree by 3 units.
The resulting heights are {7, 8, 11}.
1)	
		
{5, 8}
Returns: 0
These heights are already sorted in strictly ascending order.
2)	
		
{1, 1, 1, 1, 1}
Returns: 4
Since King Dengklek cannot decrease the heights of the trees below 1, the only possible solution is to cast his magic spell of level 4 to transform these heights into {1, 2, 3, 4, 5}.
3)	
		
{548, 47, 58, 250, 2012}
Returns: 251

*/
	public class KingdomAndTrees
	{
		public int miLnevel(int[] heights)
		{
			int n = heights.Length;
			long[] h = new long[n];
			for (int i = 0; i < n; ++i) h[i] = heights[i];
			long left = -1;
			var right = (long)1e10;
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
