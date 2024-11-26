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
            .AddTransient<AddressGenerator>()
            .AddTransient<NameGenerator>()
            .AddTransient<INameGenerator, NameGenerator>()      
            .AddTransient<CheckDuplicates>()    
            .AddTransient<UserInput>()    
            .AddTransient<DataCompiler>()
            .BuildServiceProvider();
        // ----- END OF DEPENDANCY INJECTION -----
        bool newDataset = false;
        string uI = "userinput";
        
        IDebugLog debug = serviceProvider.GetRequiredService<IDebugLog>();

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
        UserInput i = serviceProvider.GetRequiredService<UserInput>();

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
        
        // Asks user if they want names in the data set
        Console.WriteLine("Would you like names in your dataset? [Y / N ]");
            
        // Input exceptions are handled through this method
        selConf = i.Input<string>();

        // Runs if user wants Name Data
        if (selConf is "y" or "Y")
        {
            var nameGenerator = serviceProvider.GetRequiredService<NameGenerator>();
            // Call generate name method
            nameGenerator.GenerateNames(dataQuant); // Passes quantity & Service Collection to class
        } // End of If
        
        // Asks User if they want Address Data
        Console.WriteLine("Would you like Address information in your dataset? [Y / N ]");
        
        selConf = i.Input<string>();

        // Runs if user wants address data
        if (selConf is "Y" or "y")
        {
            // Gets AddressGenerator from a ServiceCollection
            var address = serviceProvider.GetRequiredService<AddressGenerator>();
            // Generates Address Data
            address.GenerateAddress(dataQuant);
        } // End of If
        
        // Data Compiler
        DataCompiler dataCompiler = serviceProvider.GetRequiredService<DataCompiler>();
        dataCompiler.GatherGeneratedData();
        debug.Write("All data has been compiled ready for export to CSV file.", LogLevel.Info);


    } // End of Main
} // End of Namespace program