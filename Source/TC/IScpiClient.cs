

namespace RadioConformanceTests.Scpi;

public interface IScpiClient
{
    void Command(string cmd);
    string Query(string query);
}



