using Algorithms.SRMs.SRM548;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.AlgorithmsTests.SRM548
{
	[TestClass()]
	public class KingdomAndDucksTests
	{
		[TestMethod]
		public void minDucksTest1()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 5, 8 });
			Assert.AreEqual(2, res);
		}

		[TestMethod]
		public void minDucksTest2()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 4, 7, 4, 7, 4 });
			Assert.AreEqual(6, res);
		}

		[TestMethod]
		public void minDucksTest3()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 17, 17, 19, 23, 23, 19, 19, 17, 17 });
			Assert.AreEqual(12, res);
		}

		[TestMethod]
		public void minDucksTest4()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 50 });
			Assert.AreEqual(1, res);
		}

		[TestMethod]
		public void minDucksTest5()
		{
			var ducks = new KingdomAndDucks();
			int res = ducks.minDucks(new[] { 10, 10, 10, 10, 10 });
			Assert.AreEqual(5, res);
		}
	}
}