﻿using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class SearchingTestCase<T>
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

	public class SearchingTestsFixture
	{
		readonly List<SearchingTestCase<int>> _testCases;
		public List<SearchingTestCase<int>> TestCases => _testCases;

		public SearchingTestsFixture()
		{
			_testCases = new List<SearchingTestCase<int>>
			{
				new SearchingTestCase<int>
				{
					Name = "test case 1",
					Input = new List<int>(),
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
	}

	public class SearchingTests : IClassFixture<SearchingTestsFixture>
	{
		SearchingTestsFixture _searchingTestsFixture;
		private readonly ITestOutputHelper _testOutputHelper;

		public SearchingTests(SearchingTestsFixture searchingTestsFixture, ITestOutputHelper testOutputHelper)
		{
			_searchingTestsFixture = searchingTestsFixture;
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void BinarySearchTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsBinarySearch)
					continue;
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expected = testCase.Expected;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Key:[{0}]", key);
				_testOutputHelper.WriteLine("Expected:[{0}]", expected == -1 ? "null" : expected.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actual = searching.BinarySearch(nodes, 0, n - 1, key);

				_testOutputHelper.WriteLine("Actual:[{0}]", actual == null ? "null" : actual.Key.ToString());

				if (expected == -1)
					Assert.Null(actual);
				else
				{
					Assert.NotNull(actual);
					Assert.Equal(expected, actual.Key);
				}
			}
		}

		[Fact]
		public void BinarySearchTailTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsBinarySearch)
					continue;
				int[] input = testCase.Input.ToArray();
				int key = testCase.Key;
				int expected = testCase.Expected;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Key:[{0}]", key);
				_testOutputHelper.WriteLine("Expected:[{0}]", expected == -1 ? "null" : expected.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actual = searching.BinarySearchTail(nodes, 0, n - 1, key);

				_testOutputHelper.WriteLine("Actual:[{0}]", actual == null ? "null" : actual.Key.ToString());

				if (expected == -1)
					Assert.Null(actual);
				else
				{
					Assert.NotNull(actual);
					Assert.Equal(expected, actual.Key);
				}
			}
		}

		[Fact]
		public void SelectMinMaxTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForMinMax)
					continue;
				int[] input = testCase.Input.ToArray();
				int expectedMin = testCase.ExpectedMin;
				int expectedMax = testCase.ExpectedMax;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Expected:[Min:{0}]", expectedMin == -1 ? "null" : expectedMin.ToString());
				_testOutputHelper.WriteLine("Expected:[Max:{0}]", expectedMax == -1 ? "null" : expectedMax.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var minMax = searching.SelectMinMax(nodes, 0, n - 1);

				_testOutputHelper.WriteLine("Actual:[Min:{0}]", minMax == null ? "null" : minMax.Item1.ToString());
				_testOutputHelper.WriteLine("Actual:[Max:{0}]", minMax == null ? "null" : minMax.Item2.ToString());

				if (expectedMin == -1)
					Assert.Null(minMax);
				else
				{
					Assert.NotNull(minMax);
					Assert.Equal(expectedMin, minMax.Item1.Key);
				}
				if (expectedMax == -1)
					Assert.Null(minMax);
				else
				{
					Assert.NotNull(minMax);
					Assert.Equal(expectedMax, minMax.Item2.Key);
				}
			}
		}

		[Fact]
		public void RandomizedSelectTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForSelection)
					continue;
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Order:[{0}]", order);
				_testOutputHelper.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actualItem = searching.RandomizedSelect(nodes, 0, n - 1, order);

				_testOutputHelper.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.Null(actualItem);
				else
				{
					Assert.NotNull(actualItem);
					Assert.Equal(expectedItem, actualItem.Key);
				}
			}
		}

		[Fact]
		public void SelectTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForSelection)
					continue;
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Order:[{0}]", order);
				_testOutputHelper.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i].ToString()));

				var actualItem = searching.Select(nodes, 0, n - 1, order);

				_testOutputHelper.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.Null(actualItem);
				else
				{
					Assert.NotNull(actualItem);
					Assert.Equal(expectedItem, actualItem.Key);
				}
			}
		}

		[Fact]
		public void OSSelectTest()
		{
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Order:[{0}]", order);
				_testOutputHelper.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var rbNode = (RBNode<int, object>)rbBinaryTree.Root;
				var actualItem = rbBinaryTree.Select(rbNode, order);

				_testOutputHelper.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.Null(actualItem);
				else
				{
					Assert.NotNull(actualItem);
					Assert.Equal(expectedItem, actualItem.Key);
				}
			}
		}

		[Fact]
		public void OSSelectTailTest()
		{
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedItem = testCase.ExpectedItem;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Order:[{0}]", order);
				_testOutputHelper.WriteLine("Expected:[Item:{0}]", expectedItem == -1 ? "null" : expectedItem.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var rbNode = (RBNode<int, object>)rbBinaryTree.Root;
				var actualItem = rbBinaryTree.SelectTail(rbNode, order);

				_testOutputHelper.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				if (expectedItem == -1)
					Assert.Null(actualItem);
				else
				{
					Assert.NotNull(actualItem);
					Assert.Equal(expectedItem, actualItem.Key);
				}
			}
		}

		[Fact]
		public void OSRankTest()
		{
			var searching = new Searching();
			foreach (var testCase in _searchingTestsFixture.TestCases)
			{
				if (!testCase.IsForSelection)
					continue;
				var rbBinaryTree = new RBBinaryTree<int, object>();
				int[] input = testCase.Input.ToArray();
				int order = testCase.Order;
				int expectedRank = testCase.ExpectedRank;
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Order:[{0}]", order);
				_testOutputHelper.WriteLine("Expected:[Rank:{0}]", expectedRank == -1 ? "0" : expectedRank.ToString());

				for (int i = 0; i < n; i++)
					rbBinaryTree.Add(input[i], "value_" + input[i].ToString());

				var nodes = new List<BaseNode<int, object>>();
				for (int i = 0; i < n; i++)
					nodes.Add(new BaseNode<int, object>(i, input[i], "value_" + input[i]));

				var actualItem = searching.Select(nodes, 0, n - 1, order);

				_testOutputHelper.WriteLine("Actual:[Item:{0}]", actualItem == null ? "null" : actualItem.Key.ToString());

				RBNode<int, object> rbNode = null;
				foreach (var node in rbBinaryTree)
				{
					if (actualItem != null && actualItem.Key == node.Key)
					{
						rbNode = (RBNode<int, object>)node;
						break;
					}
				}

				Assert.NotNull(rbNode);
				var actualRank = rbBinaryTree.Rank(rbNode);

				_testOutputHelper.WriteLine("Actual:[Rank:{0}]", actualRank.ToString());

				if (expectedRank == -1)
					Assert.True(actualRank == 0);
				else
					Assert.Equal(expectedRank, actualRank);
			}
		}

		[Fact]
		public void OSHordsTest()
		{
			var rnd = new Random();
			var rbBinaryTree = new RBBinaryTree<int, Tuple<int, int>>();
			int n = 99;
			var hords = new List<Tuple<int, int>>();
			int i = 0;
			while (i < n)
			{
				int end1 = rnd.Next(0, 360);
				int end2 = (end1 + 180) % 360;
				if (end1 == end2)
					continue;
				var hasSameHords = hords.Any(hord => hord.Item1 == end1 || hord.Item1 == end2 || hord.Item2 == end1 || hord.Item2 == end2);
				if (hasSameHords)
					continue;
				hords.Add(new Tuple<int, int>(Math.Min(end1, end2), Math.Max(end1, end2)));
				i++;
			}

			var sorting = new Sorting();
			var ends = new List<BaseNode<int, Tuple<int, int>>>();
			for (int j = 0; j < n; j++)
			{
				ends.Add(new BaseNode<int, Tuple<int, int>>(j, hords[j].Item1, hords[j]));
				ends.Add(new BaseNode<int, Tuple<int, int>>(j, hords[j].Item2, hords[j]));
			}

			sorting.QuickSort(ends, 0, ends.Count - 1);

			int intersects = 0;
			int m = 2 * n;
			for (int j = 0; j < m; j++)
			{
				bool isLeft = ends[j].Key == ends[j].Value.Item1;
				if(isLeft)
					rbBinaryTree.Add(ends[j].Key, ends[j].Value);
				else
				{
					BinaryTreeNode<int, Tuple<int, int>> replacedNode;
					var removedNode = rbBinaryTree.Remove(ends[j].Value.Item1, out replacedNode);
					if(removedNode != null && removedNode.Right != null)
						intersects += ((RBNode<int, Tuple<int, int>>)removedNode.Right).Size;
				}
			}

			_testOutputHelper.WriteLine("Output:[Intersects:{0}]", intersects);
			Assert.Equal(n * (n - 1) / 2, intersects);
		}
	}
}