using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.SRMs.SRM580;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.SRM580
{
	public class EelAndRabbitDiv1Level1Tests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public EelAndRabbitDiv1Level1Tests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void getmaxTest0()
		{
			var eelAndRabbit = new EelAndRabbit();
			var res = eelAndRabbit.getmax(new[] { 2, 4, 3, 2, 2, 1, 10 }, new[] { 2, 6, 3, 7, 0, 2, 0 });
			Assert.Equal(6, res);
		}

		[Fact]
		public void getmaxTest1()
		{
			var eelAndRabbit = new EelAndRabbit();
			var res = eelAndRabbit.getmax(new[] { 1, 1, 1 }, new[] { 2, 0, 4 });
			Assert.Equal(2, res);
		}

		[Fact]
		public void getmaxTest2()
		{
			var eelAndRabbit = new EelAndRabbit();
			var res = eelAndRabbit.getmax(new[] { 1 }, new[] { 1 });
			Assert.Equal(1, res);
		}

		[Fact]
		public void getmaxTest3()
		{
			var eelAndRabbit = new EelAndRabbit();
			var res = eelAndRabbit.getmax(new[] { 8, 2, 1, 10, 8, 6, 3, 1, 2, 5 }, new[] { 17, 27, 26, 11, 1, 27, 23, 12, 11, 13 });
			Assert.Equal(7, res);
		}
	}
}
