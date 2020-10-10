using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ds.test.impl
{
	public interface IPlugin
	{
		string PluginName { get; }
		string Version { get; }
		Image Image { get; }
		string Description { get; }
		int Run(int input1, int input2);
	}

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
			Console.WriteLine("Введите выражение: ");
			string str = Console.ReadLine();
			Match match = Match.Empty;
			Parse(str);
			return ParseAct(match);
		}

		public static int Parse(string str)
		{
			const string RegexBr = "\\(([1234567890\\.\\+\\-\\*\\/^%]*)\\)";    // Скобки
			const string RegexNum = "[-]?\\d+\\.?\\d*";                         // Числа
			const string RegexMulOp = "[\\*\\/^%]";                             // Первоприоритетные числа
			const string RegexAddOp = "[\\+\\-]";                               // Второприоритетные числа

			// Парсинг скобок
			var matchSk = Regex.Match(str, RegexBr);
			if (matchSk.Groups.Count > 1)
			{
				string inner = matchSk.Groups[0].Value.Substring(1, matchSk.Groups[0].Value.Trim().Length - 2);
				string left = str.Substring(0, matchSk.Index);
				string right = str.Substring(matchSk.Index + matchSk.Length);

				return Parse(left + Parse(inner).ToString(CultureInfo.InvariantCulture) + right);
			}

			// Парсинг действий
			var matchMulOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexMulOp, RegexNum));
			var matchAddOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexAddOp, RegexNum));
			var match = matchMulOp.Groups.Count > 1 ? matchMulOp : matchAddOp.Groups.Count > 1 ? matchAddOp : null;
			if (match != null)
			{
				string left = str.Substring(0, match.Index);
				string right = str.Substring(match.Index + match.Length);
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
			int a = Convert.ToInt32(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
			int b = Convert.ToInt32(int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture));
			
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
