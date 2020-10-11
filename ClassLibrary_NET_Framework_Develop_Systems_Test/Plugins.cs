namespace ds.test.impl
{
	public interface PluginFactory
	{
		int PluginsCount { get; }
		string[] GetPluginNames { get; }
		IPlugin GetPlugin(string pluginName);
	}

	public static class Plugins /*: IPluginFactory*/ //C0714 Релизация интерфейсов статическими классами невозможна
	{
		//int IPluginFactory.PluginsCount => throw new NotImplementedException();

		//string[] IPluginFactory.GetPluginNames => throw new NotImplementedException();

		//IPlugin IPluginFactory.GetPlugin(string pluginName)
		//{
		//	throw new NotImplementedException();
		//}
	}
}