namespace csvgenerator;

class Program
{
    private static void Main(string[] args)
    {
        
        bool newDataset = false;
        string userInput = "userinput";

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
                userInput = Console.ReadLine()!;
            } // End of try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } // End of Catch
            
            // If statement checks that userInput is not default value and
            // that userInput shows user wants to continue with the program
            if (userInput != "userinput" && userInput is "y" or "Y")
            {
                newDataset = true;
            } // End of If
            else if (userInput is "n" or "N")
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
            
        } // End of While Loop (newDataset)

        // ----- Dataset Generator starts here -----

        // Variables for user selection information
        int dataQuant;

        Console.Clear();
        Console.WriteLine("How many data entries would you like to generate?");
        while (true)
        {
            
        } // End of While Loop


    } // End of Main
} // End of Namespace program