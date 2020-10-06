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

		public Image Image { get => Image; }
		private string _description;
		public string Description { get => _description; }

		public int Run(int input1, int input2)
		{
			return 0;
		}
	}
	//class Add
	//{
	//	public static Add operator +(Add input1, Add input2)
	//	{
	//		return new Add { Property.Value = input1.Value + input2.Value };
	//	}
	//}
}
