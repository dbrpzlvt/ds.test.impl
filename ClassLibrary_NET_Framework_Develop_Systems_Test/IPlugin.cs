using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

	abstract class Property : IPlugin
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
			string line = Console.ReadLine();
			input1 = Convert.ToInt32(line.Split('+')[0]);
			input2 = Convert.ToInt32(line.Split('+')[1]);

			return Plugins.Add(input1, input2);
		}
	}
}
