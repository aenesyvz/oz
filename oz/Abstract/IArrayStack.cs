namespace oz.Abstract
{
	public interface IArrayStack<T>
	{
		void Push(T item);
		T Pop();
		T Peek();
		bool IsEmpty();
		void Clear();
		
	}
}
