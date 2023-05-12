using oz.Abstract;
using oz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace oz.Concrete
{
	public class CalculatorManagager : ICalculatorService
	{
		public bool CheckRegexAndParentheses(string input)
		{
			Regex regex = new Regex(Constant.REGEX_INPUT);
			return (regex.IsMatch(input) && CheckParenthesesMatch(input)) ? true : false;
		}

		public bool CheckParenthesesMatch(string input)
		{
			ArrayStack<char> arrayStack = new ArrayStack<char>();
			foreach (char character in input)
			{
				if (character.CompareTo('(').Equals(0))
				{
					arrayStack.Push(character);
				}
				else if (character.CompareTo(')').Equals(0))
				{
					if (arrayStack.IsEmpty() || !arrayStack.Pop().CompareTo('(').Equals(0))
					{
						return false;
					}
				}
			}
			if (!arrayStack.IsEmpty())
			{
				return false;
			}
			return true;
		}


		public string ConvertInfixToPostfix(string input)
		{
			string[] values = input.Split(' ');
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			ArrayDictionary<string, int> arrayDictionary = new ArrayDictionary<string, int>(5);

			arrayDictionary.Add("(", -1);
			arrayDictionary.Add(")", -1);
			arrayDictionary.Add("+", 2);
			arrayDictionary.Add("-", 2);
			arrayDictionary.Add("*", 3);
			arrayDictionary.Add("x", 3);
			arrayDictionary.Add("/", 3);
			arrayDictionary.Add("^", 4);

			ArrayStack<string> arrayStack = new ArrayStack<string>();

			foreach (string item in values)
			{
				if (item.Equals(""))
				{
					break;
				}
				if (arrayDictionary.ContainsKey(item))
				{
					if (arrayStack.Count == 0)
					{
						arrayStack.Push(item);
						continue;
					}

					if (item.Equals(")"))
					{
						bool found = false;
						while (!arrayStack.IsEmpty())
						{
							if (arrayStack.Peek().Equals("("))
							{
								found = true;
								arrayStack.Pop();
								break;
							}
							else
							{
								stringBuilder.Append(arrayStack.Pop());
								stringBuilder.Append(" ");
							}
						}
						if (!found && arrayStack.IsEmpty())
						{
							throw new ArgumentException("Söz dizilimi hatalı");
						}
					}
					else if (item.Equals("("))
					{
						arrayStack.Push(item);
					}
					else
					{
						if (item.Equals("^") && arrayDictionary[item] < arrayDictionary[arrayStack.Peek()])
						{
							stringBuilder.Append(arrayStack.Pop());
							stringBuilder.Append(" ");
						}
						else if (arrayDictionary[item] <= arrayDictionary[arrayStack.Peek()])
						{
							stringBuilder.Append(arrayStack.Pop());
							stringBuilder.Append(" ");
						}
						arrayStack.Push(item);
					}
				}
				else
				{
					double n;
					if (!double.TryParse(item, out n))
					{
						throw new FormatException("Söz dizilimi hatalı");
					}

					stringBuilder.Append(n);
					stringBuilder.Append(" ");
				}
			}
			while (!arrayStack.IsEmpty())
			{
				if (arrayStack.Peek().Equals(")") || arrayStack.Peek().Equals("("))
				{
					throw new ArgumentException("Söz dizilimi hatalı");
				}

				stringBuilder.Append(arrayStack.Pop());
				stringBuilder.Append(" ");
			}

			return stringBuilder.ToString().Trim();
		}

		public double EvaluatePostfixExpression(string stringBuilder)
		{
			string[] values = stringBuilder.Split(' ');
			ArrayStack<object> arrayStack = new ArrayStack<object>();
			foreach (string s in values)
			{
				double n;
				if (double.TryParse(s, out n))
				{
					arrayStack.Push(n);
				}
				else
				{
					try
					{
						double right = double.Parse(arrayStack.Pop().ToString());
						double left = double.Parse(arrayStack.Pop().ToString());

						switch (s)
						{
							case "+":
								arrayStack.Push((left + right).ToString());
								break;
							case "-":
								arrayStack.Push((left - right).ToString());
								break;
							case "/":
								arrayStack.Push((left / right).ToString());
								break;
							case "*":
								arrayStack.Push((left * right).ToString());
								break;
							case "x":
								arrayStack.Push((left * right).ToString());
								break;
							case "^":
								arrayStack.Push((Math.Pow(left, right)).ToString());
								break;
						}
					}
					catch (IndexOutOfRangeException e)
					{
						throw new FormatException("Söz dizilimi hatalı");
					}
				}
			}
			return double.Parse(arrayStack.Pop().ToString());
		}


		public string SeperateTheInput(string input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] characters = input.ToCharArray();
			bool isNumber = false;

			foreach (var item in characters)
			{
				if (Constant.OPERATORS.Contains(item))
				{
					if (isNumber)
					{
						stringBuilder.Append(" ");
						isNumber = false;
					}
					stringBuilder.Append(item);
					stringBuilder.Append(" ");
				}
				else if (item.Equals("") || item.Equals(' '))
				{
					continue;
				}
				else
				{
					stringBuilder.Append(item);
					isNumber = true;
				}
			}
			return stringBuilder.ToString().Trim();
		}
	}
}
