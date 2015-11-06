using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	[TestClass()]
	public class PrefixTreeTests
	{
		[TestMethod()]
		public void AddTest()
		{
			var prefixTree = new PrefixTree<string, char, object>();

			prefixTree.Add("abc", null);
			prefixTree.Add("abcd", null);
			prefixTree.Add("abcde", null);
			prefixTree.Add("abcdef", null);
			prefixTree.Add("ab123cd", null);
			prefixTree.Add("abc123d", null);
			prefixTree.Add("abc132d", null);

			string word = "abc";


			if (prefixTree.Contains(word))
			{
				var matches = prefixTree.GetMatches("abc");

				Console.WriteLine("Autocomplete:");

				if (matches.Count > 0)
					foreach (var m in matches)
						Console.WriteLine(string.Join(", ", m));
			}
			else
				Assert.Fail();
		}
	}
}