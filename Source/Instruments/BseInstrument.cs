using RadioConformanceTests.Drivers;

namespace RadioConformanceTests.Instruments;

public class BseInstrument: IBseInstrument{

    private readonly IScpiClient scpi;
    public BseInstrument(IScpiClient scpiObject){
        this.scpi = scpiObject;
    }

    public bool verifyBSEInstrument(){
        var instrumentIdString = this.scpi.Query("BSE:*IDN?");
        return instrumentIdString == "Keysight,BSE";
    }

    public void startCell(){
        this.scpi.Command("BSE:CELL1:TECH 5G");
        this.scpi.Command("BSE:CELL1:ON");
    }

    public void configureCell(double freq, double power){
        this.scpi.Command($"BSE:CELL1:FREQ {freq}");
        this.scpi.Command($"BSE:CELL1:POW {power}");
    }

    public bool ueIsConnected(){
        return this.scpi.Query("BSE:CELL1:UE:CONNECTED") == "1";
    }

    public void endCell(){
        this.scpi.Command("BSE:CELL1:OFF");
    }

}