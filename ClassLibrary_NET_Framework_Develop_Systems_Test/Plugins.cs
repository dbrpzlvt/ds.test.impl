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
		//static int input1;
		//static int input2;

		public interface IPluginFactory
		{
			int PluginsCount { get; }
			string[] GetPluginNames { get; }
			IPlugin GetPlugin(string pluginName);
		}

		//public static int Add(int input1, int input2)
		//{
		//	return input1 + input2;
		//}

		//public static int Sub(int input1, int input2)
		//{
		//	return input1 - input2;
		//}

		//public static int Mlt(int input1, int input2)
		//{
		//	return input1 * input2;
		//}

		//public static int Div(int input1, int input2)
		//{
		//	return input1 / input2;
		//}
	}
}