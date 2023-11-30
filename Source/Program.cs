
using RadioConformanceTests.TC;

namespace RadioConformanceTests;

class Program 
{
    const string BSE_ADDRESS = "10.10.10.1";
    static void Main(string[] args)
    {
        Console.WriteLine("RadioConformanceTests - TC_Sensitivity : START");
        
        TC_Sensitivity test = new TC_Sensitivity(BSE_ADDRESS);
        var testVerdict = test.Execute();

        Console.WriteLine($"RadioConformanceTests - Verdict : {testVerdict}");
        Console.WriteLine("RadioConformanceTests - TC_Sensitivity : END");


        Console.WriteLine("Press Q to quit..");
        while (Console.ReadKey().Key != ConsoleKey.Q);
        Environment.Exit(0);
    }
}
