using Algorithms.SRMs.SRM548;
using Xunit;

namespace Algorithms.AlgorithmsTests.SRM548
{
	public class KingdomAndDucksTests
	{
		[Fact]
		public void minDucksTest1()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 5, 8 });
			Assert.Equal(2, res);
		}

		[Fact]
		public void minDucksTest2()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 4, 7, 4, 7, 4 });
			Assert.Equal(6, res);
		}

		[Fact]
		public void minDucksTest3()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 17, 17, 19, 23, 23, 19, 19, 17, 17 });
			Assert.Equal(12, res);
		}

		[Fact]
		public void minDucksTest4()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 50 });
			Assert.Equal(1, res);
		}

		[Fact]
		public void minDucksTest5()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 10, 10, 10, 10, 10 });
			Assert.Equal(5, res);
		}
	}
}