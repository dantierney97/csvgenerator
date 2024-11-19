using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class AddressGenerator
{

    private readonly IDebugLog _debug;
    private IServiceProvider serviceProvider;

    public AddressGenerator(IDebugLog debug)
    {
        _debug = debug;
    }
    // This class will generate several lists that hold sections of a complete address
    private List<string> _houseNumber = new List<string>();
    private List<string> _streetName = new List<string>();
    private List<string> _city = new List<string>();
    private List<string> _county = new List<string>();
    private List<string> _postcode = new List<string>();

    // Get methods to allow other classes to use the address segments
    public List<string> GetHouseNumber() { return _houseNumber; }
    public List<string> GetStreetName() { return _streetName; }
    public List<string> GetCity() { return _city; }
    public List<string> GetCounty() { return _county; }
    public List<string> GetPostcode() { return _postcode; }

    // Method called by external classes to generate an address
    public void GenerateAddress(int quant, ServiceProvider serviceProvider)
    {
        
        serviceProvider = serviceProvider;
        // Stopwatch to time performance
        Stopwatch timer = new Stopwatch();
        timer.Start();
        
        _houseNumber = GenerateHouseNumber(quant);
        _streetName = GenerateStreetName(quant);
        
        timer.Stop();
        TimeSpan speed = timer.Elapsed;
        _debug.Write($"Addresses created successfully!", LogLevel.Info);
        _debug.Write($"Time taken: {speed.Milliseconds}ms", LogLevel.Info);
    }

    // Method generates a list of house numbers
    private List<string> GenerateHouseNumber(int quant)
    {
        List<string> houseNumber = new List<string>();
        
        Random rnd = new Random();

        for (int i = 0; i < quant; i++)
        {
            houseNumber.Add(rnd.Next(1, 230).ToString());
        } // End for
        
        _debug.Write("House Numbers have been generated", LogLevel.Info);
        return houseNumber;
    } // End GenerateHouseNumber

    // Method generates a list of street names
    private List<string> GenerateStreetName(int quant)
    {
        // Local list to store generated information
        List<string> streetName = new List<string>();
        
        Random rnd = new Random();

        // Variables for generation
        string prefix;
        string suffix;
        string street;

        // Loop creates street names based on number required from user
        for (int i = 0; i < quant; i++)
        {
            // Generates a street prefix
            prefix = StreetPrefixes[rnd.Next(0, StreetPrefixes.Length)];
            
            // Generates a street suffix
            suffix = StreetSuffixes[rnd.Next(0, StreetSuffixes.Length)];

            // Concatenate Strings to create a complete street name
            street = prefix + " " + suffix;
            
            // Add street name to list
            streetName.Add(street);
        } // End of For
        
        // Count number of street duplicates
        var dupCheck = serviceProvider.GetService<CheckDuplicates>();
        int duplicates = dupCheck.CountDuplicates(streetName);
        _debug.Write($"Number of duplicate Street Names: {duplicates}", LogLevel.Warning);
        
        // Returns list to main method in class
        _debug.Write("Street Names have been generated", LogLevel.Info);
        return streetName;
    } // End of GenerateStreetName
    
    // Array of street prefixes
    private static readonly string[] StreetPrefixes = 
    { 
        "High", "Church", "Station", "Park", "Victoria", "King", 
        "Queen", "Market", "Green", "North", "South", "London", 
        "Oxford", "Baker", "Cambridge", "York", "Chester", "Bridge",
        "West", "East", "St. George's", "St. John's", "Regent", "Fleet", 
        "Piccadilly", "Cannon", "Tower", "Castle", "Clarence", "Albion",
        "Hill", "Elm", "Rose", "River", "Mill", "Broad", "Windsor", 
        "New", "Old", "Abbey", "Spring", "Garden", "Holly", "Maple", 
        "Willow", "Saxon", "Derby", "Lancaster", "Kensington", "Hampstead"
    };
    
    // Array of street suffixes
    private static readonly string[] StreetSuffixes = 
    { 
        // Suffixes with high weight
        "Street", "Street", "Street", "Street", "Street",
        "Road", "Road", "Road", "Road", "Road",
        "Close", "Close", "Close", "Close", "Close",
        
        // Suffixes with low weight
        "Avenue", "Lane", "Drive", 
        "Place", "Court", "Square", "Way", "Terrace", "Crescent" 
    };

}