using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class NameGenerator
{

    private readonly IDebugLog _debug;
    public NameGenerator(IDebugLog debug)
    {
        _debug = debug;
        debug.Write("Starting NameGenerator", LogLevel.Info);
    }
    // List to store names generated by the generator
    private List<string> _names = new List<string>();

    // Allows other methods to get the list of names
    public List<string> GetNames()
    {
        return _names;
    } // End of getNames

    // Method will generate names based on quantity specified by user
    public void GenerateNames(int quantity, ServiceProvider serviceProvider)
    {
        Random rnd = new Random(); // Initialise a RNG
        
        // Create a stopwatch to measure performance
        Stopwatch timer = new Stopwatch();
        
        timer.Start();
        
        // For loop will generate the list of names for the user
        // Loop will run until the quantity limit is reached
        for (int i = 0; i < quantity; i++)
        {
            // Selects forename from array
            var forename = _forenames[rnd.Next(0, _forenames.Length)];
            
            //Decides whether surname will be double barrelled
            bool barrel;
            string hyphenate = null!;
            if (rnd.Next(0, 5) == 4)
            {
                hyphenate = _surnames[rnd.Next(0, _surnames.Length)]; // Selects surname for double barrel
                barrel = true; // sets barrel to true for concat
            } // End of if
            else
            {
                barrel = false; // sets barrel to false for no concat
            } // End of else
            
            // selects surname from array

            // If a double barrel name has been selected, it will be addded to the end of the following selection
            string surname;
            if (barrel)
            {
                surname = _surnames[rnd.Next(0, _surnames.Length)] + "-" + hyphenate;
            } // End of If
            // If a double barrel name was not selected, a surname is generated as normal
            else
            {
                surname = _surnames[rnd.Next(0, _surnames.Length)];
            } // End of Else
            
            // merge two names together for the full name
            var fullname = forename + " " + surname;
            
            // Add name to names list
            _names.Add(fullname);
        } // End of For Loop
        
        // Confirm to user the number of names that have been generated
        _debug.Write($"The number of names generated is: {_names.Count}", LogLevel.Info);
        
        // New CheckDuplicates created
        var d = serviceProvider.GetService<CheckDuplicates>();
        
        // Counts duplicate names
        Dictionary<string, int> duplicates = d.CountDuplicates(_names);
        
        // Iterate through dictionary and count the number of duplicates
        int count = 0;
        foreach (var entry in duplicates) // Iterates through every entry in duplicates
        {
            // Any entry with a value of 2 or more will be output to console
            if (entry.Value >= 2) 
            {
                count++;
            } // End of if
        } // End of foreach
        
        // Output duplicate quantity to debug
        _debug.Write($"Number of duplicate names: {count}", LogLevel.Info);
        
        // Tell the user that the names have been created successfully
        timer.Stop();
        _debug.Write("Names Generated Successfully", LogLevel.Info);
        TimeSpan duration = timer.Elapsed;
        _debug.Write($"Time taken: {duration.TotalMilliseconds}ms", LogLevel.Info);
        
    } // End of generateNames
    
    // First name array used for name generation
    private readonly string[] _forenames =
    [
        "Amelia", "Zoey", "Penelope", "Noah", "Ellie", "Connor", "Ella", "Paisley", 
    "Austin", "Emilia", "Cooper", "Elias", "Mateo", "Caleb", "Charles", "Stella", 
    "Aubrey", "Sadie", "Hudson", "Bryson", "Santiago", "Hunter", "Hannah", "Joseph", 
    "Everly", "Nicholas", "Kennedy", "Mia", "Adrian", "Chloe", "Skylar", "Autumn", 
    "Matthew", "Dylan", "Clara", "Brayden", "James", "Isaac", "Elena", "Alexander", 
    "Robert", "Jayden", "Josiah", "Camila", "Luke", "Gianna", "Leah", "Grace", 
    "Gabriella", "Peyton", "Madeline", "Isla", "Josephine", "Ryan", "Ethan", 
    "Scarlett", "Lily", "Genesis", "Eva", "Avery", "Bella", "Jaxon", "Emma", 
    "Evan", "Aaron", "Wyatt", "Henry", "Leo", "Hailey", "Hazel", "Eliana", 
    "Brooklyn", "Samuel", "Angel", "Addison", "Violet", "Asher", "Aria", 
    "Sophia", "Eli", "Everett", "Sofia", "Sophie", "Serenity", "Levi", 
    "Jaxson", "Aurora", "Eleanor", "Nora", "Christopher", "Arya", "Nova", 
    "John", "Willow", "Logan", "Kinsley", "Gabriel", "Olivia", "Isabella", 
    "Lillian", "Alice", "Kayden", "Zoe", "Isaiah", "Parker", "Landon", "Ruby", 
    "Jordan", "William", "Savannah", "Jack", "Nevaeh", "Greyson", "Cameron", 
    "Delilah", "Elijah", "Theodore", "Andrew", "Claire", "Jacob", "Mason", 
    "Abigail", "Colton", "Oliver", "Allison", "Christian", "Easton", "Luna", 
    "Madelyn", "Xavier", "Joshua", "Ezra", "Maverick", "Audrey", "Evelyn", 
    "Ivy", "Maya", "Ian", "Piper", "Jackson", "Vivian", "Ava", "Harper", 
    "Layla", "Samantha", "Quinn", "Anthony", "Sebastian", "Benjamin", "Ariana", 
    "Riley", "Michael", "Liam", "Caroline", "Mila", "Aiden", "Victoria", 
    "Miles", "Jeremiah", "Carter", "Anna", "Adeline", "Lydia", "Jonathan", 
    "Emery", "Julian", "Aaliyah", "Natalie", "Sarah", "Carson", "Daniel", 
    "Axel", "Leonardo", "Lucy", "Owen", "Thomas", "Lucas", "Valentina", 
    "Ezekiel", "Jose", "Dominic", "Adam", "David", "Elizabeth", "Naomi", 
    "Roman", "Jameson", "Cora", "Nathan", "Madison", "Lincoln"
    ];
    
    // Surname array used for name generation
    private readonly string[] _surnames =
    [
        "Cooper", "Howard", "Reynolds", "Gordon", "McDonald", "Foster", "Hicks", "Reyes", 
    "Lopez", "Baker", "Jones", "Jackson", "Henderson", "Gonzalez", "Wagner", "Rodriguez", 
    "Mills", "Robinson", "Chen", "Cook", "Anderson", "Scott", "Wright", "Moreno", 
    "Rice", "Cruz", "Evans", "Salazar", "Bryant", "Tran", "Mason", "Vargas", 
    "King", "Woods", "Herrera", "Brown", "Ross", "Torres", "Fox", "Moore", 
    "Olson", "Thompson", "Chavez", "Cole", "Harris", "Kim", "Mendoza", "Jordan", 
    "Morgan", "Graham", "Patel", "Diaz", "Murray", "Alexander", "Aguilar", "Taylor", 
    "Roberts", "Brooks", "Romero", "Ramos", "Sanders", "Castillo", "Hayes", "Rogers", 
    "Morris", "Robertson", "Smith", "Young", "Muñoz", "Rose", "Peterson", "Powell", 
    "Jenkins", "Wood", "Weaver", "Palmer", "Soto", "Martin", "Green", "Hall", 
    "Ruiz", "Carter", "Johnson", "Meyer", "Lewis", "Price", "James", "Warren", 
    "Stephens", "West", "Walker", "Vasquez", "Ford", "Myers", "Gomez", "Garcia", 
    "Kennedy", "Owens", "Freeman", "Fisher", "Gutierrez", "Black", "Davis", 
    "Harrison", "Hughes", "Porter", "Williams", "Stone", "Griffin", "Ortiz", 
    "Tucker", "Butler", "Murphy", "Turner", "Gonzales", "Ward", "Adams", "Clark", 
    "Phillips", "Holmes", "Sullivan", "Crawford", "Miller", "Alvarez", "Cox", 
    "Simpson", "Dixon", "Long", "Hamilton", "Allen", "Daniels", "Hunter", "Perry", 
    "Lee", "Garza", "Nelson", "Kelly", "Medina", "Morales", "Hill", "Ellis", 
    "Wallace", "Ferguson", "Shaw", "Burns", "Hernandez", "Nguyen", "Thomas", 
    "Martinez", "Coleman", "Ramirez", "Snyder", "Watson", "Gibson", "Guzman", 
    "Flores", "Fernandez", "Mitchell", "Nichols", "Boyd", "Campbell", "White", 
    "Rivera", "Jimenez", "Hunt", "Silva", "Edwards", "Wilson", "Bell", "Gray", 
    "Sanchez", "Marshall", "Reed", "Richardson", "Simmons", "Bailey", "Washington", 
    "Wells", "Schmidt", "Parker", "Patterson", "Webb", "Perez", "Mendez", "Barnes", 
    "Stevens", "Russell", "Stewart", "Collins", "Castro", "Bennett", "Henry"
    ];
} // End of Class