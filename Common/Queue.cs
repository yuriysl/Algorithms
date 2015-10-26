﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class Queue<TValue> : IQueue<TValue>
	{
		readonly BinaryHeap<int, TValue> _binaryHeap;

		public int Count => _binaryHeap.HeapSize;

		public Queue()
		{
			_binaryHeap = new BinaryHeap<int, TValue>();
		}

		public TValue Dequeue() => ((IMaxHeap<int, TValue>)_binaryHeap).ExtractMax().Value;

		public void Enqueue(TValue value)
		{
			((IMinHeap<int, TValue>)_binaryHeap).MinInsert(Count, value);
		}
	}
}
