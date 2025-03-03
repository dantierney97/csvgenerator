using System.Diagnostics;
using csvgenerator.Abstractions;

namespace csvgenerator.Services;

// DebugLog class that handles logging debug information
public class DebugLog : IDebugLog
{
    
    // private readonly string _filePath = "log" + $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}" + ".txt";
    private static readonly string Dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
        "CSVGenerator");

    private static readonly string File = "log" + $"{DateTime.Now:yyyy-MM-dd HHmmss}" + ".txt";
    
    private static readonly string FilePath = Path.Combine(Dir, File);
    // Constructor
    public DebugLog()
    {
        // Check if the file exists and clears it when logger is created

        if (!Directory.Exists(Dir))
        {
            Directory.CreateDirectory(Dir);
            Write("Debugger Started, Directory Created", LogLevel.Warning);
        }
        else {Write("Debugger Started", LogLevel.Info);}
        
    } // End of Constructor
    
    // Write method writes debug message to log and console
    public void Write(string message, LogLevel level)
    {
        // Construct full log message
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] - {message}";
        // Write message to file
        System.IO.File.AppendAllText(FilePath, logMessage + Environment.NewLine);
        // Write message to console
        Debug.WriteLine(logMessage);
    } // End of Write

    // Finaliser
    ~DebugLog()
    {
        Write("Debugger Destroyed", LogLevel.Warning);
    } // End of Deconstructor
    
} // End of DebugLog class