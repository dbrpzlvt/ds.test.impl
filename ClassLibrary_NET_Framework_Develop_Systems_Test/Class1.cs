using System;
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
		public string PluginName { get; }
		public abstract string Version { get; }
		public abstract Image Image { get; }
		public abstract string Description { get; }
		public abstract int Run(int input1, int input2);
	}

	static class Plugins
	{
		interface PluginFactory
		{
			int PluginsCount { get; }
			string[] GetPluginNames { get; }
			IPlugin GetPlugin(string pluginName);
		}
	}
}
