namespace RadioConformanceTests.Scpi
{
    public class ScpiClient : IScpiClient
    {
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
}