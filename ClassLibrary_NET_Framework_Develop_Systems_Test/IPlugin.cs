using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ds.test.impl
{
	//Объявление интерфейса IPlugin
	public interface IPlugin
	{
		string PluginName { get; }
		string Version { get; }
		Image Image { get; }
		string Description { get; }

		//Метод Run(), выполняющий математические операции
		int Run(int input1, int input2);
	}

	//Реализайия инфтерфейса IPlugin в абстрактном классе Property
	public abstract class Property : IPlugin
	{
		private string _pluginName;
		public string PluginName { get => _pluginName; }

		private string _version;
		public string Version { get => _version; }

		private Image _image;
		public Image Image { get => _image; }

		private string _description;
		public string Description { get => _description; }

		public int Run(int input1, int input2)
		{
			Match match = Match.Empty;
			return ParseAct(match);
		}

		//Парсинг математических операций с помощью регулярных выражений
		public static int Parse(string str)
		{
			const string RegexBr = "\\(([1234567890\\.\\+\\-\\*\\/^%]*)\\)";    // Вычленяем операторы и операнды в скобках
			const string RegexNum = "[-]?\\d+\\.?\\d*";                         // Вычленяем все операнды
			const string RegexMulOp = "[\\*\\/^%]";                             // Сначала операции умножения и/или деления
			const string RegexAddOp = "[\\+\\-]";                               // Затем операции сложения и/или вычитания

			// Парсинг скобок
			var matchSk = Regex.Match(str, RegexBr);
			if (matchSk.Groups.Count > 1) // Если кол-во скобок больше 1 группы
			{
				string inner = matchSk.Groups[0].Value.Substring(1, matchSk.Groups[0].Value.Trim().Length - 2); // Выделяем эту скобку
				string left = str.Substring(0, matchSk.Index);
				string right = str.Substring(matchSk.Index + matchSk.Length);

				//Рекурсивное вычленение 
				return Parse(left + Parse(inner).ToString(CultureInfo.InvariantCulture) + right);
			}

			// Парсинг действий
			// Сопоставление входной строки регулярным выражением значением, полученным в Format()
			// Format() - замена трех групп первого аргумента строковыми представлениями трех последующих аргументов
			var matchMulOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexMulOp, RegexNum));
			var matchAddOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexAddOp, RegexNum));
			var match = matchMulOp.Groups.Count > 1 ? matchMulOp : matchAddOp.Groups.Count > 1 ? matchAddOp : null;
			if (match != null)
			{
				string left = str.Substring(0, match.Index);
				string right = str.Substring(match.Index + match.Length);
				//Рекурсивное вычленение
				return Parse(left + ParseAct(match).ToString(CultureInfo.InvariantCulture) + right);
			}

			// Парсинг числа
			try
			{
				return int.Parse(str, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new FormatException(string.Format("Неверная входная строка '{0}'", str));
			}
		}

		public static int ParseAct(Match match)
		{
			// Переменная a - операнд слева от знака оператора, b - справа
			int a = Convert.ToInt32(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
			int b = Convert.ToInt32(int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture));
			
			// В зависимости от того, какой оператор...
			switch (match.Groups[2].Value)
			{
				case "+": { a = a + b;  Console.WriteLine(a); return a; }
				case "-": { a = a - b; Console.WriteLine(a); return a; }
				case "*": { a = a * b; Console.WriteLine(a); return a; }
				case "/": { a = a / b; Console.WriteLine(a); return a; }
				case "^": { a = (int)Math.Pow(a, b); Console.WriteLine(a); return a; } 
				case "%": { a = a % b; Console.WriteLine(a); return a; }
				default: throw new FormatException($"Неверная входная строка '{match.Value}'");
			}
		}
	}
}
