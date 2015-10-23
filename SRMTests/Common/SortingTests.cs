using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	class SortingTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public List<T> Expected { get; set; }
	}

	[TestClass()]
	public class SortingTests
	{
		private TestContext testContextInstance;

		List<SortingTestCase<int>> _testCases;

		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}

			set
			{
				testContextInstance = value;
			}
		}

		[TestInitialize()]
		public void TestInitialize()
		{
			_testCases = new List<SortingTestCase<int>>()
			{
				new SortingTestCase<int>
				{
					Name = "test case 1",
					Input = new List<int> {},
					Expected = new List<int> {}
				},
				new SortingTestCase<int>
				{
					Name = "test case 2",
					Input = new List<int> {1, 2},
					Expected = new List<int> {1, 2}
				},
				new SortingTestCase<int>
				{
					Name = "test case 3",
					Input = new List<int> {2, 1},
					Expected = new List<int> {1, 2}
				},
				new SortingTestCase<int>
				{
					Name = "test case 4",
					Input = new List<int> {3, 1, 2},
					Expected = new List<int> {1, 2, 3}
				},
				new SortingTestCase<int>
				{
					Name = "test case 5",
					Input = new List<int> {4, 8, 10, 1, 9, 2, 0, 6, 3, 11, 12, 13, 7, 14, 5, 15},
					Expected = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
				},
				new SortingTestCase<int>
				{
					Name = "test case 6",
					Input = new List<int> {4, -8, 10, 1, -9, 2, 0, 6, -3, 11, 12, 13, -7, 14, -5, 15},
					Expected = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15}
				}
				,
				new SortingTestCase<int>
				{
					Name = "test case 7",
					Input = new List<int> {1, 1, 1, 1, 1, 1},
					Expected = new List<int> {1, 1, 1, 1, 1, 1}
				}
			};
		}

		[TestMethod()]
		public void InsertionSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.InsertionSort(input);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void SelectionSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.SelectionSort(input);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void MergeSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.MergeSort(input, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void QuickSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.QuickSort(input, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void QuickSortTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.QuickSortTail(input, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void BubbleSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				sorting.BubbleSort(input);

				Console.WriteLine("Output:[{0}]", string.Join(", ", input));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], input[i]);
				}
			}
		}

		[TestMethod()]
		public void HeapSortMaxTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMax(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key).ToArray()));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], nodes[i].Key);
				}
			}
		}

		[TestMethod()]
		public void HeapSortMaxTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMaxTail(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key).ToArray()));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], nodes[i].Key);
				}
			}
		}

		[TestMethod()]
		public void HeapSortMinTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMin(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key).ToArray()));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[n - i - 1], nodes[i].Key);
				}
			}
		}

		[TestMethod()]
		public void HeapSortMinTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMinTail(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key).ToArray()));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[n - i - 1], nodes[i].Key);
				}
			}
		}
	}
}