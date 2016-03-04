using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms.AlgorithmsTests.Common
{
	public class StackTestCaseNode<T>
	{
		public int Direction { get; set; }
		public T Value { get; set; }
		public int Count { get; set; }
	}

	public class StackTestCase<T>
	{
		public string Name;
		public List<StackTestCaseNode<T>> Nodes { get; set; }
	}

	public class StackTestsFixture
	{
		readonly List<StackTestCase<string>> _testCases;
		public List<StackTestCase<string>> TestCases => _testCases;

		public StackTestsFixture()
		{
			_testCases = new List<StackTestCase<string>>
			{
				new StackTestCase<string>
				{
					Name = "test case 1",
					Nodes = new List<StackTestCaseNode<string>>
					{
						new StackTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_1",
							Count = 1
						},
						new StackTestCaseNode<string>
						{
							Direction = 0,
							Value = "value_1",
							Count = 1
						},
						new StackTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_2",
							Count = 2
						},
						new StackTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_3",
							Count = 3
						},
						new StackTestCaseNode<string>
						{
							Direction = -1,
							Value = "value_3",
							Count = 2
						}
					}
				}
			};
		}
	}

	public class StackTests : IClassFixture<StackTestsFixture>
	{
		readonly StackTestsFixture _stackTestsFixture;

		public StackTests(StackTestsFixture stackTestsFixture)
		{
			_stackTestsFixture = stackTestsFixture;
		}

		[Fact]
		public void StackTest()
		{
			var stack = new Algorithms.Common.Stack<string>();
			foreach (var testCase in _stackTestsFixture.TestCases)
			{
				int n = testCase.Nodes.Count;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("[Name:{0}]", testCase.Name);
				for(int i = 0; i < n; i++)
				{
					var node = testCase.Nodes[i];
					Console.WriteLine("Node{0}:[Direction:{1}]", i, node.Direction == 1 ? "push": (node.Direction == 0 ? "peek" : "pop"));
					string actualValue;
					if (node.Direction == 1)
						stack.Push(node.Value);
					else if (node.Direction == 0)
					{
						actualValue = stack.Peek();
						Console.WriteLine("Node{0}:[ExpectedValue:{1}], [ActualValue:{2}]", i, node.Value, actualValue);
						Assert.Equal(node.Value, actualValue);
					}
					else
					{
						actualValue = stack.Pop();
						Console.WriteLine("Node{0}:[ExpectedValue:{1}], [ActualValue:{2}]", i, node.Value, actualValue);
						Assert.Equal(node.Value, actualValue);
					}
					int actualCount = stack.Count;
					Console.WriteLine("Node{0}:[ExpectedCount:{1}], [ActualCount:{2}]", i, node.Count, actualCount);
					Assert.Equal(node.Count, actualCount);
				}
			}
		}
	}
}