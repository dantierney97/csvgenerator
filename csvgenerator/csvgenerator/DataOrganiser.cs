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
            data.Add(NameGenerator.GetNames());
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