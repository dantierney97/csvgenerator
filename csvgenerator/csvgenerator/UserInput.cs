namespace csvgenerator;

public class UserInput
{

    // Constructor
    public UserInput()
    {
        Console.WriteLine("UserInput created.");
    }
    public T Input<T>()
    {
        T input; // Initialise with default value for T

        while (true)
        {
            try
            {
                string userInput = Console.ReadLine()!; // Null suppressed as exceptions are handled
            
                if (typeof(T) == typeof(string))
                {
                    // Directly cast to T if T is string
                    return (T)(object)userInput;
                } // End if

                // Convert the string input to type T
                input = (T)Convert.ChangeType(userInput, typeof(T));
                break;
            } // End of Try
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Invalid input; Please try again.");
            } // End of Catch
        } // End of While Loop

        return input;
    } // End of UserInput Method

    // Destructor
    ~UserInput()
    {
        throw new NotImplementedException();
    }
} // End of UserInput Class