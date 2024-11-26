using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class DataCompiler
{

    private readonly IDebugLog _debug;
    private readonly IServiceProvider _serviceProvider;
    private readonly INameGenerator _nameGenerator;

    public DataCompiler(IDebugLog debug, IServiceProvider serviceProvider, INameGenerator nameGenerator)
    {
        _debug = debug;
        _serviceProvider = serviceProvider;
        _nameGenerator = nameGenerator;
    } // End Constructor

    private List<List<string>> data;

    public void gatherGeneratedData()
    {
        try
        {

            data.Add(_nameGenerator.GetNames());
            
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
    ~DataCompiler()
    {
        _debug.Write("DataCompiler destroyed", LogLevel.Warning);
    }
}