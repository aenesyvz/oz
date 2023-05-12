using oz.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oz.Core
{
	public class ArrayStack<T>:IArrayStack<T>
	{
		public T[] Stack;
		public int Count;

		public ArrayStack() : this(10)
		{
		}

		public ArrayStack(int capacity)
		{
			Stack = new T[capacity];
		}

		public void Push(T item)
		{
			if (null == item)
			{
				Console.WriteLine("Söz dizimi hatalı");
			}
			if (Count >= Stack.Length - 1)
			{
				T[] temp = new T[Stack.Length * 2];
				for (int i = 0; i < Stack.Length; i++)
				{
					temp[i] = Stack[i];
				}
				Stack = temp;
			}
			Stack[Count] = item;
			Count++;
		}

		public T Pop()
		{
			if (Count == 0)
			{

			}
			Count--;
			T ret = Stack[Count];
			Stack[Count] = default(T);
			return ret;
		}

		public T Peek()
		{
			T ret = Pop();
			Push(ret);
			return ret;
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}

		public void Clear()
		{
			Count = 0;
			Stack = new T[Stack.Length];
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = Count; i >= 0; i--)
			{
				stringBuilder.Append(Stack[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString();
		}
	}
}
