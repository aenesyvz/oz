using oz.Abstract;

namespace oz.Core
{
	public class ArrayDictionary<T, E>:IArrayDictionary<T,E>
	{
		private T[] Keys;
		private E[] Values;
		private int Count;

		public ArrayDictionary() : this(10)
		{
		}

		public ArrayDictionary(int Capacity)
		{
			Keys = new T[Capacity];
			Values = new E[Capacity];
		}

		public void Add(T key, E value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
				
			if (Count >= Keys.Length - 1)
			{
				T[] tempKeys = new T[Keys.Length * 2];
				E[] tempValues = new E[Values.Length * 2];
				for (int i = 0; i < Keys.Length; i++)
				{
					tempKeys[i] = Keys[i];
					tempValues[i] = Values[i];
				}
				Keys = tempKeys;
				Values = tempValues;
			}
			Keys[Count] = key;
			Values[Count] = value;
			Count++;
		}

		public bool Remove(T key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			
			for (int i = 0; i < Count; i++)
			{
				if (Keys[i].Equals(key))
				{
					for (int j = i + 1; j < Count; j++)
					{
						Keys[j - 1] = Keys[j];
					}
					break;
				}
			}
			return false;
		}

		public bool ContainsKey(T key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
				
			foreach (T item in Keys)
			{
				if (item == null)
				{
					return false;
				}
					
				if (item.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsValue(E value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
				
			foreach (E item in Values)
			{
				if (item.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}

		public void Clear()
		{
			Count = 0;
			Keys = new T[Keys.Length];
			Values = new E[Keys.Length];
		}

		
		public E this[T key]
		{
			get
			{
				for (int i = 0; i < Keys.Length; i++)
				{
					if (key.Equals(Keys[i]))
					{
						return Values[i];
					}
						
				}
				throw new KeyNotFoundException();
			}
			set
			{
				for (int i = 0; i < Keys.Length; i++)
				{
					if (key.Equals(Keys[i]))
					{
						Values[i] = value;
					}
						
				}
				throw new KeyNotFoundException();
			}
		}
	}
}
