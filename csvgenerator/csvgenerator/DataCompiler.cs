using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator;

public class DataCompiler
{

    private readonly IDebugLog _debug;
    private readonly IServiceProvider _serviceProvider;
    private readonly INameGenerator _nameGenerator;
    private readonly IAddressGenerator _addressGenerator;

    public DataCompiler(IDebugLog debug, IServiceProvider serviceProvider, INameGenerator nameGenerator,
    IAddressGenerator addressGenerator)
    {
        _debug = debug;
        _serviceProvider = serviceProvider;
        _nameGenerator = nameGenerator;
        _addressGenerator = addressGenerator;
    } // End Constructor

    private List<List<string>> data = new();

    public void GatherGeneratedData()
    {
        try
        {

            data.Add(_nameGenerator.GetNames());
            
            
            data.Add(_addressGenerator.GetHouseNumber());
            data.Add(_addressGenerator.GetStreetName());
            data.Add(_addressGenerator.GetCity());
            data.Add(_addressGenerator.GetCounty());
            data.Add(_addressGenerator.GetPostcode());
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