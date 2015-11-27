
namespace Common
{
	public class BaseNode<TKey, TValue>
	{
		int _index;
		TKey _key;
		TValue _value;

		public int Index { get { return _index; } set { _index = value; } }
		public TKey Key { get { return _key; } set { _key = value; } }
		public TValue Value { get { return _value; } set { _value = value; } }

		public BaseNode(int index, TKey key)
		{
			_index = index;
			_key = key;
		}

		public BaseNode(int index, TKey key, TValue value)
			: this(index, key)
		{
			_value = value;
		}
	}
}
