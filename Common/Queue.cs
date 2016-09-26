
namespace Algorithms.Common
{
	public class Queue<TValue> : IQueue<TValue>
	{
		readonly BinaryHeap<int, TValue> _binaryHeap;

		public int Count
		{
			get
			{
				return _binaryHeap.HeapSize;
			}
		}

		public Queue()
		{
			_binaryHeap = new BinaryHeap<int, TValue>();
		}

		public TValue Dequeue()
		{
			return ((IMinHeap<int, TValue>) _binaryHeap).ExtractMin().Value;
		}

		public void Enqueue(TValue value)
		{
			((IMinHeap<int, TValue>)_binaryHeap).MinInsert(Count, value);
		}

		public void Clear()
		{
			_binaryHeap.Clear();
		}
	}
}
