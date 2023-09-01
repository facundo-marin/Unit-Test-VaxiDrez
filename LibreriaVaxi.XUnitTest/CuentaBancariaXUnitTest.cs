using Moq;
using Xunit;

namespace LibreriaVaxi
{
    public class CuentaBancariaXUnitTest
    {
        private CuentaBancaria cuenta;


        [Fact]
        public void Deposito_InputMonto100LoggerFake_ReturnTrue()
        {
            var cuentaBancaria = new CuentaBancaria(new LoggerFake());

            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Fact]
        public void Deposito_InputMonto100Mocking_ReturnTrue()
        {
            var mocking = new Mock<ILoggerGeneral>();
            var cuentaBancaria = new CuentaBancaria(mocking.Object);

            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.Equal(100, cuentaBancaria.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void Retiro_Retiro100ConBalance200_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsAny<int>())).Returns(true);

            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.True(resultado); //si el retiro es exitoso es true.
        }

        [Theory]
        [InlineData(200, 300)]
        public void Retiro_Retiro300ConBalance200_ReturnsFalse(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            // loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x < 0))).Returns(false);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            var cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);

            var resultado = cuentaBancaria.Retiro(retiro);

            Assert.False(resultado); //si el retiro es exitoso es true.
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            string textoPrueba = "hola mundo";

            loggerGeneralMock.Setup(x => x.MessageConReturnStr(It.IsAny<string>()))
                .Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnStr("hoLa mundO");

            Assert.Equal(textoPrueba, resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_Return()
        {
            var loggerGeneral = new Mock<ILoggerGeneral>();
            var textoPrueba = "Hola";

            loggerGeneral.Setup(x => x.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba))
                .Returns(true);

            var parametroOut = "";
            var resultado = loggerGeneral.Object.MessageConOutParametroReturnBoolean("Marin", out parametroOut);

            Assert.True(resultado);
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingObjectRef_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            var cliente = new Cliente();
            var clienteNoUsado = new Cliente();

            loggerGeneralMock.Setup(x => x.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

            Assert.True(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));

            Assert.False(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }

        [Fact]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            loggerGeneralMock.SetupAllProperties();
            loggerGeneralMock.Setup(x => x.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(x => x.PrioridadLogger).Returns(10);

            loggerGeneralMock.Object.PrioridadLogger = 100;

            Assert.Equal("warning", loggerGeneralMock.Object.TipoLogger);
            Assert.Equal(10, loggerGeneralMock.Object.PrioridadLogger);


            //CALLBACKS
            var textoTemporal = "Facundo";
            loggerGeneralMock.Setup(x => x.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback((string parametro) => textoTemporal += parametro);

            loggerGeneralMock.Object.LogDatabase("Marin");

            Assert.Equal("FacundoMarin", textoTemporal);
        }

        [Fact]
        public void CuentaBancariaLogger_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            var cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);

            Assert.Equal(100, cuentaBancaria.GetBalance());

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
