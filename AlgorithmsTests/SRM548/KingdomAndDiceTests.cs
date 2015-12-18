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

		[TestMethod()]
		public void newFairnessTest6()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
				1000000000);
			Assert.AreEqual(0.5, res);
		}

		[TestMethod()]
		public void newFairnessTest7()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0, 0, 0, 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
				new[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 43, 45, 47, 49, 51, 53, 55, 57, 59, 61, 63, 65, 67, 69, 71, 73, 75, 77, 79, 81, 83, 85, 87, 89, 91, 93, 95, 97, 99 },
				1000000000);
			Assert.AreEqual(0.5, res);
		}

		[TestMethod()]
		public void newFairnessTest8()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0 }, new[] { 2, 4 }, 5);
			Assert.AreEqual(0.5, res);
		}

		[TestMethod()]
		public void newFairnessTest9()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0, 0, 0 }, new[] { 5, 6, 7, 8 }, 9);
			Assert.AreEqual(0.25, res);
		}

		[TestMethod()]
		public void newFairnessTest10()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 583580964, 0, 0, 0, 0, 0, 0, 0, 549353893, 0, 0, 0, 255995577, 0, 0, 161064041, 0 }, 
				new[] { 96185108, 124390413, 518950998, 108883980, 116516829, 70613296, 455878940, 27406803, 82605278, 583284951, 157082497, 28741378, 119415043, 66963096, 65882888, 361932014, 177433896 },
				637444863);
			Assert.AreEqual(0.4982698961937716, res);
		}

		[TestMethod()]
		public void newFairnessTest11()
		{
			var dice = new KingdomAndDice();
			double res = dice.newFairness(
				new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new[] { 1001, 3003, 5005, 7007, 9009, 110011, 130013, 150015, 170017, 190019, 210021, 230023, 250025, 270027, 290029, 310031, 330033, 350035, 370037, 390039, 410041, 430043, 450045, 470047, 490049, 510051, 530053, 550055, 570057, 590059, 610061, 630063, 650065, 670067, 690069, 710071, 730073, 750075, 770077, 790079, 810081, 830083, 850085, 870087, 890089, 910091, 930093, 950095, 970097, 990099 },
				1000000000);
			Assert.AreEqual(0.5, res);
		}
	}
}