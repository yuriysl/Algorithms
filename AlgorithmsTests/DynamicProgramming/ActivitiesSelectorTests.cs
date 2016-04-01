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
	public class ActivitiesSelectorTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public ActivitiesSelectorTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void GetMaxActivitiesSelectorRecursiveTest0()
		{
			var activitiesSelector = new ActivitiesSelector();
			var res = activitiesSelector.GetMaxActivitiesSelectorRecursive(new int[] { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 }, new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
			_testOutputHelper.WriteLine("MaxActivities:[{0}]", string.Join(", ", res.ToArray()));
			Assert.Equal(4, res.Count);
		}

		[Fact]
		public void GetMaxActivitiesSelectorGreedyTailTest0()
		{
			var activitiesSelector = new ActivitiesSelector();
			var res = activitiesSelector.GetMaxActivitiesSelectorGreedyTail(new int[] { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 }, new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
			_testOutputHelper.WriteLine("MaxActivities:[{0}]", string.Join(", ", res.ToArray()));
			Assert.Equal(4, res.Count);
		}
	}
}
