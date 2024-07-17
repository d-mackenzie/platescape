using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Newtonsoft.Json;
using System;
using System.IO;

namespace TeethInc.Chantry.Helpers
{
	public static class ApplicationHelper
	{
		private static WindowNotificationManager? _windowNotificationManager = null;
		private static Window? _mainWindow = null;

		public static Window GetMainWindow()
		{
			if (_mainWindow is null)
			{
				if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
				{
					_mainWindow = desktop.MainWindow ?? throw new Exception("No Application Window found.");
					_windowNotificationManager = new WindowNotificationManager(_mainWindow);
				}
				else
				{
					throw new Exception("No Application Window found.");
				}
			}

			return _mainWindow;
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

		public static void ShowToast(string title, string message, NotificationType notificationType = NotificationType.Success)
		{
			_windowNotificationManager?.Show(new Notification(title, message, notificationType));
		}

	}
}
