using csvgenerator.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace csvgenerator.Services;

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
    private readonly Object _lock = new();

    public void GatherGeneratedData()
    {
        try
        {
            // Series of lock blocks to help with thread safety
            // Blocks done individually to prevent locking out the list for an extended period
            lock (_lock)
            {
                data.Add(_nameGenerator.GetNames());
            }

            lock (_lock)
            {
                data.Add(_addressGenerator.GetHouseNumber());
            }

            lock (_lock)
            {
                data.Add(_addressGenerator.GetStreetName());
            }

            lock (_lock)
            {
                data.Add(_addressGenerator.GetStreetName());
            }

            lock (_lock)
            {
                data.Add(_addressGenerator.GetCounty());
            }

            lock (_lock)
            {
                data.Add(_addressGenerator.GetPostcode());
            }
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