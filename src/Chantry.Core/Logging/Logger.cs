using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TeethInc.Chantry.Core.Logging
{
	public static class Logger
	{
		private static ILogger _logger;

		static Logger()
		{
			using ILoggerFactory factory = LoggerFactory.Create(
				builder => builder
					.AddFilter("Platescape", LogLevel.Debug)
					.AddConsole()
					.AddDebug());

			_logger = factory.CreateLogger("Platescape");
		}

		public static void Debug(string message, [CallerMemberName] string callerMemberName = "")
		{
			_logger.LogDebug($"{callerMemberName}: {message}");
		}

		public static void Exception(Exception ex, [CallerMemberName] string callerMemberName = "")
		{
			_logger.LogError($"{callerMemberName}: {ex.ToString()}");
		}
	}
}
