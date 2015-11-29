using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public long newPassword(long oldPassword, int[] restrictedDigits)
		{
			int n = restrictedDigits.Length;
			int[] a = new int[n];
			long currPassword = oldPassword;
			for (int i = 0; i < n; i++, currPassword /= 10)
				a[n - i - 1] = (int)(currPassword % 10);
			int[] r = new int[n];
			for (int i = 0; i < n; i++)
				r[i] = restrictedDigits[i];



			return 0;
		}
	}
}
