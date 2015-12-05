using Algorithms.SRMs.SRM548;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.AlgorithmsTests.SRM548
{
	[TestClass]
	public class KingdomAndPasswordTests
	{
		[TestMethod]
		public void newPasswordTest1()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(548, new[]{5, 1, 8});
			Assert.AreEqual(485, res);
		}

		[TestMethod]
		public void newPasswordTest2()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(7777, new[] { 4, 7, 4, 7 });
			Assert.AreEqual(-1, res);
		}

		[TestMethod]
		public void newPasswordTest3()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(58, new[] { 4, 7 });
			Assert.AreEqual(58, res);
		}

		[TestMethod]
		public void newPasswordTest4()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(172, new[] { 4, 7, 4 });
			Assert.AreEqual(127, res);
		}

		[TestMethod]
		public void newPasswordTest5()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(241529363573463, new[] { 1, 4, 5, 7, 3, 9, 8, 1, 7, 6, 3, 2, 6, 4, 5 });
			Assert.AreEqual(239676554423331, res);
		}

		[TestMethod]
		public void newPasswordTest6()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(6669982649, new[] { 9, 6, 9, 4, 6, 6, 6, 9, 8, 2 });
			Assert.AreEqual(6826499669, res);
		}
	}
}