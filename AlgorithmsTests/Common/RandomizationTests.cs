﻿using System;
using System.Collections.Generic;
using Algorithms.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.AlgorithmsTests.Common
{
	class RandomizationTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public int LowerBound { get; set; }
		public int UpperBound { get; set; }
	}

	[TestClass()]
	public class RandomizationTests
	{
		List<RandomizationTestCase<int>> _testCases;

		[TestInitialize()]
		public void TestInitialize()
		{
			_testCases = new List<RandomizationTestCase<int>>()
			{
				new RandomizationTestCase<int>
				{
					Name = "test case 1",
					Input = new List<int>(),
					LowerBound = 0,
					UpperBound = -1
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 2",
					Input = new List<int> {1, 2},
					LowerBound = 0,
					UpperBound = 1
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 3",
					Input = new List<int> {1, 2, 3},
					LowerBound = 1,
					UpperBound = 2
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 4",
					Input = new List<int> {1, 2, 3},
					LowerBound = 0,
					UpperBound = 2
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 5",
					Input = new List<int> {1, 2, 3},
					LowerBound = 3,
					UpperBound = 2
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 6",
					Input = new List<int> {1, 2, 3},
					LowerBound = 4,
					UpperBound = -1
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 7",
					Input = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15},
					LowerBound = 6,
					UpperBound = 9
				},
				new RandomizationTestCase<int>
				{
					Name = "test case 8",
					Input = new List<int> {-9, -8, -7, -5, -3, 0, 1, 2, 4, 6, 10, 11, 12, 13, 14, 15},
					LowerBound = 0,
					UpperBound = 15
				}
			};
		}

		[TestMethod()]
		public void RandomizationInPlaceTest()
		{
			var randomization = new Randomization();
			foreach (var testCase in _testCases)
			{
				int[] input = testCase.Input.ToArray();
				int lowerBound = testCase.LowerBound;
				int upperBound = testCase.UpperBound;
				int n = input.Length;
				int[] expected = new int[n];
				for (int i = 0; i < n; i++) expected[i] = input[i];

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("Name:[{0}]", testCase.Name);
				Console.WriteLine("Input:[{0}]", string.Join(", ", input));
				Console.WriteLine("LowerBound:[{0}]", lowerBound);
				Console.WriteLine("UpperBound:[{0}]", upperBound);

				randomization.RandomizationInPlace(input, lowerBound, upperBound);

				Console.WriteLine("Actual:[{0}]", string.Join(", ", input));

				if (upperBound >= lowerBound)
				{
					for (int i = 0; i < lowerBound; i++)
					{
						Assert.AreEqual(expected[i], input[i]);
					}

					for (int i = upperBound + 1; i < n; i++)
					{
						Assert.AreEqual(expected[i], input[i]);
					}
				}
				else
				{
					for (int i = 0; i < n; i++)
					{
						Assert.AreEqual(expected[i], input[i]);
					}
				}

				for (int i = lowerBound; i <= upperBound; i++)
				{
					int actual = input[i];
					bool isExists = false;
					for (int j = lowerBound; j <= upperBound; j++)
					{
						if (expected[j].CompareTo(actual) == 0)
						{
							isExists = true;
							break;
						}
					}
					Assert.IsTrue(isExists);
				}
			}
		}
	}
}