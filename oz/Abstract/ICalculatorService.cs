using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace oz.Abstract
{
	public interface ICalculatorService
	{
		 bool CheckRegexAndParentheses(string input);
	     bool CheckParenthesesMatch(string input);
		 string ConvertInfixToPostfix(string input);
		 double EvaluatePostfixExpression(string stringBuilder);
		string SeperateTheInput(string input);


	}

	public interface IArrayDictionary<T, E>
	{
		void Add(T key, E value);
		bool Remove(T key);
		bool ContainsKey(T key);
		bool ContainsValue(E value);
		bool IsEmpty();
		void Clear();
	}
}
