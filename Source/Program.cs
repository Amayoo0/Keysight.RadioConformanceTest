using RadioConformanceTests.TC;
using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;

namespace RadioConformanceTests;

class Program 
{
    const string BSE_ADDRESS = "10.10.10.1";
    static void Main(string[] args)
    {
        ILogger logger = new Logger();
        IScpiClient BseConnection = new ScpiClient(BSE_ADDRESS);
        IBseInstrument bse = new BseInstrument(BseConnection);
        

        logger.Info("RadioConformanceTests - TC_Sensitivity : START");
        
        TC_Sensitivity test = new TC_Sensitivity(bse, logger);
        var testVerdict = test.Execute();

        logger.Info($"RadioConformanceTests - Verdict : {testVerdict}");
        logger.Info("RadioConformanceTests - TC_Sensitivity : END");


        logger.Info("Press Q to quit..");
        while (Console.ReadKey().Key != ConsoleKey.Q);

        // Mantener concola en blanco
        logger.Reset();
        
        Environment.Exit(0);


    }
}
