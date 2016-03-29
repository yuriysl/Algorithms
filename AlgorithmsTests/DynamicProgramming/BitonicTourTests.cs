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
		public void getMinTourTest0()
		{
			var bitonicTour = new BitonicTour();
			var res = bitonicTour.GetMinTour(new double[] { 0, 1, 2, 5, 6, 7, 8 }, new double[] { 6, 0, 3, 4, 1, 5, 2 });
			Assert.Equal(25.58, res);
		}

		[Fact]
		public void getMinTourTest1()
		{
			var bitonicTour = new BitonicTour();
			var res = bitonicTour.GetMinTour(new double[] { 1, 2, 3, 4, 7 }, new double[] { 0, 8, 4, 2, 7 });
			Assert.Equal(25.70, res);
		}
	}
}
