namespace csvgenerator.Abstractions;


public interface IAddressGenerator
{
    /// <summary>
    /// Retrieves a list of generated house numbers in a thread-safe manner.
    /// </summary>
    /// <returns>
    /// A <see cref="List{String}"/> representing the house numbers.
    /// This list contains the house number as a string. The method is thread-safe due to the use
    /// of a lock around the shared resource.
    /// </returns>
    /// <remarks>
    /// This method ensures that multiple threads can safely access the `_houseNumber` variable at the
    /// same time without causing data corruption. The method locks the code block to ensure exclusive
    /// access to `_houseNumber` whilst it is being returned.
    /// </remarks>
    List<string> GetHouseNumber();
    
    /// <summary>
    /// Retrieves a list of generated street names in a thread-safe manner.
    /// </summary>
    /// <returns>
    /// A <see cref="List{String}"/> representing the street names.
    /// This list contains the street names as a string. This method is thread-safe due to the use of a lock block.
    /// </returns>
    /// <remarks>
    /// This method ensures that multiple threads can safely access the '_streetName' variable at the sme time without
    /// causing data corruption. The method locks the code block to ensure exclusive access to '_streetName'
    /// whilst it is being returned
    /// </remarks>
    List<string> GetStreetName();
    
    /// <summary>
    /// Retrieves a list of generated cities in a thread-safe manner.
    /// </summary>
    /// <returns>
    /// A <see cref="List{String}"/> representing the cities. This list contains the cities as strings. This method is
    /// thread-safe due to the use of a lock block
    /// </returns>
    /// <remarks>
    /// This method ensures that multiple threads can safely access the '_city' variable at the same time without
    /// causing data corruption. The method locks the code block to ensure exclusive access to '_city'
    /// whilst it is being returned
    /// </remarks>
    List<string> GetCity();
    
    /// <summary>
    /// Retrieves a list of generated counties in a thread-safe manner.
    /// </summary>
    /// <returns>
    /// A <see cref="List{String}"/> representing the counties. The list contains counties as strings. This method is
    /// thread-safe due to the use of a lock block.
    /// </returns>
    /// <remarks>
    /// This method ensures that multiple threads can safely access the '_county' variable at the same time without
    /// causing data corruption. The method locks the code block to ensure exclusive access to '_county' whilst it
    /// is being returned
    /// </remarks>
    List<string> GetCounty();
    
    /// <summary>
    /// Retrieves a list of generated postcodes in a thread-safe manner.
    /// </summary>
    /// <returns>
    /// A <see cref="List{String}"/> representing the postcodes. The list contains postcodes as strings. This method is
    /// thread-safe due to the use of a lock block.
    /// </returns>
    /// <remarks>
    /// This method ensures that multiple threads can safely access the '_postcode' variable at the same time without
    /// causing data corruption. The method locks the code block to ensure exclusive access to '_postcode' whilst it
    /// is being returned
    /// </remarks>
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