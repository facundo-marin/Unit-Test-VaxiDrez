using Xunit;

namespace LibreriaVaxi
{
    public class ClienteXUnitTest
    {
        private Cliente cliente;

        public ClienteXUnitTest()
        {
            cliente = new Cliente();
        }

        [Fact]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            //act
            cliente.CrearNombreCompleto("Facundo", "Marin");

            //assert
            Assert.Multiple(() => Assert.Equal("Facundo Marin", cliente.ClienteNombre),
                () => Assert.Contains("Facu", cliente.ClienteNombre), // esto es para ver si contiene cierto texto
                () => Assert.StartsWith("Facundo", cliente.ClienteNombre), //Evalua si el string comienza con facundo.
                () => Assert.EndsWith("Marin", cliente.ClienteNombre) //Evalua que el string termine con Marin
            );
        }

        [Fact]
        public void ClienteNombre_NoValues_ReturnNull()
        {
            Assert.Null(cliente.ClienteNombre);
            Assert.Equal(cliente.ClienteNombre, null);
        }
        
        [Fact]
        public void DescuentoEvaluacion_DefaultCliente_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
        
            Assert.InRange(descuento, 5, 24);
        }
        
        [Fact]
        public void CrearNombreCompleto_InputNombre_ReturnNotNull()
        {
            cliente.CrearNombreCompleto("Facu", "");
            Assert.NotNull(cliente.ClienteNombre);
            Assert.False(string.IsNullOrWhiteSpace(cliente.ClienteNombre));
        }
        
        [Fact]
        public void ClienteNombre_InputNombreEnBlanco_ThrowException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "marin"));
            Assert.Equal("El nombre esta en blanco", exceptionDetalle.Message);

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "marin"));
        }
        
        [Fact]
        public void GetClienteDetalle_CrearClienteConMenos500OrderTotal_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 300;
            var resultado = cliente.GetClienteDetalle();
            // Assert.That(resultado,Is.TypeOf<ClienteBasico>());
            Assert.IsType<ClienteBasico>(resultado);
        }
        
        [Fact]
        public void GetClienteDetalle_CrearClienteConMas500OrderTotal_ReturnsClientePremium()
        {
            cliente.OrderTotal = 600;
            var resultado = cliente.GetClienteDetalle();
            // Assert.That(resultado, Is.TypeOf<ClientePremium>());
            Assert.IsType<ClientePremium>(resultado);

        }
    }
}
