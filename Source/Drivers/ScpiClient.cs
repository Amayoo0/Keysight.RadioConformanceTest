namespace RadioConformanceTests.Drivers;
public class ScpiClient : IScpiClient
{
    /// <summary>
    /// Standard Commands for Programmable Instruments
    /// </summary>
    public ScpiClient(string addr)
    {
    }
    
    public void Command(string cmd)
    {
    }

    public string Query(string query)
    {
        return "";
    }

}
