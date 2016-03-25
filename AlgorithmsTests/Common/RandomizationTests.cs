using System;
using System.Collections.Generic;
using Algorithms.Common;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class RandomizationTestCase<T>
	{
		public string Name;
		public List<T> Input { get; set; }
		public int LowerBound { get; set; }
		public int UpperBound { get; set; }
	}

	public class RandomizationTestsFixture
	{
		readonly List<RandomizationTestCase<int>> _testCases;
		public List<RandomizationTestCase<int>> TestCases => _testCases;

		public RandomizationTestsFixture()
		{
			_testCases = new List<RandomizationTestCase<int>>
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
	}

	public class RandomizationTests : IClassFixture<RandomizationTestsFixture>
	{
		readonly RandomizationTestsFixture _randomizationTestsFixture;
		private readonly ITestOutputHelper _testOutputHelper;

		public RandomizationTests(RandomizationTestsFixture randomizationTestsFixture, ITestOutputHelper testOutputHelper)
		{
			_randomizationTestsFixture = randomizationTestsFixture;
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void RandomizationInPlaceTest()
		{
			var randomization = new Randomization();
			foreach (var testCase in _randomizationTestsFixture.TestCases)
			{
				int[] input = testCase.Input.ToArray();
				int lowerBound = testCase.LowerBound;
				int upperBound = testCase.UpperBound;
				int n = input.Length;
				int[] expected = new int[n];
				for (int i = 0; i < n; i++) expected[i] = input[i];

				_testOutputHelper.WriteLine("--------------------------------------------------------");
				_testOutputHelper.WriteLine("Name:[{0}]", testCase.Name);
				_testOutputHelper.WriteLine("Input:[{0}]", string.Join(", ", input));
				_testOutputHelper.WriteLine("LowerBound:[{0}]", lowerBound);
				_testOutputHelper.WriteLine("UpperBound:[{0}]", upperBound);

				randomization.RandomizationInPlace(input, lowerBound, upperBound);

				_testOutputHelper.WriteLine("Actual:[{0}]", string.Join(", ", input));

				if (upperBound >= lowerBound)
				{
					for (int i = 0; i < lowerBound; i++)
					{
						Assert.Equal(expected[i], input[i]);
					}

					for (int i = upperBound + 1; i < n; i++)
					{
						Assert.Equal(expected[i], input[i]);
					}
				}
				else
				{
					for (int i = 0; i < n; i++)
					{
						Assert.Equal(expected[i], input[i]);
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
					Assert.True(isExists);
				}
			}
		}

		[Fact]
		public void GetUniformDistributionTest()
		{
			var randomization = new Randomization();
			int zeroCount = 0;
			int n = 1000000;
			for (int i = 0; i < n; i++)
			{
				var newVal = randomization.GetUniformDistribution();
				if (newVal == 0)
					zeroCount++;
			}
			_testOutputHelper.WriteLine("Expected:[{0}+/-0.5%], Actual:[{1}]", 0.5 * n, zeroCount);
			Assert.True(Math.Abs(2 * zeroCount - n) <= 2 * 0.005 * n);
		}
	}
}