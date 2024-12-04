using System.Diagnostics;
using System.Text.Json;
using csvgenerator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator.Services;

public class AddressGenerator : IAddressGenerator
{

    private readonly IDebugLog _debug;
    private readonly IServiceProvider _serviceProvider;
    private readonly Object _lock = new();

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
    /// <inheritdoc />
    public List<string> GetHouseNumber()
    {
        lock (_lock) // Makes returning _houseNumber Thread Safe
        {
            return _houseNumber;
        }
    }
    
    /// <inheritdoc />
    public List<string> GetStreetName()
    {
        lock(_lock) // Makes returning _streetName Thread Safe
        {
            return _streetName;
        }
    }

    /// <inheritdoc />
    public List<string> GetCity()
    {
        lock (_lock) // Makes returning _city Thread Safe
        {
            return _city;
        }
    }

    /// <inheritdoc />
    public List<string> GetCounty()
    {
        lock (_lock) // Makes returning _county Thread Safe
        {
            return _county;
        }
    }

    /// <inheritdoc />
    public List<string> GetPostcode()
    {
        lock(_lock) // Makes returning _postcode Thread Safe
        {
            return _postcode;
        }
    }

    
    ///<inheritdoc />
    public void GenerateAddress(int quant)
    {
        // Stopwatch to time performance
        Stopwatch timer = new Stopwatch();
        timer.Start();

        lock (_lock) // Locks all lists whilst the data is generating, making all data generated thread safe
        {
            _houseNumber = GenerateHouseNumber(quant);
            _streetName = GenerateStreetName(quant);
            _city = GenerateCity(quant);
            _county = GenerateCounty(quant);
            _postcode = GeneratePostcode(quant);
        } // End of Lock

        timer.Stop();
        TimeSpan speed = timer.Elapsed;
        _debug.Write($"Addresses created successfully!", LogLevel.Info);
        _debug.Write($"Time taken: {speed.Milliseconds}ms", LogLevel.Info);
    } // End of Generate Address

    /// <summary>
    /// Generates a list of house numbers as strings.
    /// </summary>
    /// <param name="quant">The quantity of house numbers to generate.</param>
    /// <returns>A list of house numbers as strings, each representing a random number between 1 and 230.</returns>
    /// <remarks>
    /// This method uses a <see cref="Random"/> instance to generate house numbers.
    /// The range for house numbers is from 1 to 230, inclusive.
    /// After generation, a log entry is written to indicate that house numbers have been generated.
    /// </remarks>
    /// <example>
    /// <code>
    /// List{string} houseNumbers = GenerateHouseNumber(5);
    /// // Possible output: ["12", "89", "7", "42", "230"]
    /// </code>
    /// </example>
    private List<string> GenerateHouseNumber(int quant)
    {
        // Stores generated house numbers ready to be returned
        List<string> houseNumber = new List<string>();

        // Random number generator to generate the random numbers
        Random rnd = new Random();

        // Loop iterates upto the user's specified quantity, generating a new house number each time
        for (int i = 0; i < quant; i++)
        {
            // Adds the generated house number to the houseNumber local variable
            houseNumber.Add(rnd.Next(1, 230).ToString());
        } // End for

        // Log output to mark when the house numbers have been generated
        _debug.Write("House Numbers have been generated", LogLevel.Info);
        
        // Returns the list to the caller in GenerateAddress method
        return houseNumber;
    } // End GenerateHouseNumber

    /// <summary>
    /// Generates a list of street names as strings.
    /// </summary>
    /// <param name="quant">The quantity of street names to generate.</param>
    /// <returns>A list of street names as strings, each representing a random number between 1 and 230.</returns>
    /// <remarks>
    /// This method uses a <see cref="Random"/> instance to generate street names.
    /// The random selects a number from 0 to either StreetPrefixes.Length or StreetSuffixes.Length
    /// After generation, a log entry is written to indicate that street names have been generated.
    /// </remarks>
    /// <example>
    /// <code>
    /// List{string} streetNames = GenerateStreetName(5);
    /// // Possible output: ["Church Street", "Station Street", "Cambridge Close", "Abbey Drive", "Clarence Way"]
    /// </code>
    /// </example>
    private List<string> GenerateStreetName(int quant)
    {
        // Local list to store generated information
        List<string> streetName = new List<string>();

        Random rnd = new Random();

        // Variables for generation

        // Loop creates street names based on number required from user
        for (int i = 0; i < quant; i++)
        {
            // Generates a street prefix
            var prefix = StreetPrefixes[rnd.Next(0, StreetPrefixes.Length)];

            // Generates a street suffix
            var suffix = StreetSuffixes[rnd.Next(0, StreetSuffixes.Length)];

            // Concatenate Strings to create a complete street name
            var street = prefix + " " + suffix;

            // Add street name to list
            streetName.Add(street);
        } // End of For

        // Count number of street duplicates
        var dupCheck = _serviceProvider.GetService<CheckDuplicates>();
        if (dupCheck != null)
        {
            int duplicates = dupCheck.CountDuplicates(streetName);
            _debug.Write($"Number of duplicate Street Names: {duplicates}", LogLevel.Warning);
        }

        // Returns list to main method in class
        _debug.Write("Street Names have been generated", LogLevel.Info);
        return streetName;
    } // End of GenerateStreetName

