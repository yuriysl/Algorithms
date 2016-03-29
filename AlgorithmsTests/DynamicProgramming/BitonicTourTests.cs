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
	public class BitonicTourTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public BitonicTourTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void getmaxTest0()
		{
			var bitonicTour = new BitonicTour();
			var res = bitonicTour.GetMinTour(new double[] { 0, 1, 2, 5, 6, 7, 8 }, new double[] { 6, 0, 3, 4, 1, 5, 2 });
			Assert.Equal(25.58, res);
		}
	}
}
