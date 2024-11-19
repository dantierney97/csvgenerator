using System.Diagnostics;

namespace csvgenerator;

// Enum holds log levels for use by the logger
public enum LogLevel
{
    Info, Warning, Error
} // End of enum LogLevel

// Interface allows commands to be accessible by other classes in the apllication
public interface IDebugLog
{
    void Write(string message, LogLevel level);
} // End of interface IDebugLog

// DebugLog class that handles logging debug information
public class DebugLog : IDebugLog
{
    private readonly string _filePath = "log.txt";
    // Constructor
    public DebugLog()
    {
        Console.WriteLine("Debug Log created.");
        
        // Check if the file exists and clears it when logger is created

        if (File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, string.Empty);
        } // End of If
    } // End of Constructor
    
    // Write method writes debug message to log and console
    public void Write(string message, LogLevel level)
    {
        // Construct full log message
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] - {message}";
        // Write message to file
        File.AppendAllText(_filePath, logMessage + Environment.NewLine);
        // Write message to console
        Debug.WriteLine(logMessage);
    } // End of Write

    // Deconstructor
    ~DebugLog()
    {
        Console.WriteLine("Debug Log destroyed.");
    } // End of Deconstructor
    
} // End of DebugLog class