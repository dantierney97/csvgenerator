using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class AddressGenerator
{

    private readonly IDebugLog _debug;
    private IServiceProvider _serviceProvider;

    public AddressGenerator(IDebugLog debug, IServiceProvider serviceProvider)
    {
        _debug = debug;
        _serviceProvider = serviceProvider;
    }

    // This class will generate several lists that hold sections of a complete address
    private List<string> _houseNumber = new List<string>();
    private List<string> _streetName = new List<string>();
    private List<string> _city = new List<string>();
    private List<string> _county = new List<string>();
    private List<string> _postcode = new List<string>();

    // Get methods to allow other classes to use the address segments
    public List<string> GetHouseNumber()
    {
        return _houseNumber;
    }

    public List<string> GetStreetName()
    {
        return _streetName;
    }

    public List<string> GetCity()
    {
        return _city;
    }

    public List<string> GetCounty()
    {
        return _county;
    }

    public List<string> GetPostcode()
    {
        return _postcode;
    }

    // Method called by external classes to generate an address
    public void GenerateAddress(int quant)
    {
        // Stopwatch to time performance
        Stopwatch timer = new Stopwatch();
        timer.Start();

        _houseNumber = GenerateHouseNumber(quant);
        _streetName = GenerateStreetName(quant);
        _city = GenerateCity(quant);
        _county = GenerateCounty(quant);

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
        var dupCheck = _serviceProvider.GetService<CheckDuplicates>();
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

    // Method generates a list of cities

    private List<string> GenerateCity(int quant)
    {
        // Placeholder list to store generated cities
        List<string> city = new List<string>();

        Random rnd = new Random();

        for (int i = 0; i < quant; i++)
        {
            city.Add(cities[rnd.Next(0, cities.Length)]);
        } // End of for
        
        // Output to debug log
        _debug.Write("City generated successfully!", LogLevel.Info);

        // Returns generated list
        return city;
    } // End of GenerateCity

    // Array of Cities and Towns
    string[] cities = new string[]
    {
        "London", "Birmingham", "Manchester", "Liverpool", "Leeds",
        "Sheffield", "Bristol", "Newcastle upon Tyne", "Nottingham", "Leicester",
        "Southampton", "Portsmouth", "Brighton", "Cambridge", "Oxford",
        "Reading", "Norwich", "Ipswich", "Exeter", "Plymouth",
        "Derby", "Coventry", "Sunderland", "Wolverhampton", "Bath",
        "York", "Blackpool", "Chester", "Luton", "Edinburgh",
        "Glasgow", "Aberdeen", "Dundee", "Inverness", "Stirling",
        "Cardiff", "Swansea", "Newport", "Wrexham", "Bangor",
        "Belfast", "Londonderry", "Lisburn", "Newry", "Milton Keynes",
        "Northampton", "Peterborough", "Gloucester", "Huddersfield", "Swindon"
    }; // End of Array
     
     // Method Generates a county based on the city
     private List<string> GenerateCounty(int quant)
     {
         List<string> counties = new List<string>();

         for (int i = 0; i < quant; i++)
         {
             counties.Add(cityCountyLookup[_city[i]]);
             
             // Check county is correct
             //_debug.Write($"{counties[i]}", LogLevel.Info);
             //if (cityCountyLookup.ContainsKey(_city[i]))
             //{
             //    _debug.Write($"{cityCountyLookup[_city[i]]}", LogLevel.Info);   
             //}
         } // End of for
         
         // Output to debug log
         _debug.Write("County generated successfully!", LogLevel.Info);
         
         return counties;
     } // End of GenerateCounty
     
     // Dictionary that looks up a city's county
     Dictionary<string, string> cityCountyLookup = new Dictionary<string, string>
     {
         // England
         { "London", "Greater London" },
         { "Birmingham", "West Midlands" },
         { "Manchester", "Greater Manchester" },
         { "Liverpool", "Merseyside" },
         { "Leeds", "West Yorkshire" },
         { "Sheffield", "South Yorkshire" },
         { "Bristol", "Bristol" },
         { "Newcastle upon Tyne", "Tyne and Wear" },
         { "Nottingham", "Nottinghamshire" },
         { "Leicester", "Leicestershire" },
         { "Southampton", "Hampshire" },
         { "Portsmouth", "Hampshire" },
         { "Brighton", "East Sussex" },
         { "Cambridge", "Cambridgeshire" },
         { "Oxford", "Oxfordshire" },
         { "Reading", "Berkshire" },
         { "Norwich", "Norfolk" },
         { "Ipswich", "Suffolk" },
         { "Exeter", "Devon" },
         { "Plymouth", "Devon" },
         { "Derby", "Derbyshire" },
         { "Coventry", "West Midlands" },
         { "Sunderland", "Tyne and Wear" },
         { "Wolverhampton", "West Midlands" },
         { "Bath", "Somerset" },
         { "York", "North Yorkshire" },
         { "Blackpool", "Lancashire" },
         { "Chester", "Cheshire" },
         { "Luton", "Bedfordshire" },

         // Scotland
         { "Edinburgh", "City of Edinburgh" },
         { "Glasgow", "Glasgow City" },
         { "Aberdeen", "Aberdeenshire" },
         { "Dundee", "Dundee City" },
         { "Inverness", "Highland" },
         { "Stirling", "Stirling" },

         // Wales
         { "Cardiff", "Cardiff" },
         { "Swansea", "Swansea" },
         { "Newport", "Newport" },
         { "Wrexham", "Wrexham" },
         { "Bangor", "Gwynedd" },

         // Northern Ireland
         { "Belfast", "County Antrim" },
         { "Londonderry", "County Londonderry" },
         { "Lisburn", "County Antrim" },
         { "Newry", "County Down" },

         // Other cities in England
         { "Milton Keynes", "Buckinghamshire" },
         { "Northampton", "Northamptonshire" },
         { "Peterborough", "Cambridgeshire" },
         { "Gloucester", "Gloucestershire" },
         { "Huddersfield", "West Yorkshire" },
         { "Swindon", "Wiltshire" } 
     }; // End of Dictionary
     
     // Method to generate a postcode using postcodes.io by providing a partial postcode
     private async Task<List<string>> GeneratePostcode(int quant)
     {
         List<string> postcodes = new List<string>();

         return postcodes;
     }
     
     // Method calls opensource API to get the rest of a postcode returned
     
     private static readonly HttpClient _client = new HttpClient();

     private async Task<string> GetPostcode(string partial)
     {
         string postcode = null;
         string apiAddress = $"https://api.postcodes.io/postcodes/{partial}/autocomplete";
         
         // Call the API
         try
         {
             
             // API Call
             var response = await _client.GetStringAsync(apiAddress);
             
             // Deserialise JSON
             using var JsonDoc = JsonDocument.Parse(response);
             var root = JsonDoc.RootElement;
             
             // Check status of response
             if (root.GetProperty("status").GetInt32() != 200)
             {
                 _debug.Write($"API Call Failed. Check Partial Postcode: {partial}", LogLevel.Error);
                 postcode = "NULL";
                 return postcode;
             } // End of If

             List<string> postcodes = new List<string>();
             
             // Parse returned information
             foreach (var result in root.GetProperty("result").EnumerateArray())
             {
                 // Addds the postcode to the list
                 postcodes.Add(result.ToString());
             } // End of foreach

         } // End of Try
         catch (Exception e)
         {
             Console.WriteLine(e);
             _debug.Write($"{e}", LogLevel.Error);
             _debug.Write($"{e.Message}", LogLevel.Error);
             throw;
         } // End of Catch

         return postcode;
     }


     // Dictionary to hold Cities and towns, and some random postcodes from that area
     private Dictionary<string, string> cityPostcodeLookup = new()
     {
         // England
         { "London", "E1" },
         { "Birmingham", "B1" },
         { "Manchester", "M1" },
         { "Liverpool", "L1" },
         { "Leeds", "LS1" },
         { "Sheffield", "S1" },
         { "Bristol", "BS1" },
         { "Newcastle upon Tyne", "NE1" },
         { "Nottingham", "NG1" },
         { "Leicester", "LE1" },
         { "Southampton", "SO14" },
         { "Portsmouth", "PO1" },
         { "Brighton", "BN1" },
         { "Cambridge", "CB1" },
         { "Oxford", "OX1" },
         { "Reading", "RG1" },
         { "Norwich", "NR1" },
         { "Ipswich", "IP1" },
         { "Exeter", "EX1" },
         { "Plymouth", "PL1" },
         { "Derby", "DE1" },
         { "Coventry", "CV1" },
         { "Sunderland", "SR1" },
         { "Wolverhampton", "WV1" },
         { "Bath", "BA1" },
         { "York", "YO1" },
         { "Blackpool", "FY1" },
         { "Chester", "CH1" },
         { "Luton", "LU1" },

         // Scotland
         { "Edinburgh", "EH1" },
         { "Glasgow", "G1" },
         { "Aberdeen", "AB10" },
         { "Dundee", "DD1" },
         { "Inverness", "IV1" },
         { "Stirling", "FK7" },

         // Wales
         { "Cardiff", "CF10" },
         { "Swansea", "SA1" },
         { "Newport", "NP10" },
         { "Wrexham", "LL11" },
         { "Bangor", "LL57" },

         // Northern Ireland
         { "Belfast", "BT1" },
         { "Londonderry", "BT47" },
         { "Lisburn", "BT28" },
         { "Newry", "BT34" },

         // Other cities in England
         { "Milton Keynes", "MK9" },
         { "Northampton", "NN1" },
         { "Peterborough", "PE1" },
         { "Gloucester", "GL1" },
         { "Huddersfield", "HD1" },
         { "Swindon", "SN1" }
     }; // End of Dictionary 



}