using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	class PrefixTreeTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public List<T> Expected { get; set; }
		public T Key { get; set; }
		public int Count { get; set; }
		public bool IsForPrefix;
		public bool IsForRadix;
	}

	[TestClass()]
	public class PrefixTreeTests
	{
		List<PrefixTreeTestCase<string>> _testCases;

		[TestInitialize()]
		public void TestInitialize()
		{
			_testCases = new List<PrefixTreeTestCase<string>>()
			{
				new PrefixTreeTestCase<string>
				{
					Name = "test case 1",
					Input = new List<string> {"abc", "abcd", "abcde", "abcdef", "ab123cd", "abc132d", "abc123d"},
					Key = "abc",
					Count = 6,
					Expected = new List<string> {"abc", "abc123d", "abc132d", "abcd", "abcde", "abcdef"},
					IsForPrefix = true,
					IsForRadix = true
				},
				new PrefixTreeTestCase<string>
				{
					Name = "test case 2",
					Input = new List<string> {"1011", "10", "011", "100", "0"},
					Key = "",
					Count = 5,
					Expected = new List<string> {"0", "011", "10", "100", "1011"},
					IsForPrefix = true,
					IsForRadix = true
				},
				new PrefixTreeTestCase<string>
				{
					Name = "test case 3",
					Input = new List<string> {"romane", "romanus", "romulus", "rubens", "ruber", "rubicon", "rubiconus", "rubicundus"},
					Key = "rubicon",
					Count = 2,
					Expected = new List<string> {"rubicon", "rubiconus"},
					IsForPrefix = true,
					IsForRadix = true
				}
			};
		}

		[TestMethod()]
		public void AddPrefixTreeTest()
		{
			foreach (var testCase in _testCases)
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
					prefixTree.Add(input[i], "value_" + input[i].ToString());

				if (prefixTree.Contains(key))
				{
					var matches = prefixTree.GetMatches(key);
					Assert.AreEqual(count, matches.Count);

					Console.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							Console.WriteLine("Output:[ActualKey:{0}]", actualKey);
							Assert.AreEqual(expected[i++], actualKey);
						}

					}

				}
				else
					Assert.Fail();
			}
		}

		[TestMethod()]
		public void AddRubexTreeTest()
		{
			foreach (var testCase in _testCases)
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
					radixTree.Add(input[i], "value_" + input[i].ToString());

				if (radixTree.Contains(key))
				{
					var matches = radixTree.GetMatches(key);
					Assert.AreEqual(count, matches.Count);

					Console.WriteLine("Autocomplete:");

					if (matches.Count > 0)
					{
						int i = 0;
						foreach (var m in matches)
						{
							var actualKey = string.Join("", m);
							Console.WriteLine("Output:[ActualKey:{0}]", actualKey);
							Assert.AreEqual(expected[i++], actualKey);
						}

					}

				}
				else
					Assert.Fail();
			}
		}
	}
}