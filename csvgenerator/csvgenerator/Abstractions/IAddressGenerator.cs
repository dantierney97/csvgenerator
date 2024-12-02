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
    
    // Method to generate the address information
    void GenerateAddress(int quant);
}