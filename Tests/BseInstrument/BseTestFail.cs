using Moq;
using NUnit.Framework;

using RadioConformanceTests.Instruments;
using RadioConformanceTests.Drivers;

public class BseTestFail{
    [Test]
    public void notVerifyBSEInstrumentTest(){
        // Verificar que si no se detecta la BSE verifyBSEInstrument() devuelve false
        var mockScpi = new Mock<IScpiClient>();
        // Se define que mockScpi.Query devuelva vacío
        mockScpi.Setup(s => s.Query("BSE:*IDN?")).Returns("");

        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsFalse(bse.verifyBSEInstrument());
    }
    
    [Test]
    public void notUEConnected(){
        
        // Verificar que si no se detecta el usuario conectado la BSE devuelve ueIsConnected = false
        var mockScpi = new Mock<IScpiClient>();
        // Se define que mockScpi.Query devuelva 0 UE conectados
        mockScpi.Setup(s => s.Query("BSE:CELL1:UE:CONNECTED")).Returns("0");

        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsFalse(bse.ueIsConnected());
    }
}