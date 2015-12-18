using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.SRMs.SRM675;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.SRM675.Tests
{
	[TestClass()]
	public class LengthUnitCalculatorTests
	{
		[TestMethod()]
		public void newFairnessTest1()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "mi", "ft");
			Assert.AreEqual(5280.0, res);
		}

		[TestMethod()]
		public void newFairnessTest2()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "ft", "mi");
			Assert.AreEqual(1.893939393939394E-4, res);
		}

		[TestMethod()]
		public void newFairnessTest3()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1000, "mi", "in");
			Assert.AreEqual(6.336E7, res);
		}

		[TestMethod()]
		public void newFairnessTest4()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(1, "in", "mi");
			Assert.AreEqual(1.5782828282828283E-5, res);
		}

		[TestMethod()]
		public void newFairnessTest5()
		{
			var calc = new LengthUnitCalculator();
			double res = calc.calc(47, "mi", "mi");
			Assert.AreEqual(47.0, res);
		}
	}
}