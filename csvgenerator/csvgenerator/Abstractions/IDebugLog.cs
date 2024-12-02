namespace csvgenerator.Abstractions;

public interface IDebugLog
{
    void Write(string message, LogLevel level);
} // End of interface IDebugLog

// Enum holds log levels for use by the logger
public enum LogLevel
{
    Info, Warning, Error
} // End of enum LogLevel