/*
  
REGISTER LOG IN
COMPETE
 
LEARN
 
COMMUNITY
 	
		   Problem Statement  	



 Problem Statement for EelAndRabbit


Problem Statement
    	Rabbit went to a river to catch eels. All eels are currently swimming down the stream at the same speed. Rabbit is standing by the river, downstream from all the eels. 



Each point on the river has a coordinate. The coordinates increase as we go down the stream. Initially, Rabbit is standing at the origin, and all eels have non-positive coordinates. 



You are given two int[]s: l and t. These describe the current configuration of eels. The speed of each eel is 1 (one). For each i, the length of eel number i is l[i]. The head of eel number i will arrive at the coordinate 0 precisely at the time t[i]. Therefore, at any time T the eel number i has its head at the coordinate T-t[i], and its tail at the coordinate T-t[i]-l[i]. 



Rabbit may only catch an eel when some part of the eel (between head and tail, inclusive) is at the same coordinate as the rabbit. Rabbit can catch eels at most twice. Each time he decides to catch eels, he may catch as many of the currently available eels as he wants. (That is, he can only catch eels that are in front of him at that instant, and he is allowed and able to catch multiple eels at once.) 



Return the maximal total number of eels Rabbit can catch.
 
Definition
    	
Class:	EelAndRabbit
Method:	getmax
Parameters:	int[], int[]
Returns:	int
Method signature:	int getmax(int[] l, int[] t)
(be sure your method is public)
    
 
Constraints
-	l will contain between 1 and 50 elements, inclusive.
-	Each element of l will be between 1 and 1,000,000,000, inclusive.
-	l and t will contain the same number of elements.
-	Each element of t will be between 0 and 1,000,000,000, inclusive.
 
Examples
0)	
    	
{2, 4, 3, 2, 2, 1, 10}
{2, 6, 3, 7, 0, 2, 0}
Returns: 6
Rabbit can catch 6 eels in the following way:
At time 2, catch Eel 0, Eel 4, Eel 5, and Eel 6.
At time 8, catch Eel 1 and Eel 3.
1)	
    	
{1, 1, 1}
{2, 0, 4}
Returns: 2
No two eels are in front of Rabbit at the same time, so Rabbit can catch at most two eels.
2)	
    	
{1}
{1}
Returns: 1
3)	
    	
{8, 2, 1, 10, 8, 6, 3, 1, 2, 5}
{17, 27, 26, 11, 1, 27, 23, 12, 11, 13}
Returns: 7
This problem statement is the exclusive and proprietary property of TopCoder, Inc. Any unauthorized use or reproduction of this information without the prior written consent of TopCoder, Inc. is strictly prohibited. (c)2010, TopCoder, Inc. All rights reserved.




This problem was used for: 
       Single Round Match 580 Round 1 - Division I, Level One 
       Single Round Match 580 Round 1 - Division II, Level Two



SITEMAP ABOUT US CONTACT US HELP CENTER PRIVACY POLICY TERMS
© 2015 topcoder. All Rights Reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.SRM580
{
	public class EelAndRabbit
	{
		public int getmax(int[] l, int[] t)
		{
			return GetMax(l, t);
		}

		int GetMax(int[] l, int[] t)
		{
			int res = 0;
			var n = l.Length;
			int[] overlappings = new int[n];

			for (int i = 0; i < n; i++)
			{
				for (int k = 0; k < n; k++)
					overlappings[k] = 0;

				overlappings[i] = 1;
				int firstCount = SetOverlappings(l, t, overlappings, i, 1);
				if (firstCount > res)
					res = firstCount;
				for (int j = i + 1; j < n; j++)
				{
					if (overlappings[j] == 1)
						continue;

					for (int k = 0; k < n; k++)
						if(overlappings[k] == 2)
							overlappings[k] = 0;

					overlappings[j] = 2;
					int secondCount = SetOverlappings(l, t, overlappings, j, 2);
					if (firstCount + secondCount > res)
						res = firstCount + secondCount;
				}
			}

			return res;
		}

		private int SetOverlappings(int[] l, int[] t, int[] overlappings, int m, int step)
		{
			int count = 0;
			var n = l.Length;
			for (int k = 0; k < n; k++)
			{
				if (k == m || overlappings[k] == 1)
					continue;
				if (!(-(t[k]) < (-t[m]) || ((-t[k]) - l[k]) > (-t[m])))
				{
					overlappings[k] = step;
					count++;
				}
			}
			return count + 1;
		}
	}
}
