﻿namespace Common
{
	public interface IQueue<TValue>
	{
		TValue Dequeue();
		void Enqueue(TValue value);
	}
}
