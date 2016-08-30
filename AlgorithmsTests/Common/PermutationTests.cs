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
		public void GetTranspositionsTest()
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
			var transpositions = permutation.GetTranspositions(input, 0).Select(item => string.Join(",", item.Select(node => node.Key))).ToList();
			int n = input.Length;
			int expectedCount = Factorial(n);
			int i = 0;
			foreach (var transposition in transpositions)
			{
				_testOutputHelper.WriteLine("[Transpositions:{0}], [{1}]", i, transposition);
				i++;
			}
			Assert.Equal(expectedCount, transpositions.Count);
		}

		private int Factorial(int n)
		{
			int res = 1;
			for (int i = 0; i < n; i++)
				res *= (i + 1);
			return res;
		}
	}
}