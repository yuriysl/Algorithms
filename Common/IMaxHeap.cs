namespace Algorithms.Common
{
	public interface IMaxHeap<TKey, TValue>
	{
		void BuildMax();
		void BuildMaxTail();
		void MaxHeapify(BinaryHeapNode<TKey, TValue> node);
		void MaxHeapifyTail(BinaryHeapNode<TKey, TValue> node);
		BinaryHeapNode<TKey, TValue> Max();
		BinaryHeapNode<TKey, TValue> ExtractMax();
		BinaryHeapNode<TKey, TValue> ExtractMaxTail();
		void IncreaseKey(BinaryHeapNode<TKey, TValue> node, TKey newKey);
		void MaxInsert(TKey key, TValue value);
		void MaxDelete(BinaryHeapNode<TKey, TValue> node);
	}
}
