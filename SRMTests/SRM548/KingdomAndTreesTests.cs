using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRM548.KingdomAndTrees;

namespace SRMTests.SRM548
{
    [TestClass]
	public class KingdomAndTreesTests
	{
		[TestMethod]
		public void minLevel1()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.miLnevel(new[] {9, 5, 11});
			Assert.AreEqual(3, result);
		}

		[TestMethod]
		public void minLevel2()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.miLnevel(new[] { 5, 8 });
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void minLevel3()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.miLnevel(new[] { 1, 1, 1, 1, 1 });
			Assert.AreEqual(4, result);
		}

		[TestMethod]
		public void minLevel4()
		{
			var kingdomAndTrees = new KingdomAndTrees();
			int result = kingdomAndTrees.miLnevel(new[] { 548, 47, 58, 250, 2012 });
			Assert.AreEqual(251, result);
		}
	}
}
