using System;
using System.Collections.Generic;
using Algorithms.Common;
using Xunit;

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

		public PrefixTreeTests(PrefixTreeTestsFixture prefixTreeTestsFixture)
		{
			_prefixTreeTestsFixture = prefixTreeTestsFixture;
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

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Input:[Key:{0}]", key);
				Console.WriteLine("Input:[Count:{0}]", count);
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					prefixTree.Add(input[i], "value_" + input[i]);

				if (prefixTree.Contains(key))
				{
					var matches = prefixTree.GetMatches(key);
					Assert.Equal(count, matches.Count);

					Console.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							Console.WriteLine("Output:[ActualKey:{0}]", actualKey);
							Assert.Equal(expected[i++], actualKey);
						}

					}

				}
				else
					Assert.True(false, "error");
			}
		}

		[Fact]
		public void AddRubexTreeTest()
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

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("Input:[Key:{0}]", key);
				Console.WriteLine("Input:[Count:{0}]", count);
				Console.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				for (int i = 0; i < n; i++)
					radixTree.Add(input[i], "value_" + input[i]);

				if (radixTree.Contains(key))
				{
					var matches = radixTree.GetMatches(key);
					Assert.Equal(count, matches.Count);

					Console.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							Console.WriteLine("Output:[ActualKey:{0}]", actualKey);
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