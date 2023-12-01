using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;

namespace RadioConformanceTests.TC;

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
    private readonly IBseInstrument bse;

    public TC_Sensitivity(string bseAddress)
    {
        this.scpi = new ScpiClient(bseAddress);
        this.bse = new BseInstrument(this.scpi);
        
    }

    public TestVerdict Execute()
    {
        try
        {
            var finalVerdict = TestVerdict.Pass;
            Console.WriteLine($"TC_Sensitivity::START");

            //Verify BSE instrument
            var verify = this.bse.verifyBSEInstrument();
            if( !verify ){
                Console.WriteLine($"TC_Sensitivity::Fail Unknown instrument");
                return TestVerdict.Fail;
            }

            // Start Cell
            this.bse.startCell();


            // Frequency sweep
            uint stepCount = 1 ;
            for(double freq = cfg_StartFreq_MHz; freq <= cfg_EndFreq_MHz; freq += cfg_FreqStep_MHz)
            {
                for(double power=cfg_StartPower_DBm; power >= cfg_EndPower_DBm; power+=cfg_PowerStep_DBm)
                {
                    Console.WriteLine($"TC_Sensitivity:: Step {stepCount} Configure BSE {freq}MHz {power}dBm");
                    this.bse.configureCell(freq, power);

                    // Wait for UE to connect
                    Thread.Sleep(100);

                    if( !this.bse.ueIsConnected() ){
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
            // End Cell
            this.bse.endCell();
            Console.WriteLine($"TC_Sensitivity::END");
        }
    }

}