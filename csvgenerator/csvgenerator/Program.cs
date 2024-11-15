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
                userInput = Console.ReadLine();
            } // End try
            catch (Exception e)
            {
                Console.WriteLine(e.Message as string);
            } // End Catch
            
            // If statement checks that userInput is not default value and
            // that userInput shows user wants to continue with the program
            if (userInput != "userinput" && userInput is "y" or "Y")
            {
                newDataset = true;
            } // End If
        } // End While Loop (newDataset)

        


    } // End Main
} // End Namespace program