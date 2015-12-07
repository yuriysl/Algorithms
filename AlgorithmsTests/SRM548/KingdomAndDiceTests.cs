using Algorithms.SRMs.SRM548;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.AlgorithmsTests.SRM548
{
	[TestClass()]
	public class KingdomAndDiceTests
	{
		[TestMethod()]
		public void newFairnessTest1()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 2, 7, 0}, new[] { 6, 3, 8, 10 }, 12);
			Assert.AreEqual(0.4375, res);
		}

		[TestMethod()]
		public void newFairnessTest2()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 2, 7, 0 }, new[] { 6, 3, 8, 10 }, 10);
			Assert.AreEqual(0.375, res);
		}

		[TestMethod()]
		public void newFairnessTest3()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0 }, new[] { 5, 8 }, 47);
			Assert.AreEqual(0.5, res);
		}

		[TestMethod()]
		public void newFairnessTest4()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 19, 50, 4 }, new[] { 26, 100, 37 }, 1000);
			Assert.AreEqual(0.2222222222222222, res);
		}

		[TestMethod()]
		public void newFairnessTest5()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 6371, 0, 6256, 1852, 0, 0, 6317, 3004, 5218, 9012 }, 
				new[] { 1557, 6318, 1560, 4519, 2012, 6316, 6315, 1559, 8215, 1561},
				10000);
			Assert.AreEqual(0.49, res);
		}
	}
}