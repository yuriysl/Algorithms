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
		public int Order{ get; set; }
		public int Expected { get; set; }
		public T ExpectedMin { get; set; }
		public T ExpectedMax { get; set; }
		public T ExpectedItem { get; set; }
		public int ExpectedRank { get; set; }
		public bool IsBinarySearch { get; set; }
		public bool IsForMinMax { get; set; }
		public bool IsForSelection { get; set; }
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
					Expected = -1,
					ExpectedMin = -1,
					ExpectedMax = -1,
					IsBinarySearch = true,
					IsForMinMax = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 2",
					Input = new List<int> {1, 2},
					Key = 2,
					Expected = 2,
					ExpectedMin = 1,
					ExpectedMax = 2,
					Order = 1,
					ExpectedItem = 1,
					ExpectedRank = 1,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 3",
					Input = new List<int> {1, 2, 3},
					Key = 1,
					Expected = 1,
					ExpectedMin = 1,
					ExpectedMax = 3,
					Order = 3,
					ExpectedItem = 3,
					ExpectedRank = 3,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 4",
					Input = new List<int> {1, 2, 3},
					Key = 2,
					Expected = 2,
					ExpectedMin = 1,
					ExpectedMax = 3,
					Order = 2,
					ExpectedItem = 2,
					ExpectedRank = 2,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 5",
					Input = new List<int> {1, 2, 3},
					Key = 3,
					Expected = 3,
					ExpectedMin = 1,
					ExpectedMax = 3,
					Order = 1,
					ExpectedItem = 1,
					ExpectedRank = 1,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 6",
					Input = new List<int> {1, 2, 3},
					Key = 4,
					Expected = -1,
					ExpectedMin = 1,
					ExpectedMax = 3,
					Order = 1,
					ExpectedItem = 1,
					ExpectedRank = 1,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 7",
					Input = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15},
					Key = 6,
					Expected = 6,
					ExpectedMin = -9,
					ExpectedMax = 15,
					Order = 2,
					ExpectedItem = -8,
					ExpectedRank = 2,
					IsBinarySearch = true,
					IsForMinMax = true,
					IsForSelection = true
				},
				new SearchingTestCase<int>
				{
					Name = "test case 8",
					Input = new List<int> {15, -9, -8, -7, -6, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14},
					Key = 11,
					Expected = 11,
					ExpectedMin = -9,
					ExpectedMax = 15,
					Order = 9,
					ExpectedItem = 2,
					ExpectedRank = 9,
					IsBinarySearch = false,
					IsForMinMax = true,
					IsForSelection = true
				}
			};
		}

		[TestMethod()]
		public void BinarySearchTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsBinarySearch)
					continue;
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expected = testCase.Expected;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Key:[{0}]", key);
				Console.WriteLine("Expected:[{0}]", expected == -1 ? "null" : expected.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actual = searching.BinarySearch(nodes, 0, n - 1, key);

				Console.WriteLine("Actual:[{0}]", actual == null ? "null" : actual.Key.ToString());

				if (expected == -1)
					Assert.IsNull(actual);
				else
				{
					Assert.IsNotNull(actual);
					Assert.AreEqual(expected, actual.Key);
				}
			}
		}

		[TestMethod()]
		public void BinarySearchTailTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsBinarySearch)
					continue;
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expected = testCase.Expected;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Key:[{0}]", key);
				Console.WriteLine("Expected:[{0}]", expected == -1 ? "null" : expected.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actual = searching.BinarySearchTail(nodes, 0, n - 1, key);

				Console.WriteLine("Actual:[{0}]", actual == null ? "null" : actual.Key.ToString());

				if (expected == -1)
					Assert.IsNull(actual);
				else
				{
					Assert.IsNotNull(actual);
					Assert.AreEqual(expected, actual.Key);
				}
			}
		}

		[TestMethod()]
		public void SelectMinMaxTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForMinMax)
					continue;
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expectedMin = testCase.ExpectedMin;
				int expectedMax = testCase.ExpectedMax;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[Min:{0}]", expectedMin == -1 ? "null" : expectedMin.ToString());
				Console.WriteLine("Expected:[Max:{0}]", expectedMax == -1 ? "null" : expectedMax.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var minMax = searching.SelectMinMax(nodes, 0, n - 1);

				Console.WriteLine("Actual:[Min:{0}]", minMax == null ? "null" : minMax.Item1.ToString());
				Console.WriteLine("Actual:[Max:{0}]", minMax == null ? "null" : minMax.Item2.ToString());

				if (expectedMin == -1)
					Assert.IsNull(minMax);
				else
				{
					Assert.IsNotNull(minMax);
					Assert.AreEqual(expectedMin, minMax.Item1.Key);
				}
				if (expectedMax == -1)
					Assert.IsNull(minMax);
				else
				{
					Assert.IsNotNull(minMax);
					Assert.AreEqual(expectedMax, minMax.Item2.Key);
				}
			}
		}

		[TestMethod()]
		public void RandomizedSelectTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForSelection)
					continue;
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Order:[{0}]", order);
				Console.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actualItem = searching.RandomizedSelect(nodes, 0, n - 1, order);

				Console.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.IsNull(actualItem);
				else
				{
					Assert.IsNotNull(actualItem);
					Assert.AreEqual(expectedItem, actualItem.Key);
				}
			}
		}

		[TestMethod()]
		public void SelectTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForSelection)
					continue;
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Order:[{0}]", order);
				Console.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actualItem = searching.Select(nodes, 0, n - 1, order);

				Console.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.IsNull(actualItem);
				else
				{
					Assert.IsNotNull(actualItem);
					Assert.AreEqual(expectedItem, actualItem.Key);
				}
			}
		}

		[TestMethod()]
		public void OSSelectTest()
		{
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Order:[{0}]", order);
				Console.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var rbNode = (RBNode<int, object>)rbBinaryTree.Root;
				var actualItem = rbBinaryTree.Select(rbNode, order);

				Console.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.IsNull(actualItem);
				else
				{
					Assert.IsNotNull(actualItem);
					Assert.AreEqual(expectedItem, actualItem.Key);
				}
			}
		}

		[TestMethod()]
		public void OSSelectTailTest()
		{
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Order:[{0}]", order);
				Console.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var rbNode = (RBNode<int, object>)rbBinaryTree.Root;
				var actualItem = rbBinaryTree.SelectTail(rbNode, order);

				Console.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.IsNull(actualItem);
				else
				{
					Assert.IsNotNull(actualItem);
					Assert.AreEqual(expectedItem, actualItem.Key);
				}
			}
		}

		[TestMethod()]
		public void OSRankTest()
		{
			var searching = new Searching();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedRank = testCase.ExpectedRank;
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Order:[{0}]", order);
				Console.WriteLine("Expected:[Rank:{0}]", expectedRank == -1 ? "0" : expectedRank.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actualItem = searching.Select(nodes, 0, n - 1, order);

				Console.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				RBNode<int, object> rbNode = null;
				foreach (var node in rbBinaryTree)
				{
					if (actualItem.Key == node.Key)
					{
						rbNode = (RBNode<int, object>)node;
						break;
					}
				}

				Assert.IsNotNull(rbNode);
				var actualRank = rbBinaryTree.Rank(rbNode);

				Console.WriteLine("Actual:[Rank:{0}]", actualRank.ToString());

				if (expectedRank == -1)
					Assert.IsTrue(actualRank == 0);
				else
					Assert.AreEqual(expectedRank, actualRank);
			}
		}
	}
}