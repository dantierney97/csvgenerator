namespace csvgenerator.Abstractions;

public interface INameGenerator
{
    
    void GenerateNames(int quantity);
    
    List<string> GetNames();
}