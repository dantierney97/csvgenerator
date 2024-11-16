namespace csvgenerator;

public class AddressGenerator
{
    // This class will generate several lists that hold sections of a complete address
    private List<string> _houseNumber = new List<string>();
    private List<string> _streetName = new List<string>();
    private List<string> _city = new List<string>();
    private List<string> _county = new List<string>();
    private List<string> _postcode = new List<string>();

    // Get methods to allow other classes to use the address segments
    public List<string> GetHouseNumber() { return _houseNumber; }
    public List<string> GetStreetName() { return _streetName; }
    public List<string> GetCity() { return _city; }
    public List<string> GetCounty() { return _county; }
    public List<string> GetPostcode() { return _postcode; }

    // Method called by external classes to generate an address
    public void GenerateAddress(int quant)
    {
        _houseNumber = GenerateHouseNumber(quant);
    }

    // Method generates a list of house numbers
    private List<string> GenerateHouseNumber(int quant)
    {
        List<string> houseNumber = new List<string>();
        
        Random rnd = new Random();

        for (int i = 0; i < quant; i++)
        {
            houseNumber.Add(rnd.Next(1, 230).ToString());
        }
        return houseNumber;
    }

}