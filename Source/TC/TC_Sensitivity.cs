using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;

namespace RadioConformanceTests.TC;

public class TC_Sensitivity 
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
    private readonly IBseInstrument bse;
    private readonly ILogger logger;

    /* DUDA: Me he dado cuenta que no he refactorizado bien.
    En el creador de esta clase debería haberle pasado un objeto
    de tipo IBseInstrument para que la instancia se cree en nuestro Program.

    Me he dado cuenta mientras hacia el TestCase y no sé cómo resolverlo
    ¿debería hacerlo en un commit dentro de la rama que me he creado para 
    la creación del TestCase? Creo que no es buena práctica, pues estás 
    editando código que no es tuyo (mi tarea era crear el TestCase)...

    Debería solucionarlo en esta rama TestCase o debería comunicarle al equipo
    que realizó la Refactorización para que realicen los cambios en la rama de refactorización
    y vuelcen de nuevo los cambios a la rama main. 

    Voy a realizar la segunda opción. De no ser lo adecuado, por favor, indíquemelo.
    Gracias :) 
    */

    public TC_Sensitivity(IBseInstrument instrumentObject, ILogger loggerObject)
    {
        this.logger = loggerObject;
        this.bse    = instrumentObject;
    }

    public TestVerdict Execute()
    {
        try
        {
            var finalVerdict = TestVerdict.Pass;
            logger.Info($"TC_Sensitivity::START");

            //Verify BSE instrument
            var verify = this.bse.verifyBSEInstrument();
            if( !verify ){
                logger.Error($"TC_Sensitivity::Fail Unknown instrument");
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
                    logger.Info($"TC_Sensitivity:: Step {stepCount} Configure BSE {freq}MHz {power}dBm");
                    this.bse.configureCell(freq, power);

                    // Wait for UE to connect
                    Thread.Sleep(100);

                    if( !this.bse.ueIsConnected() ){
                        logger.Error($"TC_Sensitivity:: Step {stepCount} Fail");
                        finalVerdict = TestVerdict.Fail;
                    }
                    stepCount++;
                }
            }

            logger.Info($"TC_Sensitivity:: {finalVerdict}");
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
            logger.Info($"TC_Sensitivity::END");
        }
    }

}