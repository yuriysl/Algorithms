namespace Common
{
	public interface IMinHeap<TKey, TValue>
	{
		void BuildMin();
		void BuildMinTail();
		void MinHeapify(BinaryHeapNode<TKey, TValue> node);
		void MinHeapifyTail(BinaryHeapNode<TKey, TValue> node);
		BinaryHeapNode<TKey, TValue> Min();
		BinaryHeapNode<TKey, TValue> ExtractMin();
		BinaryHeapNode<TKey, TValue> ExtractMinTail();
		void DecreaseKey(BinaryHeapNode<TKey, TValue> node, TKey newKey);
		void MinInsert(TKey key, TValue value);
		void MinDelete(BinaryHeapNode<TKey, TValue> node);
	}
}
