using Moq;
using NUnit.Framework;

using RadioConformanceTests.TC;
using RadioConformanceTests.Instruments;
using RadioConformanceTests.Drivers;
using System;

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

    [Test]
    public void failVerdictUELoseConnectionTest(){
        // Verificar que da fail cuando el UE pierde la conexion
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(s => s.ueIsConnected()).Returns(() => 
        {    
            Random rand = new Random();
            return rand.NextDouble() >= 0.3;
        });

        mockBse.Setup(s => s.verifyBSEInstrument()).Returns(true);
        
        ILogger logger = new Logger();
        var test = new TC_Sensitivity(mockBse.Object, logger);

        Assert.That(test.Execute(), Is.EqualTo(TestVerdict.Fail));

        

    }

}