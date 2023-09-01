using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace LibreriaVaxi
{
    [TestFixture]
    public class OperacionNUnitTest
    {
        [Test]
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
            Assert.AreEqual(118, resultado); //evalua si ambos valores son iguales.
        }

        [Test]
        public void IsValorPar_InputNumeroImpar_ReturnFalse()
        {
            var op = new Operacion();
            var numeroImpar = 7;

            var isPar = op.IsValorPar(numeroImpar);

            Assert.IsFalse(isPar);
            Assert.That(isPar, Is.EqualTo(false));
        }

        [Test]
        public void IsValorPar_InputNumeroPar_ReturnTrue()
        {
            var op = new Operacion();
            var numeroPar = 6;

            var isPar = op.IsValorPar(numeroPar);

            Assert.IsTrue(isPar);
            Assert.That(isPar, Is.EqualTo(true));
        }

        [Test]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(20)]
        public void IsValorPar_InputNumeroPar_ReturnTrueParam(int numeroPar)
        {
            var op = new Operacion();

            var isPar = op.IsValorPar(numeroPar);

            Assert.IsTrue(isPar);
            Assert.That(isPar, Is.EqualTo(true));
        }

        [Test]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = false)]
        public bool IsValorPar_InputNumeroImpar_ReturnFalseExpectedResult(int numeroImpar)
        { 
            var op = new Operacion();
            
            return op.IsValorPar(numeroImpar);
        }

        [Test]
        [TestCase(2.2, 1.2)] // resultado debe ser 3.4
        [TestCase(2.23, 1.24)] // resultado debe ser 3.47
        public void SumarDecimal_InputDosNumeros_GetValorCorrecto(double decimal1Test, double decimal2Test)
        {
            //1.arrange 
            // inicializar las varaibles o componentes que ejecutaran el test

            var op = new Operacion();
            //2.Act
            var resultado = op.SumarDecimal(decimal1Test, decimal2Test);

            //3.Assert
            //Creamos un intervalo entre 3.3 y 3.5 para que los valores que esten ese rango sean validos
            Assert.AreEqual(3.4, resultado, 0.1); 
        }

        [Test]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            //arrange
            var op = new Operacion();
            var numerosImparesEsperados = new List<int> { 5, 7, 9 };

            //Act
            var resultados = op.GetListaNumerosImpares(5, 10);

            //Assert
            Assert.That(resultados, Is.EquivalentTo(numerosImparesEsperados));
            Assert.AreEqual(numerosImparesEsperados, resultados);
            Assert.That(resultados, Does.Contain(5)); //para validar si el 5 esta en los resultados
            Assert.Contains(5, resultados); //para validar si el 5 esta en los resultados
            Assert.That(resultados, Is.Not.Empty); //comprara que no es una lista vacia.
            Assert.That(resultados.Count, Is.EqualTo(3)); //Contar la cantidad de elementos
            Assert.That(resultados, Has.No.Member(100)); //validar que un valor no este en la lista
            Assert.That(resultados, Is.Ordered.Ascending); //validar que este ordenado Ascendente
            Assert.That(resultados, Is.Unique); //buscar valores duplicados. Pasa el test si los valores son unicos.
        }
    }
}
