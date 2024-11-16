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
    public List<string> getHouseName(){ return _houseName; }
    public List<string> getStreetName(){ return _streetName; }
    public List<string> getCity(){ return _city; }
    public List<string> getCounty(){ return _county; }
    public List<string> getPostcode(){ return _postcode; }
}