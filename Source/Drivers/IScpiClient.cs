namespace RadioConformanceTests.Drivers;

public interface IScpiClient
{
    void Command(string cmd);
    string Query(string query);
}



