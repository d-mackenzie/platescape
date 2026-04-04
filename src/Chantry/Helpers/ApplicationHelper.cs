using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using System;
using System.IO;

namespace TeethInc.Chantry.Helpers
{
	public static class ApplicationHelper
	{
		private static Lazy<Window> _mainWindow =
			new Lazy<Window>(() =>
			{
				if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
				{
					return desktop.MainWindow ?? throw new Exception("No Application Window found.");
				}
				else
				{
					throw new Exception("No Application Window found.");
				}
			});

		private static Lazy<WindowNotificationManager> _windowNotificationManager =
			new Lazy<WindowNotificationManager>(() =>
				new WindowNotificationManager(_mainWindow.Value));

		public static Window MainWindow => _mainWindow.Value;

		private static WindowNotificationManager WindowNotificationManager => _windowNotificationManager.Value;

		// public static void SaveConfiguration(Configuration configuration)
		// {
		// 	string json = JsonConvert.SerializeObject(configuration, Formatting.Indented);
		// 	File.WriteAllText(ConfigurationFilename, json);
		// }
		//
		// public static Configuration LoadConfiguration()
		// {
		// 	string json = "";
		//
		// 	if (File.Exists(ConfigurationFilename))
		// 		json = File.ReadAllText(ConfigurationFilename);
		//
		// 	if (JsonConvert.DeserializeObject<Configuration>(json) is Configuration configuration)
		// 		return configuration;
		//
		// 	return new Configuration();
		// }

		public static string ConfigurationFilename
		{
			get { return Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.json"); }
		}

		public static void ShowToast(string title, string message, NotificationType notificationType = NotificationType.Success)
		{
			WindowNotificationManager.Show(new Notification(title, message, notificationType));
		}

	}
}
