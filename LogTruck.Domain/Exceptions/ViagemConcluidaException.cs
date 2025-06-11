using System;

namespace LogTruck.Domain.Exceptions
{
    public class ViagemConcluidaException : Exception
    {
        public ViagemConcluidaException()
            : base("A viagem já está concluída e não pode ser alterada.") { }

        public ViagemConcluidaException(string message)
            : base(message) { }

        public ViagemConcluidaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}