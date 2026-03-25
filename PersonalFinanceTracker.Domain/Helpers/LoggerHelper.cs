using Microsoft.Extensions.Logging;

namespace PersonalFinanceTracker.Domain.Helpers
{
	public static class LoggerHelper
	{
		public static void LogServiceException(
			ILogger logger, 
			Exception exception
			) => logger.LogError(exception, "An error occurred in the service layer: {Message}", exception.Message);
	}
}
