using System;
using System.Linq;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class PermutationTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public PermutationTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void GetPremutationsTest()
		{
			var permutation = new Permutation();
			var input = new[] {
				new BaseNode<int, object>(0, 0),
				new BaseNode<int, object>(0, 1),
				new BaseNode<int, object>(0, 2),
				new BaseNode<int, object>(0, 3),
				new BaseNode<int, object>(0, 4),
				new BaseNode<int, object>(0, 5)
			};
			var permutations = permutation.GetPremutations(input, input.Length).ToList();
			int i = 0;
			foreach (var item in permutations)
			{
				_testOutputHelper.WriteLine("[Permutation:{0}], [{1}]", i, string.Join(",", item.Select(node => node.Key)));
				i++;
			}
			var groups = permutations.GroupBy(item => string.Join(",", item.Select(node => node.Key)), item => item);
			i = 0;
			foreach (var group in groups)
			{
				_testOutputHelper.WriteLine("[Permutation Group:{0}], [Key:{1}], [Count:{2}]", i, group.Key, group.Count());
				i++;
			}
		}
	}
}