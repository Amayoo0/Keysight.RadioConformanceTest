namespace RadioConformanceTests.Instruments;

public interface IBseInstrument{
    public bool verifyBSEInstrument();
    public void startCell();
    public void configureCell(double freq, double power);
    public bool ueIsConnected();
    public void endCell();
}