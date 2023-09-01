using Moq;
using NUnit.Framework;

namespace LibreriaVaxi
{
    [TestFixture]
    public class CuentaBancariaNUnitTest
    {
        private CuentaBancaria cuenta;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Deposito_InputMonto100LoggerFake_ReturnTrue()
        {
            var cuentaBancaria = new CuentaBancaria(new LoggerFake());

            var resultado = cuentaBancaria.Deposito(100);
            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));
        }

        [Test]
        public void Deposito_InputMonto100Mocking_ReturnTrue()
        {
            var mocking = new Mock<ILoggerGeneral>();
            var cuentaBancaria = new CuentaBancaria(mocking.Object);

            var resultado = cuentaBancaria.Deposito(100);
            Assert.IsTrue(resultado);
            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));
        }

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void Retiro_Retiro100ConBalance200_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsAny<int>())).Returns(true);

            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.IsTrue(resultado); //si el retiro es exitoso es true.
        }

        [Test]
        [TestCase(200, 300)]
        public void Retiro_Retiro300ConBalance200_ReturnsFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            // loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x < 0))).Returns(false);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.IsFalse(resultado); //si el retiro es exitoso es true.
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            string textoPrueba = "hola mundo";

            loggerGeneralMock.Setup(x => x.MessageConReturnStr(It.IsAny<string>()))
                .Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnStr("hoLa mundO");

            Assert.That(resultado, Is.EqualTo(textoPrueba));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_Return()
        {
            var loggerGeneral = new Mock<ILoggerGeneral>();
            var textoPrueba = "Hola";

            loggerGeneral.Setup(x => x.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba))
                .Returns(true);

            var parametroOut = "";
            var resultado = loggerGeneral.Object.MessageConOutParametroReturnBoolean("Marin", out parametroOut);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingObjectRef_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            var cliente = new Cliente();
            var clienteNoUsado = new Cliente();

            loggerGeneralMock.Setup(x => x.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

            Assert.IsTrue(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));

            Assert.IsFalse(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            loggerGeneralMock.SetupAllProperties();
            loggerGeneralMock.Setup(x => x.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(x => x.PrioridadLogger).Returns(10);

            loggerGeneralMock.Object.PrioridadLogger = 100;

            Assert.That(loggerGeneralMock.Object.TipoLogger, Is.EqualTo("warning"));
            Assert.That(loggerGeneralMock.Object.PrioridadLogger, Is.EqualTo(10));


            //CALLBACKS
            var textoTemporal = "Facundo";
            loggerGeneralMock.Setup(x => x.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback((string parametro) => textoTemporal += parametro);

            loggerGeneralMock.Object.LogDatabase("Marin");

            Assert.That(textoTemporal, Is.EqualTo("FacundoMarin"));
        }

        [Test]
        public void CuentaBancariaLogger_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            var cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);

            Assert.That(cuentaBancaria.GetBalance(), Is.EqualTo(100));

            //Verifica cuantas veces el mock esta llamando al metodo .message

            loggerGeneralMock.Verify(x => x.Message(It.IsAny<string>()), Times.Exactly(3));
            loggerGeneralMock.Verify(x => x.Message("tercera vez"), Times.AtLeastOnce);

            //Para verificar propiedades se usa VerifySet
            loggerGeneralMock.VerifySet(x => x.PrioridadLogger = 100, Times.Once);
            //Para verificar cuando se obtiene el valor de una propiedad.
            loggerGeneralMock.VerifyGet(x => x.PrioridadLogger, Times.Once);

        }
    }
}
