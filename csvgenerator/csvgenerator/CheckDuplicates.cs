namespace csvgenerator;

public class CheckDuplicates
{

    // Constructor
    public CheckDuplicates()
    {
        Console.WriteLine("Duplicate checker created");
    }
    
    // Count Duplicates method
    public static Dictionary<T, int> CountDuplicates<T>(List<T> list) where T : notnull
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

        return duplicates; // returns the dictionary
        
    } // End of CountDuplicates

    // Deconstructor
    ~CheckDuplicates()
    {
        Console.WriteLine("Duplicate checker destroyed");
    }
    
} // End of CheckDuplicates Class