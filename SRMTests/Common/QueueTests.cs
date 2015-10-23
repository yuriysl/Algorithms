using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
	class QueueTestCaseNode<T>
	{
		public int Direction { get; set; }
		public T Value { get; set; }
		public int Count { get; set; }
	}

	class QueueTestCase<T>
	{
		public string Name;
		public List<QueueTestCaseNode<T>> Nodes { get; set; }
	}

	[TestClass()]
	public class QueueTests
	{
		List<QueueTestCase<string>> _testCases;
		[TestInitialize()]
		public void TestInitialize()
		{
			_testCases = new List<QueueTestCase<string>>
			{
				new QueueTestCase<string>
				{
					Name = "test case 1",
					Nodes = new List<QueueTestCaseNode<string>>
					{
						new QueueTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_1",
							Count = 1
						},
						new QueueTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_2",
							Count = 2
						},
						new QueueTestCaseNode<string>
						{
							Direction = 1,
							Value = "value_3",
							Count = 3
						},
						new QueueTestCaseNode<string>
						{
							Direction = -1,
							Value = "value_1",
							Count = 2
						}
					}
				}
			};
		}

		[TestMethod()]
		public void QueueTest()
		{
			var queue = new Queue<string>();
			foreach (var testCase in _testCases)
			{
				int n = testCase.Nodes.Count;

				Console.WriteLine("--------------------------------------------------------");
				Console.WriteLine("[Name:{0}]", testCase.Name);
				for (int i = 0; i < n; i++)
				{
					var node = testCase.Nodes[i];
					Console.WriteLine("Node{0}:[Direction:{1}]", i, node.Direction == 1 ? "enqueue" : "Dequeue");
					string actualValue = null;
					if (node.Direction == 1)
						queue.Enqueue(node.Value);
					else
					{
						actualValue = queue.Dequeue();
						Console.WriteLine("Node{0}:[ExpectedValue:{1}], [ActualValue:{2}]", i, node.Value, actualValue);
						Assert.AreEqual(node.Value, actualValue);
					}
					int actualCount = queue.Count;
					Console.WriteLine("Node{0}:[ExpectedCount:{1}], [ActualCount:{2}]", i, node.Count, actualCount);
					Assert.AreEqual(node.Count, actualCount);
				}
			}
		}
	}
}