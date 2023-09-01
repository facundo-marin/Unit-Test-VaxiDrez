using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaVaxi;

namespace LibreriaVaxiMSTest
{
    [TestClass]
    public class OperacionMSTest
    {
        [TestMethod]
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
            Assert.AreEqual(119, resultado); //evalua si ambos valores son iguales.
        }
    }
}
