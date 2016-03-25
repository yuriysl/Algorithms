using Algorithms.SRMs.SRM548;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.SRM548
{
	public class KingdomAndPasswordTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public KingdomAndPasswordTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void newPasswordTest1()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(548, new[]{5, 1, 8});
			Assert.Equal(485, res);
		}

		[Fact]
		public void newPasswordTest2()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(7777, new[] { 4, 7, 4, 7 });
			Assert.Equal(-1, res);
		}

		[Fact]
		public void newPasswordTest3()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(58, new[] { 4, 7 });
			Assert.Equal(58, res);
		}

		[Fact]
		public void newPasswordTest4()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(172, new[] { 4, 7, 4 });
			Assert.Equal(127, res);
		}

		[Fact]
		public void newPasswordTest5()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(241529363573463, new[] { 1, 4, 5, 7, 3, 9, 8, 1, 7, 6, 3, 2, 6, 4, 5 });
			Assert.Equal(239676554423331, res);
		}

		[Fact]
		public void newPasswordTest6()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(6669982649, new[] { 9, 6, 9, 4, 6, 6, 6, 9, 8, 2 });
			Assert.Equal(6826499669, res);
		}

		[Fact]
		public void newPasswordTest7()
		{
			var pass = new KingdomAndPassword();
			long res = pass.newPassword(9735633893326, new[] { 3, 1, 8, 5, 1, 7, 3, 6, 8, 5, 9, 9, 3 });
			Assert.Equal(9736235333689, res);
		}
	}
}