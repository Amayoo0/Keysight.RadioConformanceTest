using Moq;
using NUnit.Framework;

using RadioConformanceTests.TC;
using RadioConformanceTests.Instruments;

public class TC_SensitivityTest{
    [Test]
    public void passVerdictTest(){
        // Verifica que Verdict = pass
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(s => s.verifyBSEInstrument()).Returns(true);
        //mockBse.Setup(s => s.startCell())
        //mockBse.Setup(s => s.configureCell())
        mockBse.Setup(s => s.ueIsConnected()).Returns(true);
        //mockBse.Setup(s => s.endCell())

        //var test = new TC_SensitivityTest(mockBse.Object)


    }
}