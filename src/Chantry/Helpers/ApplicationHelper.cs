using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
	public static class ApplicationHelper
	{
		public static Window GetMainWindow()
		{
			if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				return desktop.MainWindow ?? throw new Exception("No Application Window found.");
			}

			throw new Exception("No Application Window found.");
		}

		public static void SaveConfiguration(Configuration configuration)
		{
			string json = JsonConvert.SerializeObject(configuration, Formatting.Indented);
			File.WriteAllText(ConfigurationFilename, json);
		}

		public static Configuration LoadConfiguration()
		{
			string json = "";

			if (File.Exists(ConfigurationFilename))
				json = File.ReadAllText(ConfigurationFilename);

			if (JsonConvert.DeserializeObject<Configuration>(json) is Configuration configuration)
				return configuration;

			return new Configuration();
		}

		public static string ConfigurationFilename
		{
			get { return Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.json"); }
		}
	}
}
