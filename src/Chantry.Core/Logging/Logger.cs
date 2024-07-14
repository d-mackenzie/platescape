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

		public static void Trace(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "")
		{
			Log(message, LogLevel.Trace, callerFilePath, callerLineNumber, callerMemberName);
		}

		public static void Debug(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "")
		{
			Log(message, LogLevel.Debug, callerFilePath, callerLineNumber, callerMemberName);
		}

		public static void Info(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "")
		{
			Log(message, LogLevel.Information, callerFilePath, callerLineNumber, callerMemberName);
		}

		public static void Exception(Exception ex, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "")
		{
			Log(ex.ToString(), LogLevel.Error, callerFilePath, callerLineNumber, callerMemberName);
		}

		private static void Log(string message, LogLevel logLevel, string callerFilePath, int callerLineNumber, string callerMemberName)
		{
			_logger.Log(logLevel, $"{callerFilePath}:{callerLineNumber} {callerMemberName}(): {message}");
		}
	}
}
