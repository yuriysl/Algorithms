using Xunit;
using Algorithms.SRMs.SRM675;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Algorithms.SRMs.SRM675.Tests
{
	public class LengthUnitCalculatorTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public LengthUnitCalculatorTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void newFairnessTest1()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "mi", "ft");
			Assert.Equal(5280.0, res);
		}

		[Fact]
		public void newFairnessTest2()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "ft", "mi");
			Assert.Equal(1.893939393939394E-4, res);
		}

		[Fact]
		public void newFairnessTest3()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1000, "mi", "in");
			Assert.Equal(6.336E7, res);
		}

		[Fact]
		public void newFairnessTest4()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "in", "mi");
			Assert.Equal(1.5782828282828283E-5, res);
		}

		[Fact]
		public void newFairnessTest5()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(47, "mi", "mi");
			Assert.Equal(47.0, res);
		}
	}
}