using NUnit.Framework;

namespace LibreriaVaxi
{
    [TestFixture]
    public class ClienteNUnitTest
    {
        private Cliente cliente;

        [SetUp]
        public void Setup()
        {
            cliente = new Cliente();
        }

        [Test]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            //act
            cliente.CrearNombreCompleto("Facundo", "Marin");

            //assert

            Assert.Multiple(() =>
            {
                Assert.That(cliente.ClienteNombre, Is.EqualTo("XFacundo Marin"));
                Assert.AreEqual(cliente.ClienteNombre, "Facundo Marin");

                Assert.That(cliente.ClienteNombre, Does.Contain("Facu")); // esto es para ver si contiene cierto texto
                Assert.That(cliente.ClienteNombre, Does.Contain("Facu")); // no ignora mayusculas y minisculas.
                Assert.That(cliente.ClienteNombre, Does.Contain("facu").IgnoreCase); //ignora mayusculas y minusculas.
                Assert.That(cliente.ClienteNombre, Does.StartWith("Facundo"));//Evalua si el string comienza con facundo.
                Assert.That(cliente.ClienteNombre, Does.EndWith("Marin")); //Evalua que el string termine con Marin
            });
        }

        [Test]
        public void ClienteNombre_NoValues_ReturnNull()
        {
            Assert.IsNull(cliente.ClienteNombre);
            Assert.That(cliente.ClienteNombre, Is.Null);
        }

        [Test]
        public void DescuentoEvaluacion_DefaultCliente_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;

            Assert.That(descuento, Is.InRange(5,24));
        }

        [Test]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Facu", "");
            Assert.IsNotNull(cliente.ClienteNombre);
            Assert.IsFalse(string.IsNullOrWhiteSpace(cliente.ClienteNombre));
        }

        [Test]
        public void ClienteNombre_InputNombreEnBlanco_ThrowException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "marin"));
            Assert.AreEqual("El nombre esta en blanco", exceptionDetalle.Message);
            Assert.That(() => 
                cliente.CrearNombreCompleto("", "Marin"), Throws.ArgumentException.With.Message.EqualTo("El nombre esta en blanco"));

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "marin"));
            Assert.That(() =>
                cliente.CrearNombreCompleto("", "Marin"), Throws.ArgumentException);
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMenos500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 300;
            var resultado = cliente.GetClienteDetalle();
            Assert.That(resultado,Is.TypeOf<ClienteBasico>());
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMas500OrderTotal_ReturnsClientePremium()
        {
            cliente.OrderTotal = 600;
            var resultado = cliente.GetClienteDetalle();
            Assert.That(resultado, Is.TypeOf<ClientePremium>());
        }
    }
}
