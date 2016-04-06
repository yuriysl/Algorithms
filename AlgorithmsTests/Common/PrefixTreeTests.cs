using System;
using System.Collections.Generic;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Algorithms.AlgorithmsTests.Common
{
	public class PrefixTreeTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public List<char> InputChars { get; set; }
		public List<int> InputFreq { get; set; }
		public List<T> Expected { get; set; }
		public T Key { get; set; }
		public int Count { get; set; }
		public bool IsForPrefix;
		public bool IsForHuffmanPrefix;
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
				},
				new PrefixTreeTestCase<string>
				{
					Name = "test case 1",
					InputChars = new List<char> {'a', 'b', 'c', 'd', 'e', 'f'},
					InputFreq = new List<int> {45, 13, 12, 16, 9, 5},
					Key = "",
					Count = 6,
					Expected = new List<string> {"0", "101", "100", "111", "1101", "1100"},
					IsForPrefix = false,
					IsForHuffmanPrefix = true
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
							var actualKey = string.Join("", m.Select(item => item.Key).ToArray());
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
							var keys = m.Select(item => string.Join("", item.KeyItems)).ToArray();
							var actualKey = string.Join("", keys);
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
		public void AddHuffmanPrefixTreeTest()
		{
			foreach (var testCase in _prefixTreeTestsFixture.TestCases)
			{
				if (!testCase.IsForHuffmanPrefix)
					continue;
				var prefixTree = new PrefixTree<string, char, object>();
				char[] inputChars = testCase.InputChars.ToArray();
				int[] inputFreq = testCase.InputFreq.ToArray();
				string key = testCase.Key;
				int count = testCase.Count;
				string[] expected = testCase.Expected.ToArray();
				int n = inputChars.Length;

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("InputChars:[{0}]", string.Join(", ", inputChars));
				_testOutputHelper.WriteLine("InputFreq:[{0}]", string.Join(", ", inputFreq));
				_testOutputHelper.WriteLine("Input:[Key:{0}]", key);
				_testOutputHelper.WriteLine("Input:[Count:{0}]", count);
				_testOutputHelper.WriteLine("Expected:[{0}]", string.Join(", ", expected));

				var huffmanCode = PrefixTree<string, char, char>.BuildHuffmanCode(inputChars, inputFreq);
				
				if (huffmanCode.Contains(key))
				{
					var matches = huffmanCode.GetMatches(key);
					Assert.Equal(count, matches.Count);

					_testOutputHelper.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						for (int i = 0; i < count; i++)
						{
							var expectedKey = expected[i];
							var expectedChar = inputChars[i];
							bool hasKey = false;
							bool hasChar = false;
							foreach (var m in matches)
							{
								var actualKey = string.Join("", m.Select(item => item.Key).ToArray());
								var actualChar = string.Join("", m.Last().Value);
								if(string.Equals(expectedKey, actualKey) && string.Equals(expectedChar.ToString(), actualChar))
								{
									_testOutputHelper.WriteLine("Output:[ActualKey:{0}]", actualKey);
									_testOutputHelper.WriteLine("Output:[ActualChar:{0}]", actualChar);
									hasKey = true;
									hasChar = true;
									break;
								}
							}
							Assert.True(hasKey);
							Assert.True(hasChar);
						}
						

					}

				}
				else
					Assert.True(false, "error");
			}
		}
	}
}