    /// <summary>
    /// An array of common street name prefixes used to generate street names
    /// </summary>
    /// <remarks>
    /// This array will be used in conjuction with the StreetSuffixes array to return a full street name during
    /// generation.
    /// </remarks>
    private static readonly string[] StreetPrefixes =
    [
        "High", "Church", "Station", "Park", "Victoria", "King",
        "Queen", "Market", "Green", "North", "South", "London",
        "Oxford", "Baker", "Cambridge", "York", "Chester", "Bridge",
        "West", "East", "St. George's", "St. John's", "Regent", "Fleet",
        "Piccadilly", "Cannon", "Tower", "Castle", "Clarence", "Albion",
        "Hill", "Elm", "Rose", "River", "Mill", "Broad", "Windsor",
        "New", "Old", "Abbey", "Spring", "Garden", "Holly", "Maple",
        "Willow", "Saxon", "Derby", "Lancaster", "Kensington", "Hampstead"
    ];

    /// <summary>
    /// An array of common street suffixes used to create full street names.
    /// </summary>
    /// <remarks>
    /// This array will be used with StreetPrefixes to generate full street names. Some Prefixes appear several times
    /// to add weight to certain names that would increase their appearance during generation. This has been implemented
    /// to more closely reflect real world data.
    /// </remarks>
    private static readonly string[] StreetSuffixes =
    [
        // Suffixes with high weight
        "Street", "Street", "Street", "Street", "Street",
        "Road", "Road", "Road", "Road", "Road",
        "Close", "Close", "Close", "Close", "Close",

        // Suffixes with low weight
        "Avenue", "Lane", "Drive",
        "Place", "Court", "Square", "Way", "Terrace", "Crescent"
    ];

    // Method generates a list of cities

    private List<string> GenerateCity(int quant)
    {
        // Placeholder list to store generated cities
        List<string> city = new List<string>();

        Random rnd = new Random();

        for (int i = 0; i < quant; i++)
        {
            city.Add(_cities[rnd.Next(0, _cities.Length)]);
        } // End of for
        
        // Output to debug log
        _debug.Write("City generated successfully!", LogLevel.Info);

        // Returns generated list
        return city;
    } // End of GenerateCity

    // Array of Cities and Towns
    private readonly string[] _cities =
    [
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
    ]; // End of Array
     
     // Method Generates a county based on the city
     private List<string> GenerateCounty(int quant)
     {
         List<string> counties = new List<string>();

         for (int i = 0; i < quant; i++)
         {
             counties.Add(_cityCountyLookup[_city[i]]);
             
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
     readonly Dictionary<string, string> _cityCountyLookup = new Dictionary<string, string>
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
     private List<string> GeneratePostcode(int quant)
     {
         List<string> postcodes = new List<string>();

         try
         {
             for (int i = 0; i < quant; i++)
             {
                 // Using the current value of i, a partial postcode is pulled from cityPostcodeLookup
                 // based on the city name in the array _city which was generated previously
                 // This partial postcode is then sent to another method which will return a full Postcode
                 postcodes.Add(GetPostcode(_cityPostcodeLookup[_city[i]]).ToString()
                               ?? throw new InvalidOperationException());
             } // End for
         }
         catch (Exception e)
         {
             _debug.Write(e.ToString(), LogLevel.Error);
             _debug.Write(e.Message, LogLevel.Error);
             
         }

         return postcodes;
     }
     
     // Method calls opensource API to get the rest of a postcode returned
     
     private static readonly HttpClient Client = new HttpClient();

     private async Task<string> GetPostcode(string partial)
     {
         string postcode;
         string apiAddress = $"https://api.postcodes.io/postcodes/{partial}/autocomplete";
         
         // Call the API
         try
         {
             
             // API Call
             var response = await Client.GetStringAsync(apiAddress);
             
             // Deserialise JSON
             using var jsonDoc = JsonDocument.Parse(response);
             var root = jsonDoc.RootElement;
             
             // Check status of response
             if (root.GetProperty("status").GetInt32() != 200)
             {
                 _debug.Write($"API Call Failed. Check Partial Postcode: {partial}", LogLevel.Error);
                 _debug.Write($"API Response: {response}", LogLevel.Error);
                 postcode = "NULL";
                 return postcode;
             } // End of If

             List<string> postcodes = new List<string>();
             
             // Parse returned information
             foreach (var result in root.GetProperty("result").EnumerateArray())
             {
                 // Adds the postcode to the list
                 postcodes.Add(result.ToString());
             } // End of foreach
             
             // Selects a random postcode from the returned list and returns that to be stored in a list
             Random rnd = new Random();
             
             postcode = postcodes[rnd.Next(0, postcodes.Count)];

         } // End of Try
         catch (Exception e)
         {
             Console.WriteLine(e);
             _debug.Write(e.ToString(), LogLevel.Error);
             _debug.Write(e.Message, LogLevel.Error);
             throw;
         } // End of Catch

         return postcode;
     }


     // Dictionary to hold Cities and towns, and some random postcodes from that area
     private readonly Dictionary<string, string> _cityPostcodeLookup = new()
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

     // Finaliser
     ~AddressGenerator()
     {
         _debug.Write("AddressGenerator Destroyed", LogLevel.Warning);
     }



}