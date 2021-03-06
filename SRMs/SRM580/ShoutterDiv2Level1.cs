﻿/* 

Problem Statement
    
A group of freshman rabbits has recently joined the Eel club. No two of the rabbits knew each other. Today, each of the rabbits went to the club for the first time. You are given int[]s s and t with the following meaning: For each i, rabbit number i entered the club at the time s[i] and left the club at the time t[i].
Each pair of rabbits that was in the club at the same time got to know each other, and they became friends on the social network service Shoutter. This is also the case for rabbits who just met for a single moment (i.e., one of them entered the club exactly at the time when the other one was leaving).
Compute and return the number of pairs of rabbits that became friends today.
Definition
    
Class:
ShoutterDiv2
Method:
count
Parameters:
int[], int[]
Returns:
int
Method signature:
int count(int[] s, int[] t)
(be sure your method is public)
Limits
    
Time limit (s):
2.000
Memory limit (MB):
64
Constraints
-
s and t will contain between 1 and 50 integers, inclusive.
-
s and t will contain the same number of elements.
-
Each integer in s and t will be between 0 and 100, inclusive.
-
For each i, t[i] will be greater than or equal to s[i].
Examples
0)

    
{1, 2, 4}
{3, 4, 6}
Returns: 2
Rabbit 0 and Rabbit 1 will be friends because both of them are in the club between time 2 and 3. Rabbit 0 and Rabbit 2 won't be friends because Rabbit 0 will leave the club before Rabbit 2 enters the club. Rabbit 1 and Rabbit 2 will be friends because both of them are in the club at time 4. 
1)

    
{0}
{100}
Returns: 0

2)

    
{0,0,0}
{1,1,1}
Returns: 3

3)

    
{9,26,8,35,3,58,91,24,10,26,22,18,15,12,15,27,15,60,76,19,12,16,37,35,25,4,22,47,65,3,2,23,26,33,7,11,34,74,67,32,15,45,20,53,60,25,74,13,44,51}
{26,62,80,80,52,83,100,71,20,73,23,32,80,37,34,55,51,86,97,89,17,81,74,94,79,85,77,97,87,8,70,46,58,70,97,35,80,76,82,80,19,56,65,62,80,49,79,28,75,78}
Returns: 830

This problem statement is the exclusive and proprietary property of TopCoder, Inc. Any unauthorized use or reproduction of this information without the prior written consent of TopCoder, Inc. is strictly prohibited. (c)2003, TopCoder, Inc. All rights reserved.
*/

namespace Algorithms.SRMs.SRM580
{
	public class ShoutterDiv2
	{
		public int count(int[] s, int[] t)
		{
			return GetMeets(s, t);
		}

		int GetMeets(int[] s, int[] t)
		{
			int meets = 0;
			int n = s.Length;
			for (int i = 0; i < n; i++)
			{
				for (int j = i + 1; j < n; j++)
				{
					if(!(t[i] < s[j] || s[i] > t[j]))
					{
						meets++;
					}
				}
			}
			return meets;
		}
	}
}
