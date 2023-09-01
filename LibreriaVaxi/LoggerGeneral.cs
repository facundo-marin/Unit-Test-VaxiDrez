using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVaxi
{
    public interface ILoggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }
        void Message(string message);
        bool LogDatabase(string message);
        bool LogBalanceDespuesRetiro(int balanceDespuesRetiro);
        string MessageConReturnStr(string message);
        bool MessageConOutParametroReturnBoolean(string message, out string outputStr);
        bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente);
    }
    public class LoggerGeneral : ILoggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public bool LogDatabase(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogBalanceDespuesRetiro(int balanceDespuesRetiro)
        {
            if (balanceDespuesRetiro >= 0)
            {
                Console.WriteLine("exito");
                return true;
            }

            Console.WriteLine("error");
            return false;
        }

        public string MessageConReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }

        public bool MessageConOutParametroReturnBoolean(string message, out string outputStr)
        {
            outputStr = "Hola" + message;
            return true;
        }

        public bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente)
        {
            return true;
        }
    }

    public class LoggerFake : ILoggerGeneral
    {
        public int PrioridadLogger { get; set; }
        public string TipoLogger { get; set; }

        public void Message(string message)
        {
            
        }

        public bool LogDatabase(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogBalanceDespuesRetiro(int balanceDespuesRetiro)
        {
            if (balanceDespuesRetiro >= 0)
            {
                Console.WriteLine("exito");
                return true;
            }

            Console.WriteLine("error");
            return false;
        }

        public string MessageConReturnStr(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }

        public bool MessageConOutParametroReturnBoolean(string message, out string outputStr)
        {
            outputStr = "Hola" + message;
            return true;
        }

        public bool MessageConObjetoReferenciaReturnBoolean(ref Cliente cliente)
        {
            return true;
        }
    }
}
