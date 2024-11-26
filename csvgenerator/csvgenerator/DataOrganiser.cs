using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class DataOrganiser
{

    private readonly IDebugLog _debug;
    private readonly IServiceProvider _serviceProvider;

    public DataOrganiser(IDebugLog debug, IServiceProvider serviceProvider)
    {
        _debug = debug;
        _serviceProvider = serviceProvider;
    } // End Constructor

    private List<List<string>> data;

    public void gatherGeneratedData()
    {
        try
        {

            NameGenerator nameGenerator = _serviceProvider.GetRequiredService<NameGenerator>();
            data.Add(nameGenerator.GetNames());
            
            AddressGenerator addressGenerator = _serviceProvider.GetRequiredService<AddressGenerator>();
            data.Add(addressGenerator.GetHouseNumber());
            data.Add(addressGenerator.GetStreetName());
            data.Add(addressGenerator.GetCity());
            data.Add(addressGenerator.GetCounty());
            data.Add(addressGenerator.GetPostcode());
        }
        catch (Exception e)
        {
            _debug.Write(e.ToString(), LogLevel.Error);
            _debug.Write(e.Message, LogLevel.Error);
        }
    }
    

    // Finaliser
    ~DataOrganiser()
    {
        _debug.Write("DataOrganiser destroyed", LogLevel.Warning);
    }
}