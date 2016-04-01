using Algorithms.SRMs.DynamicProgramming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.DynamicProgramming
{
	public class KnapSackTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public KnapSackTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void GetMaxCostDiscreteTest0()
		{
			var knapSack = new KnapSack();
			var res = knapSack.GetMaxCostDiscrete(new int[] { 60, 100, 120 }, new int[] { 10, 20, 30 }, 50);
			Assert.Equal(220, res);
		}

		[Fact]
		public void GetMaxCostFractionalTest0()
		{
			var knapSack = new KnapSack();
			var res = knapSack.GetMaxCostFractional(new int[] { 60, 100, 120 }, new int[] { 10, 20, 30 }, 50);
			Assert.Equal(240, res);
		}
	}
}
