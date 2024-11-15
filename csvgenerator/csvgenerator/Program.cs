namespace csvgenerator;

class Program
{
    private static void Main(string[] args)
    {
        
        bool newDataset = false;

        while (!newDataset)
        {
            Console.WriteLine("Hello, World!");
        
            // This application will be used to generate .csv files full of data.
            // This data will be created to a user specification by using a selection menu
        
            // Opening console output to explain the program
            Console.WriteLine("Welcome to CSV generator!\n" +
                              "This application will help you to generate new datasets in the .cvs file format.\n" +
                              "To begin, please confirm that you wish to create a new dataset ( Y / N )\n");

            string userInput = "userinput";
        
            try
            {
                userInput = Console.ReadLine();
            }
            catch (exception e)
            {
                Console.WriteLine(e.Message as string);
            }

            if (userInput == "userinput") continue;
            if (userInput is "Y" or "y")
            {
                newDataset = true;
            }
        }

        


    }
}