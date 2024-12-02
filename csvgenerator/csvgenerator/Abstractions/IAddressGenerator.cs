namespace csvgenerator.Abstractions;


public interface IAddressGenerator
{
    // Method to generate a house number
    List<string> GetHouseNumber();
    
    // Method to generate a street name
    List<string> GetStreetName();
    
    // Method to generate a city name
    List<string> GetCity();
    
    // Method to generate a county name
    List<string> GetCounty();
    
    // Method to generate a postcode
    List<string> GetPostcode();
    
    /// <summary>
    /// Generates Address data, specifically a house number, street, city, county, and postcode
    /// </summary>
    /// <param name="quant">The number of address components to generate</param>
    /// <remarks>
    /// This method uses multiple Lock statements to ensure thread safety whilst the shared
    /// address components are generated and updated. This method also times how long it takes to
    /// generate the data.
    /// </remarks>
    /// <example>
    /// <code>
    /// GenerateAddress(10);
    /// </code>
    /// This will generate 10 addresses and store each part of which in its associated list
    /// </example>
    /// <exception cref="ArgumentException">Thrown if <paramref name="quant"/> is negative or zero</exception>
    void GenerateAddress(int quant);
}