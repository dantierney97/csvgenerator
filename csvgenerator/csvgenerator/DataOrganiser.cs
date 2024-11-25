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

    ~DataOrganiser()
    {
        _debug.Write("DataOrganiser destroyed", LogLevel.Warning);
    }
}