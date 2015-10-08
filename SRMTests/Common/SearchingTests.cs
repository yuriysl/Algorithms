using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	class SearchingTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public T Key { get; set; }
		public int Expected { get; set; }
	}

	[TestClass()]
	public class SearchingTests
	{
		List<SearchingTestCase<int>> _testCases;

		[TestInitialize()]
		public void TestInitialize()
		{
			_testCases = new List<SearchingTestCase<int>>()
			{
				new SearchingTestCase<int>
				{
					Name = "test case 1",
					Input = new List<int> {},
					Key = 0,
					Expected = -1
				},
				new SearchingTestCase<int>
				{
					Name = "test case 2",
					Input = new List<int> {1, 2},
					Key = 2,
					Expected = 1
				},
				new SearchingTestCase<int>
				{
					Name = "test case 3",
					Input = new List<int> {1, 2, 3},
					Key = 1,
					Expected = 0
				},
				new SearchingTestCase<int>
				{
					Name = "test case 4",
					Input = new List<int> {1, 2, 3},
					Key = 2,
					Expected = 1
				},
				new SearchingTestCase<int>
				{
					Name = "test case 5",
					Input = new List<int> {1, 2, 3},
					Key = 3,
					Expected = 2
				},
				new SearchingTestCase<int>
				{
					Name = "test case 6",
					Input = new List<int> {1, 2, 3},
					Key = 4,
					Expected = -1
				},
				new SearchingTestCase<int>
				{
					Name = "test case 7",
					Input = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15},
					Key = 6,
					Expected = 9
				}
			};
		}

		[TestMethod()]
		public void BinarySearchTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expected = testCase.Expected;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Key:[{0}]", key);
				Console.WriteLine("Expected:[{0}]", expected);

				int actual = searching.BinarySearch(input, 0, n - 1, key);

				Console.WriteLine("Actual:[{0}]", actual);

				Assert.AreEqual(expected, actual);
			}


			
		}
	}
}