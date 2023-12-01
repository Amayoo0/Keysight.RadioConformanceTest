using Moq;
using NUnit.Framework;

using RadioConformanceTests.TC;
using RadioConformanceTests.Instruments;
using RadioConformanceTests.Drivers;

public class TC_SensitivityTest{
    [Test]
    public void passVerdictTest(){
        // Verifica que Verdict = pass
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(s => s.verifyBSEInstrument()).Returns(true);
        mockBse.Setup(s => s.ueIsConnected()).Returns(true);
/*      DUDA: No se deberían Mockear el resto de métodos utilizados en LC_Sensitivity?¿
        mockBse.Setup(s => s.endCell());
        mockBse.Setup(s => s.startCell());
        mockBse.Setup(s => s.configureCell()); */

        ILogger logger = new Logger();
        var test = new TC_Sensitivity(mockBse.Object, logger);

        Assert.That(test.Execute(), Is.EqualTo(TestVerdict.Pass));


    }
}