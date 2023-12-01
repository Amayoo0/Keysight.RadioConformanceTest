using Moq;
using NUnit.Framework;

using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;

public class BseTests
{
    [Test]
    public void configureCellTest(){
        // Verificar que se realiza la llamada de los comandos de configuración correctamente
        var mockScpi = new Mock<IScpiClient>();
        var bse = new BseInstrument(mockScpi.Object);
        bse.configureCell(-10, -10);
        mockScpi.Verify(v => v.Command("BSE:CELL1:FREQ -10"));
        mockScpi.Verify(v => v.Command("BSE:CELL1:POW -10"));
    }

    [Test]
    public void verifyBSEInstrumentTest(){
        // Verificar que al llamar a verifyBSEInstruments se devuelva true
        var mockScpi = new Mock<IScpiClient>();
        mockScpi.Setup(s => s.Query("BSE:*IDN?")).Returns("Keysight,BSE").Verifiable();

        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsTrue(bse.verifyBSEInstrument());
        mockScpi.Verify();
    }

    [Test]
    public void UeIsConnectedTest(){
        //Verifica que los tests dan PASS si el UE permanece conectado para cada cambio de frecuencia y potencia
        var mockScpi = new Mock<IScpiClient>();
        mockScpi.Setup(s => s.Query("BSE:CELL1:UE:CONNECTED")).Returns("1").Verifiable();

        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsTrue(bse.ueIsConnected());
        mockScpi.Verify();
    }
}