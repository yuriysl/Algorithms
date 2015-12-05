namespace Algorithms.Common
{
	public interface IStack<TValue>
	{
		TValue Peek();
		TValue Pop();
		void Push(TValue value);
		void Clear();
	}
}
