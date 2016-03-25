using System;
using System.Collections.Generic;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class PrefixTreeTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public List<T> Expected { get; set; }
		public T Key { get; set; }
		public int Count { get; set; }
		public bool IsForPrefix;
	}

	public class PrefixTreeTestsFixture
	{
		readonly List<PrefixTreeTestCase<string>> _testCases;
		public List<PrefixTreeTestCase<string>> TestCases => _testCases;

		public PrefixTreeTestsFixture()
		{
			_testCases = new List<PrefixTreeTestCase<string>>
			{
				new PrefixTreeTestCase<string>
				{
					Name = "test case 1",
					Input = new List<string> {"abc", "abcd", "abcde", "abcdef", "ab123cd", "abc132d", "abc123d"},
					Key = "abc",
					Count = 6,
					Expected = new List<string> {"abc", "abc123d", "abc132d", "abcd", "abcde", "abcdef"},
					IsForPrefix = true
				},
				new PrefixTreeTestCase<string>
				{
					Name = "test case 2",
					Input = new List<string> {"1011", "10", "011", "100", "0"},
					Key = "",
					Count = 5,
					Expected = new List<string> {"0", "011", "10", "100", "1011"},
					IsForPrefix = true
				},
				new PrefixTreeTestCase<string>
				{
					Name = "test case 3",
					Input = new List<string> {"romane", "romanus", "romulus", "rubens", "ruber", "rubicon", "rubiconus", "rubicundus"},
					Key = "rubicon",
					Count = 2,
					Expected = new List<string> {"rubicon", "rubiconus"},
					IsForPrefix = true
				}
			};
		}
	}

	public class PrefixTreeTests : IClassFixture<PrefixTreeTestsFixture>
	{
		readonly PrefixTreeTestsFixture _prefixTreeTestsFixture;
		private readonly ITestOutputHelper _testOutputHelper;

		public PrefixTreeTests(PrefixTreeTestsFixture prefixTreeTestsFixture, ITestOutputHelper testOutputHelper)
		{
			_prefixTreeTestsFixture = prefixTreeTestsFixture;
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void AddPrefixTreeTest()
		{
			foreach (var testCase in _prefixTreeTestsFixture.TestCases)
			{
				if (!testCase.IsForPrefix)
					continue;
				var prefixTree = new PrefixTree<string, char, object>();
				string[] input = testCase.Input.ToArray();
				string key= testCase.Key;
				int count = testCase.Count;
				string[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Input:[Key:{0}]", key);
				_testOutputHelper.WriteLine("Input:[Count:{0}]", count);
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					prefixTree.Add(input[i], "value_" + input[i]);

				if (prefixTree.Contains(key))
				{
					var matches = prefixTree.GetMatches(key);
					Assert.Equal(count, matches.Count);

					_testOutputHelper.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							_testOutputHelper.WriteLine("Output:[ActualKey:{0}]", actualKey);
							Assert.Equal(expected[i++], actualKey);
						}

					}

				}
				else
					Assert.True(false, "error");
			}
		}

		[Fact]
		public void AddRadixTreeTest()
		{
			foreach (var testCase in _prefixTreeTestsFixture.TestCases)
			{
				if (!testCase.IsForPrefix)
					continue;
				var radixTree = new RadixTree<string, char, object>();
				string[] input = testCase.Input.ToArray();
				string key = testCase.Key;
				int count = testCase.Count;
				string[] expected = testCase.Expected.ToArray();
				int n = input.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("Input:[Key:{0}]", key);
				_testOutputHelper.WriteLine("Input:[Count:{0}]", count);
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					radixTree.Add(input[i], "value_" + input[i]);

				if (radixTree.Contains(key))
				{
					var matches = radixTree.GetMatches(key);
					Assert.Equal(count, matches.Count);

					_testOutputHelper.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							_testOutputHelper.WriteLine("Output:[ActualKey:{0}]", actualKey);
							Assert.Equal(expected[i++], actualKey);
						}

					}

				}
				else
					Assert.True(false, "error");
			}
		}
	}
}