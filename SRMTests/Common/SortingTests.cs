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
		public T Left { get; set; }
		public T Right { get; set; }
		public List<T> Expected { get; set; }
		public T Count { get; set; }
		public int BucketLeft { get; set; }
		public int BucketRight { get; set; }
		public int BucketCount { get; set; }
		public bool IsForCounting;
		public bool IsForCountingString;
		public bool IsForRadix;
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
					Expected = new List<int> {},
					IsForCounting = true,
					IsForCountingString = true,
					IsForRadix = true,
					Left = 0,
					Right = 0,
					Count = 0,
					BucketLeft = 0,
					BucketRight = 0,
					BucketCount = 1
				},
				new SortingTestCase<int>
				{
					Name = "test case 2",
					Input = new List<int> {1, 2},
					Expected = new List<int> {1, 2},
					IsForCounting = true,
					IsForCountingString = true,
					IsForRadix = true,
					Left = 1,
					Right = 2,
					Count = 2,
					BucketLeft = 1,
					BucketRight = 2,
					BucketCount = 1
				},
				new SortingTestCase<int>
				{
					Name = "test case 3",
					Input = new List<int> {2, 1},
					Expected = new List<int> {1, 2},
					IsForCounting = true,
					IsForCountingString = true,
					IsForRadix = true,
					Left = 0,
					Right = 3,
					Count = 2,
					BucketLeft = 1,
					BucketRight = 2,
					BucketCount = 1
				},
				new SortingTestCase<int>
				{
					Name = "test case 4",
					Input = new List<int> {3, 1, 2},
					Expected = new List<int> {1, 2, 3},
					IsForCounting = true,
					IsForCountingString = true,
					IsForRadix = true,
					Left = 0,
					Right = 2,
					Count = 2,
					BucketLeft = 1,
					BucketRight = 3,
					BucketCount = 1
				},
				new SortingTestCase<int>
				{
					Name = "test case 5",
					Input = new List<int> {4, 8, 10, 1, 9, 2, 0, 6, 3, 11, 12, 13, 7, 14, 5, 15},
					Expected = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15},
					IsForCounting = true,
					IsForCountingString = true,
					IsForRadix = false,
					Left = 0,
					Right = 8,
					Count = 9,
					BucketLeft = 0,
					BucketRight = 15,
					BucketCount = 4
				},
				new SortingTestCase<int>
				{
					Name = "test case 6",
					Input = new List<int> {4, -8, 10, 1, -9, 2, 0, 6, -3, 11, 12, 13, -7, 14, -5, 15},
					Expected = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15},
					IsForCounting = true,
					IsForCountingString = false,
					IsForRadix = false,
					Left = -8,
					Right = 8,
					Count = 9,
					BucketLeft = -15,
					BucketRight = 15,
					BucketCount = 8
				},
				new SortingTestCase<int>
				{
					Name = "test case 7",
					Input = new List<int> {1, 1, 1, 1, 1, 1},
					Expected = new List<int> {1, 1, 1, 1, 1, 1},
					IsForCounting = true,
					IsForRadix = true,
					BucketLeft = 0,
					BucketRight = 2,
					BucketCount = 1
				},
				new SortingTestCase<int>
				{
					Name = "test case 8",
					Input = new List<int> {329, 457, 657, 839, 436, 720, 355},
					Expected = new List<int> {329, 355, 436, 457, 657, 720, 839},
					IsForCounting = false,
					IsForCountingString = false,
					IsForRadix = true,
					BucketLeft = 300,
					BucketRight = 900,
					BucketCount = 3
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.InsertionSort(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.SelectionSort(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.MergeSort(nodes, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.QuickSort(nodes, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.QuickSortTail(nodes, 0, n - 1);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.BubbleSort(nodes);

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], nodes[i].Key);
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

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[n - i - 1], nodes[i].Key);
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

				Console.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[n - i - 1], nodes[i].Key);
			}
		}

		[TestMethod()]
		public void BinaryTreeSortMaxTest()
		{
			foreach (var testCase in _testCases)
			{
				var binaryTree = new BinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					binaryTree.Add(input[i], "value_" + input[i].ToString());

				Console.WriteLine("Output:[{0}]", string.Join(", ", binaryTree.Select(node => node.Key)));

				int j = 0;
				foreach (var node in binaryTree)
					Assert.AreEqual(expected[j++], node.Key);
			}
		}

		[TestMethod()]
		public void BinaryTreeStackSortMaxTest()
		{
			foreach (var testCase in _testCases)
			{
				var binaryTree = new BinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					binaryTree.Add(input[i], "value_" + input[i].ToString());


				int j = 0;
				var inOrderStackEnumerator = binaryTree.GetInOrderStackEnumerator();
				while (inOrderStackEnumerator.MoveNext())
				{
					Console.WriteLine("Output:[{0}]", inOrderStackEnumerator.Current.Key);
					Assert.AreEqual(expected[j++], inOrderStackEnumerator.Current.Key);
				}
			}
		}

		[TestMethod()]
		public void RBBinaryTreeSortMaxTest()
		{
			foreach (var testCase in _testCases)
			{
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				Console.WriteLine("Output:[{0}]", string.Join(", ", rbBinaryTree.Select(node => node.Key)));

				int j = 0;
				foreach (var node in rbBinaryTree)
					Assert.AreEqual(expected[j++], node.Key);
			}
		}

		[TestMethod()]
		public void CountingSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForCounting)
					continue;
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.CountingSort(nodes, null, -15, 15);

				Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], output[i].Key);
			}
		}

		[TestMethod()]
		public void GetCountInIntervalTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForCounting)
					continue;
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Input:[Left:{0}]", testCase.Left);
				Console.WriteLine("Input:[Right:{0}]", testCase.Right);
				Console.WriteLine("Expected:[Count:{0}]", testCase.Count);

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.CountingSortForIntervalChecking(nodes, null, -15, 15);
				int count = sorting.GetCountInInterval(output, testCase.Left, testCase.Right, -15, 15);

				Console.WriteLine("Output:[{0}], [Count:{1}]", string.Join(", ", output), count);

				Assert.AreEqual(testCase.Count, count);
			}
		}

		[TestMethod()]
		public void CountingStringSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForCountingString)
					continue;
				string[] input = new string[testCase.Input.Count];
				for (int i = 0; i <testCase.Input.Count; i++)
					input[i] = testCase.Input[i].ToString();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<string, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<string, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.CountingSort(nodes, null, -15, 15);

				Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i].ToString(), output[i].Key);
			}
		}

		[TestMethod()]
		public void RadixSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForRadix)
					continue;
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.RadixSort(nodes, input.Length == 0 ? 0 : input[0].ToString().Length);

				Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i], output[i].Key);
			}
		}

		[TestMethod()]
		public void RadixSortStringTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _testCases)
			{
				if (!testCase.IsForRadix)
					continue;
				string[] input = new string[testCase.Input.Count];
				for (int i = 0; i < testCase.Input.Count; i++)
					input[i] = testCase.Input[i].ToString();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<string, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<string, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.RadixSort(nodes, input.Length == 0 ? 0 : input[0].ToString().Length, '0', '9');

				Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.AreEqual(expected[i].ToString(), output[i].Key);
			}
		}

		[TestMethod()]
		public void BucketSortTest()
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
				Console.WriteLine("Input:[BucketLeft:{0}]", testCase.BucketLeft);
				Console.WriteLine("Input:[BucketRight:{0}]", testCase.BucketRight);
				Console.WriteLine("Input:[BucketCount:{0}]", testCase.BucketCount);
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.BucketSort(nodes, testCase.BucketCount, testCase.BucketLeft, testCase.BucketRight);

				Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
				{
					Assert.AreEqual(expected[i], output[i].Key);
				}
			}
		}

		[TestMethod()]
		public void BucketSortRandomTest()
		{
			var rnd = new Random();
			var sorting = new Sorting();
			int[] input = new int[1000];
			int n = input.Length;
			for (int i = 0; i < n; i++)
				input[i] = (int)Math.Floor(rnd.NextDouble() * 1000);

			Console.WriteLine("--------------------------------------------------------");
			Console.WriteLine("Input:[{0}]", string.Join(", ", input));
			Console.WriteLine("Input:[BucketLeft:{0}]", 0);
			Console.WriteLine("Input:[BucketRight:{0}]", 1000);
			Console.WriteLine("Input:[BucketCount:{0}]", 16);

			var nodes = new List<BaseNode<int, object>>();
			for (int i = 0; i < n; i++)
				nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

			var output = sorting.BucketSort(nodes, 16, 0, 1000);

			Console.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

			for (int i = 0; i < n; i++)
			{
				if(i > 0)
					Assert.IsTrue(output[i].Key >= output[i].Key);
				else
					Assert.IsTrue(output[i].Key >= 0);
			}
		}
	}
}