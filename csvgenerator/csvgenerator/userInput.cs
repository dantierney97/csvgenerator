namespace csvgenerator;

public class Userinput
{
    public string UserInput()
    {
        string input = null;

        while (input is null)
        {
            try
            {
                input = Console.ReadLine();
            } // End Try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Please try again.");
            } // End Catch

        } // End While

        return input;
    }
}