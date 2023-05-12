using oz.Abstract;
using oz.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace oz
{
	class Program
	{
		static void Main(string[] args)
		{
			CalculatorManagager calculatorManagager = new CalculatorManagager();
			bool IsContinue = true;

			while (IsContinue)
			{
				Console.WriteLine("Lütfen bir girdi giriniz");
				string input = Console.ReadLine();

				bool isTrue = calculatorManagager.CheckRegexAndParentheses(input);
				if (!isTrue)
				{
					Console.WriteLine("Söz dizilimi hatalı");
					continue;
				}

				input = calculatorManagager.SeperateTheInput(input);
				Console.WriteLine("Girdiniz: " + input);
				input =  calculatorManagager.ConvertInfixToPostfix(input);
	
				double result = -1;
				try
				{
					result = calculatorManagager.EvaluatePostfixExpression(input);
				}
				catch (FormatException e)
				{
					Console.WriteLine(e.Message);
				}
				Console.WriteLine();
				Console.WriteLine("Söz dizilimi doğru : " + result);
				Console.WriteLine("Devam etmek için <Enter> tuşuna basınız\nDevam etmek istemiyorsanız farklı bir tuşa basınız");
				while (true)
				{
					if (Console.ReadKey().Key == ConsoleKey.Enter)
					{
						Console.Clear();
						break;
					}
					else
						IsContinue = false;
				}
			}
			Console.WriteLine("Çıkış Yaptınız");
		}
	}
}
