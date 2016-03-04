using Algorithms.SRMs.SRM580;
using Xunit;

namespace Algorithms.AlgorithmsTests.SRM580
{
	public class ShoutterDiv2Level1Tests
	{
		[Fact]
		public void Count0Test()
		{
			var shoutter = new ShoutterDiv2();
			var res = shoutter.count(new[] { 1, 2, 4 }, new[] { 3, 4, 6 });
			Assert.Equal(2, res);
		}

		[Fact]
		public void Count1Test()
		{
			var shoutter = new ShoutterDiv2();
			var res = shoutter.count(new[] { 0 }, new[] { 100 });
			Assert.Equal(0, res);
		}

		[Fact]
		public void Count2Test()
		{
			var shoutter = new ShoutterDiv2();
			var res = shoutter.count(new[] { 0, 0, 0 }, new[] { 1, 1, 1 });
			Assert.Equal(3, res);
		}

		[Fact]
		public void Count3Test()
		{
			var shoutter = new ShoutterDiv2();
			var res = shoutter.count(
				new[] { 9, 26, 8, 35, 3, 58, 91, 24, 10, 26, 22, 18, 15, 12, 15, 27, 15, 60, 76, 19, 12, 16, 37, 35, 25, 4, 22, 47, 65, 3, 2, 23, 26, 33, 7, 11, 34, 74, 67, 32, 15, 45, 20, 53, 60, 25, 74, 13, 44, 51 },
				new[] { 26, 62, 80, 80, 52, 83, 100, 71, 20, 73, 23, 32, 80, 37, 34, 55, 51, 86, 97, 89, 17, 81, 74, 94, 79, 85, 77, 97, 87, 8, 70, 46, 58, 70, 97, 35, 80, 76, 82, 80, 19, 56, 65, 62, 80, 49, 79, 28, 75, 78 });
			Assert.Equal(830, res);
		}
	}
}
