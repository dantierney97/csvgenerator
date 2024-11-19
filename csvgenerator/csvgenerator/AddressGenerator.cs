namespace csvgenerator;

public class AddressGenerator
{

    private readonly IDebugLog _debug;

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
    public void GenerateAddress(int quant)
    {
        _houseNumber = GenerateHouseNumber(quant);
    }

    // Method generates a list of house numbers
    private List<string> GenerateHouseNumber(int quant)
    {
        List<string> houseNumber = new List<string>();
        
        Random rnd = new Random();

        for (int i = 0; i < quant; i++)
        {
            houseNumber.Add(rnd.Next(1, 230).ToString());
        }
        return houseNumber;
    }

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
            
            // Debug output to console
            Console.WriteLine(street);
            
            // Add street name to list
            streetName.Add(street);
        }
        
        // Returns list to main method in class
        return streetName;
    }
    
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