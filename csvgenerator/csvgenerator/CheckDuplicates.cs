namespace csvgenerator;

public class CheckDuplicates
{

    // Constructor
    private readonly IDebugLog _debug;
    public CheckDuplicates(IDebugLog debug)
    {
        _debug = debug;
        _debug.Write("Duplicate checker created", LogLevel.Info);
    }
    
    // Count Duplicates method
    public int CountDuplicates<T>(List<T> list) where T : notnull
    {
        // New dictionary to store every unique value and its count
        var duplicates = new Dictionary<T, int>();

        // Loop to iterate through every item in the given list
        foreach (var item in list)
        {
            // Adds 1 to the value if entry repeats
            if (duplicates.ContainsKey(item))
            {
                duplicates[item]++;
            } // End of if
            // sets value to 1 on first instance of value
            else
            {
                duplicates[item] = 1;
            } // End of else
        } // End of foreach

        // Perameter for counting number of duplicates
        int count = 0;
        
        // Counts the number of entries with a value of 2 or higher
        foreach (var duplicate in duplicates)
        {
            // Any entry with a value of 2 or more will be output to console
            if (duplicate.Value >= 2) 
            {
                count++;
            } // End of if
        } // End foreach

        return count;

    } // End of CountDuplicates

    // Finaliser
    ~CheckDuplicates()
    {
        _debug.Write("Duplicate checker destroyed", LogLevel.Warning);
    }
    
} // End of CheckDuplicates Class