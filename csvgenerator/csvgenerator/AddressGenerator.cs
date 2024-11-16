namespace csvgenerator;

public class AddressGenerator
{
    // This class will generate several lists that hold sections of a complete address
    private List<string> _houseName = new List<string>();
    private List<string> _streetName = new List<string>();
    private List<string> _city = new List<string>();
    private List<string> _county = new List<string>();
    private List<string> _postcode = new List<string>();
    
    // Get methods to allow other classes to use the address segments
    public List<string> GetHouseName(){ return _houseName; }
    public List<string> GetStreetName(){ return _streetName; }
    public List<string> GetCity(){ return _city; }
    public List<string> GetCounty(){ return _county; }
    public List<string> GetPostcode(){ return _postcode; }
}