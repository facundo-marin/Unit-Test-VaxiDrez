using Xunit;

namespace LibreriaVaxi
{
    public class OperacionXUnitTest
    {
        [Fact]
        public void SumarNumeros_InputDosNumeros_GetValorCorrecto()
        {
            //1.arrange 
            // inicializar las varaibles o componentes que ejecutaran el test

            var op = new Operacion();
            var numero1Test = 50;
            var numero2Test = 69;

            //2.Act
            var resultado = op.SumarNumero(numero1Test, numero2Test);

            //3.Assert
            Assert.Equal(119, resultado); //evalua si ambos valores son iguales.
        }

        [Fact]
        public void IsValorPar_InputNumeroImpar_ReturnFalse()
        {
            var op = new Operacion();
            var numeroImpar = 7;

            var isPar = op.IsValorPar(numeroImpar);

            Assert.False(isPar);
        }

        [Fact]
        public void IsValorPar_InputNumeroPar_ReturnTrue()
        {
            var op = new Operacion();
            var numeroPar = 6;

            var isPar = op.IsValorPar(numeroPar);

            Assert.True(isPar);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrueParam(int numeroPar)
        {
            var op = new Operacion();

            var isPar = op.IsValorPar(numeroPar);

            Assert.True(isPar);
        }

        [Theory]
        [InlineData(3, false)]
        [InlineData(5, false)]
        [InlineData(11, false)]
        public void IsValorPar_InputNumeroImpar_ReturnFalseExpectedResult(int numeroImpar, bool expectedResult)
        { 
            var op = new Operacion();
            var resultado = op.IsValorPar(numeroImpar);
            Assert.Equal(expectedResult, resultado);
        }

        [Theory]
        [InlineData(2.2, 1.2)] // resultado debe ser 3.4
        [InlineData(2.23, 1.24)] // resultado debe ser 3.47
        public void SumarDecimal_InputDosNumeros_GetValorCorrecto(double decimal1Test, double decimal2Test)
        {
            //1.arrange 
            // inicializar las varaibles o componentes que ejecutaran el test

            var op = new Operacion();
            //2.Act
            var resultado = op.SumarDecimal(decimal1Test, decimal2Test);

            //3.Assert
            //Creamos un intervalo entre 3.3 y 3.5 para que los valores que esten ese rango sean validos
            Assert.Equal(3.4, resultado, 0.1); 
        }

        [Fact]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            //arrange
            var op = new Operacion();
            var numerosImparesEsperados = new List<int> { 5, 7, 9 };

            //Act
            var resultados = op.GetListaNumerosImpares(5, 10);

            //Assert
            Assert.Equal(numerosImparesEsperados, resultados);
            Assert.Contains(5, resultados); //para validar si el 5 esta en los resultados
            Assert.NotEmpty(resultados); //comprara que no es una lista vacia.
            Assert.Equal(3, resultados.Count); //Contar la cantidad de elementos
            Assert.DoesNotContain(100, resultados); //validar que un valor no este en la lista
            Assert.Equal(resultados.OrderBy(x => x), resultados); //validar que este ordenado Ascendente
        }
    }
}
