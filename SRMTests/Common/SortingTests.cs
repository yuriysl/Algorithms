using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	[TestClass()]
	public class SortingTests
	{
		[TestMethod()]
		public void InsertionSortTest()
		{
			var sorting = new Sorting();
			int[] input = { };
			sorting.InsertionSort(input);
			Assert.IsTrue(input.Length == 0);
		}

		[TestMethod()]
		public void InsertionSortTest1()
		{
			var sorting = new Sorting();
			int[] input = {1, 2};
			sorting.InsertionSort(input);
			Assert.IsTrue(input[0] == 1);
			Assert.IsTrue(input[1] == 2);
		}

		[TestMethod]
		public void InsertionSortTest2()
		{
			var sorting = new Sorting();
			int[] input = { 2, 1 };
			sorting.InsertionSort(input);
			Assert.AreEqual(1, input[0]);
			Assert.AreEqual(2, input[1]);
		}

		[TestMethod]
		public void InsertionSortTest3()
		{
			var sorting = new Sorting();
			int[] input = { 3, 1, 2 };
			sorting.InsertionSort(input);
			Assert.AreEqual(1, input[0]);
			Assert.AreEqual(2, input[1]);
			Assert.AreEqual(3, input[2]);
		}
	}
}