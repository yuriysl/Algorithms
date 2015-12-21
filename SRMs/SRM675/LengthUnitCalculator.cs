using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SRMs.SRM675
{
	public class LengthUnitCalculator
	{
		enum Units
		{
			inch = 0,
			ft = 1,
			yd = 2,
			mi = 3
		}

		public double calc(int amount, string fromUnit, string toUnit)
		{
			var from = (Units) Enum.Parse(typeof (Units), fromUnit.Replace("in", "inch"));
			var to = (Units) Enum.Parse(typeof (Units), toUnit.Replace("in", "inch"));

			return 0;
		}
    }
}
