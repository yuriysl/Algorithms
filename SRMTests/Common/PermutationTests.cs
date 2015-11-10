using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	[TestClass()]
	public class PermutationTests
	{
		[TestMethod()]
		public void GetPremutationsTest()
		{
			var permutation = new Permutation();
			var input = new BaseNode<int, object>[6] {
				new BaseNode<int, object>(0, 0),
				new BaseNode<int, object>(0, 1),
				new BaseNode<int, object>(0, 2),
				new BaseNode<int, object>(0, 3),
				new BaseNode<int, object>(0, 4),
				new BaseNode<int, object>(0, 5)
			};
			var permutations = permutation.GetPremutations(input, input.Length);
			int i = 0;
			foreach (var item in permutations)
			{
				Console.WriteLine("[Permutation:{0}], [{1}]", i, string.Join(",", item.Select(node => node.Key)));
				i++;
			}
			var groups = permutations.GroupBy(item => string.Join(",", item.Select(node => node.Key)), item => item);
			i = 0;
			foreach (var group in groups)
			{
				Console.WriteLine("[Permutation Group:{0}], [Key:{1}], [Count:{2}]", i, group.Key, group.Count());
				i++;
			}
		}
	}
}