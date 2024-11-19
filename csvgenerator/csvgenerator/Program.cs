using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

class Program
{
    private static void Main()
    {
        
        // ----- DEPENDANCY INJECTION SECTION -----
        
        // Setting up the DI Container

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IDebugLog>(provider => new DebugLog())
            // Add to classes here --->
            .AddTransient<AddressGenerator>()   // Allows AddressGenerator to use debugger
            .AddTransient<NameGenerator>()      // Allows NameGenerator to use debugger 
            .AddTransient<CheckDuplicates>()    // Allows CheckDuplicates to use debugger
            .AddTransient<UserInput>()          // Allows UserInput to use debugger    
            .BuildServiceProvider();
        // ----- END OF DEPENDANCY INJECTION -----
        
        bool newDataset = false;
        string uI = "userinput";

        // Loop asks user if they want to generate a data set
        while (!newDataset)
        {
            Console.WriteLine("Hello, World!");
        
            // This application will be used to generate .csv files full of data.
            // This data will be created to a user specification by using a selection menu
        
            // Opening console output to explain the program
            Console.WriteLine("Welcome to CSV generator!\n" +
                              "This application will help you to generate new datasets in the .cvs file format.\n" +
                              "To begin, please confirm that you wish to create a new dataset ( Y / N )\n");
        
            try
            {
                uI = Console.ReadLine()!;
            } // End of try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } // End of Catch
            
            // If statement checks that uI is not default value and
            // that uI shows user wants to continue with the program
            if (uI != "uI" && uI is "y" or "Y")
            {
                newDataset = true;
            } // End of If
            else if (uI is "n" or "N")
            {
                Console.WriteLine("No dataset needed, application is closing!");
                break; // Ends loop and the program is the user selects N
            } // End of else if
            else
            {
                // Asks user to try again if the input isn't an expected outcome
                Console.Clear();
                Console.WriteLine("Invalid input, please try again."); 
            } // End of else
            
        } // End of While Loop

        // ----- Dataset Generator starts here -----

        // Initialise UserInput object to receive user input
        // This class deals with the exception handling of the input
        // which will reduce the number of try/catch blocks in this code
        UserInput i = new UserInput();

        // Variables for user selection information
        int dataQuant;      // Stores the number of data entries required
        string selConf;     // String for selection confirmation

        Console.Clear();
        // Loop asks user how many data entries they would like to generate
        while (true)
        {
            
            Console.WriteLine("How many data entries would you like to generate?");
            
            dataQuant = i.Input<int>();

            Console.WriteLine("You have chosen to generate {0} data entries.\n" +
                              "Is this correct? [ Y / N ]", dataQuant );

            selConf = i.Input<string>();

            if (selConf is "Y" or "y") break;

        } // End of While Loop
        
        // Loop asks user if they want names in the data set
        while (true)
        {
            Console.WriteLine("Would you like names in your dataset? [Y / N ]");
            
            // Input exceptions are handled through this method
            selConf = i.Input<string>();
            
            if (selConf is "n" or "N") break; // If user selects n, moves onto next data section
            
            // Initialise NameGenerator
            var names = serviceProvider.GetService<NameGenerator>();
            
            // Call generate name method
            names.GenerateNames(dataQuant);

            break;

        } // End of While Loop

    } // End of Main
} // End of Namespace program