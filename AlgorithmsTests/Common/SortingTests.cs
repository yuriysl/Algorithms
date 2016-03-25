using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class SortingTestCase<T>
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

	public class SortingTestsFixture
	{
		readonly List<SortingTestCase<int>> _testCases;
		public List<SortingTestCase<int>> TestCases => _testCases;

		public SortingTestsFixture()
		{
			_testCases = new List<SortingTestCase<int>>
			{
				new SortingTestCase<int>
				{
					Name = "test case 1",
					Input = new List<int>(),
					Expected = new List<int>(),
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
	}

	public class SortingTests : IClassFixture<SortingTestsFixture>
	{
		SortingTestsFixture _sortingTestFixture;
		private readonly ITestOutputHelper _testOutputHelper;

		public SortingTests(SortingTestsFixture sortingTestFixture, ITestOutputHelper testOutputHelper)
		{
			_sortingTestFixture = sortingTestFixture;
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void InsertionSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.InsertionSort(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void SelectionSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.SelectionSort(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void MergeSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.MergeSort(nodes, 0, n - 1);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void QuickSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.QuickSort(nodes, 0, n - 1);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void QuickSortTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.QuickSortTail(nodes, 0, n - 1);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void BubbleSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.BubbleSort(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void HeapSortMaxTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMax(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void HeapSortMaxTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMaxTail(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], nodes[i].Key);
			}
		}

		[Fact]
		public void HeapSortMinTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMin(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[n - i - 1], nodes[i].Key);
			}
		}

		[Fact]
		public void HeapSortMinTailTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BinaryHeapNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BinaryHeapNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				sorting.HeapSortMinTail(nodes);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", nodes.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[n - i - 1], nodes[i].Key);
			}
		}

		[Fact]
		public void BinaryTreeSortMaxTest()
		{
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				var binaryTree = new BinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					binaryTree.Add(input[i], "value_" + input[i].ToString());

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", binaryTree.Select(node => node.Key)));

				int j = 0;
				foreach (var node in binaryTree)
					Assert.Equal(expected[j++], node.Key);
			}
		}

		[Fact]
		public void BinaryTreeStackSortMaxTest()
		{
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				var binaryTree = new BinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					binaryTree.Add(input[i], "value_" + input[i].ToString());

				int j = 0;
				var inOrderStackEnumerator = binaryTree.GetInOrderStackEnumerator();
				while (inOrderStackEnumerator.MoveNext())
				{
					_testOutputHelper.WriteLine("Output:[{0}]", inOrderStackEnumerator.Current.Key);
					Assert.Equal(expected[j++], inOrderStackEnumerator.Current.Key);
				}
			}
		}

		[Fact]
		public void RBBinaryTreeSortMaxTest()
		{
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", rbBinaryTree.Select(node => node.Key)));

				int j = 0;
				foreach (var node in rbBinaryTree)
					Assert.Equal(expected[j++], node.Key);
			}
		}

		[Fact]
		public void CountingSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				if (!testCase.IsForCounting)
					continue;
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.CountingSort(nodes, null, -15, 15);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], output[i].Key);
			}
		}

		[Fact]
		public void GetCountInIntervalTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				if (!testCase.IsForCounting)
					continue;
				int[] input = testCase.Input.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Input:[Left:{0}]", testCase.Left);
				_testOutputHelper.WriteLine("Input:[Right:{0}]", testCase.Right);
				_testOutputHelper.WriteLine("Expected:[Count:{0}]", testCase.Count);

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.CountingSortForIntervalChecking(nodes, null, -15, 15);
				int count = sorting.GetCountInInterval(output, testCase.Left, testCase.Right, -15, 15);

				_testOutputHelper.WriteLine("Output:[{0}], [Count:{1}]", string.Join(", ", output), count);

				Assert.Equal(testCase.Count, count);
			}
		}

		[Fact]
		public void CountingStringSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				if (!testCase.IsForCountingString)
					continue;
				string[] input = new string[testCase.Input.Count];
				for (int i = 0; i <testCase.Input.Count; i++)
					input[i] = testCase.Input[i].ToString();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<string, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<string, object>(i, input[i], "value_" + input[i]));

				var output = sorting.CountingSort(nodes, null, -15, 15);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i].ToString(), output[i].Key);
			}
		}

		[Fact]
		public void RadixSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				if (!testCase.IsForRadix)
					continue;
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.RadixSort(nodes, input.Length == 0 ? 0 : input[0].ToString().Length);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i], output[i].Key);
			}
		}

		[Fact]
		public void RadixSortStringTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				if (!testCase.IsForRadix)
					continue;
				string[] input = new string[testCase.Input.Count];
				for (int i = 0; i < testCase.Input.Count; i++)
					input[i] = testCase.Input[i].ToString();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<string, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<string, object>(i, input[i], "value_" + input[i]));

				var output = sorting.RadixSort(nodes, input.Length == 0 ? 0 : input[0].Length, '0', '9');

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
					Assert.Equal(expected[i].ToString(), output[i].Key);
			}
		}

		[Fact]
		public void BucketSortTest()
		{
			var sorting = new Sorting();
			foreach (var testCase in _sortingTestFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Input:[BucketLeft:{0}]", testCase.BucketLeft);
				_testOutputHelper.WriteLine("Input:[BucketRight:{0}]", testCase.BucketRight);
				_testOutputHelper.WriteLine("Input:[BucketCount:{0}]", testCase.BucketCount);
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var output = sorting.BucketSort(nodes, testCase.BucketCount, testCase.BucketLeft, testCase.BucketRight);

				_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

				for (int i = 0; i < n; i++)
				{
					Assert.Equal(expected[i], output[i].Key);
				}
			}
		}

		[Fact]
		public void BucketSortRandomTest()
		{
			var rnd = new Random();
			var sorting = new Sorting();
			int[] input = new int[1000];
			int n = input.Length;
			for (int i = 0; i < n; i++)
				input[i] = (int)Math.Floor(rnd.NextDouble() * 1000);

			_testOutputHelper.WriteLine("--------------------------------------------------------");
			_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
			_testOutputHelper.WriteLine("Input:[BucketLeft:{0}]", 0);
			_testOutputHelper.WriteLine("Input:[BucketRight:{0}]", 1000);
			_testOutputHelper.WriteLine("Input:[BucketCount:{0}]", 16);

			var nodes = new List<BaseNode<int, object>>();
			for (int i = 0; i < n; i++)
				nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

			var output = sorting.BucketSort(nodes, 16, 0, 1000);

			_testOutputHelper.WriteLine("Output:[{0}]", string.Join(", ", output.Select(node => node.Key)));

			for (int i = 0; i < n; i++)
			{
				if(i > 0)
					Assert.True(output[i].Key >= output[i-1].Key);
				else
					Assert.True(output[i].Key >= 0);
			}
		}
	}
}