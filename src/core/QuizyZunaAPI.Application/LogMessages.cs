using Microsoft.Extensions.Logging;

namespace QuizyZunaAPI.Presentation;

public static partial class LogMessages
{
    [LoggerMessage(Level = LogLevel.Error, Message = "Exception occurred: {Message}")]
    public static partial void LogException(this ILogger logger, string Message);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Processing Request {RequestName}")]
    public static partial void LogStartingRequest(this ILogger logger, string RequestName);
    
    [LoggerMessage(Level = LogLevel.Information, Message = "Completed Request {RequestName}")]
    public static partial void LogFinishedRequest(this ILogger logger, string RequestName);
}
