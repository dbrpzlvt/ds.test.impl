using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds.test.impl
{
	public static class Plugins
	{
		public interface IPluginFactory
		{
			int PluginsCount { get; }
			string[] GetPluginNames { get; }
			IPlugin GetPlugin(string pluginName);
		}

		public static double Add(Plugins.IPluginFactory int input1, Plugins.IPluginFactory int input2)
		{
			return input1 + input2;
		}
	}


	//	Console.WriteLine("Какой-то текст");
	//	Console.ReadKey();
	//}
}