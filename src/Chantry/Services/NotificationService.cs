using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Logging;

namespace TeethInc.Chantry.Services
{
	internal static class NotificationService
	{

		public static void NotifyOfException(Exception ex)
		{
			Logger.Exception(ex);
		}

		public static void NotifyOfInformation(string message)
		{
			Logger.Info(message);
		}
	}
}
