using Algorithms.SRMs.SRM548;
using Xunit;

namespace Algorithms.AlgorithmsTests.SRM548
{
	public class KingdomAndTreesTests
	{
		[Fact]
		public void minLevel1()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.minLevel(new[] {9, 5, 11});
			Assert.Equal(3, result);
		}

		[Fact]
		public void minLevel2()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.minLevel(new[] { 5, 8 });
			Assert.Equal(0, result);
		}

		[Fact]
		public void minLevel3()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.minLevel(new[] { 1, 1, 1, 1, 1 });
			Assert.Equal(4, result);
		}

		[Fact]
		public void minLevel4()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.minLevel(new[] { 548, 47, 58, 250, 2012 });
			Assert.Equal(251, result);
		}
	}
}
