using RadioConformanceTests.Scpi;

namespace RadioConformanceTests.TC;

public enum TestVerdict
{
    None, 
    Inconclusive, 
    Pass, 
    Fail, 
    Error
}

class TC_Sensitivity 
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    private double cfg_StartFreq_MHz = 0;
    private double cfg_EndFreq_MHz = 6000;
    private double cfg_FreqStep_MHz = 500;

    private double cfg_StartPower_DBm = -10;
    private double cfg_EndPower_DBm = -90;
    private double cfg_PowerStep_DBm = -10;
    private readonly IScpiClient scpi;

    public TC_Sensitivity(string bseAddress)
    {
        this.scpi = new ScpiClient(bseAddress);
    }

    public TestVerdict Execute()
    {
        try
        {
            var finalVerdict = TestVerdict.Pass;
            Console.WriteLine($"TC_Sensitivity::START");

            //Verify BSE instrument
            var instrumentIdString = this.scpi.Query("BSE:*IDN?");
            if(instrumentIdString != "Keysight,BSE")
            {
                Console.WriteLine($"TC_Sensitivity::Fail Unknown instrument");
                return TestVerdict.Fail;
            }

            // Start Cell
            this.scpi.Command("BSE:CELL1:TECH 5G");
            this.scpi.Command("BSE:CELL1:ON");

            // Frequency sweep
            uint stepCount = 1 ;
            for(double freq = cfg_StartFreq_MHz; freq <= cfg_EndFreq_MHz; freq += cfg_FreqStep_MHz)
            {
                this.scpi.Command($"BSE:CELL1:FREQ {freq}");

                for(double power=cfg_StartPower_DBm; power >= cfg_EndPower_DBm; power+=cfg_PowerStep_DBm)
                {
                    Console.WriteLine($"TC_Sensitivity:: Step {stepCount} Configure BSE {freq}MHz {power}dBm");
                
                    this.scpi.Command($"BSE:CELL1:POW {power}");

                    // Wait for UE to connect
                    Thread.Sleep(100);

                    if(this.scpi.Query("BSE:CELL1:UE:CONNECTED") != "1")
                    {
                         Console.WriteLine($"TC_Sensitivity:: Step {stepCount} Fail");
                        finalVerdict = TestVerdict.Fail;
                    }
                    stepCount++;
                }
            }

            Console.WriteLine($"TC_Sensitivity:: {finalVerdict}");
            return finalVerdict;
        }
        catch
        {
            return TestVerdict.Error;
        }
        finally
        {
            // Start Cell
            this.scpi.Command("BSE:CELL1:OFF");
            Console.WriteLine($"TC_Sensitivity::END");
        }
    }

}