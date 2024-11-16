namespace csvgenerator;

class Program
{
    private static void Main(string[] args)
    {
        
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
        Userinput i = new Userinput();

        // Variables for user selection information
        int dataQuant;      // Stores the number of data entries required
        string selConf;     // String for selection confirmation

        Console.Clear();
        // Loop asks user how many data entries they would like to generate
        while (true)
        {
            
            Console.WriteLine("How many data entries would you like to generate?");
            
            dataQuant = i.UserInput<int>();

            Console.WriteLine("You have chosen to generate {0} data entries.\n" +
                              "Is this correct? [ Y / N ]", dataQuant );

            selConf = i.UserInput<string>();

            if (selConf is "Y" or "y") break;

        } // End of While Loop

    } // End of Main
} // End of Namespace program