using System;

namespace LogTruck.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public Guid? UsuarioAlteracaoId  { get; set; }
        public DateTime CriadoEm { get; protected set; }
        public DateTime? AtualizadoEm { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = null;
            UsuarioAlteracaoId = null;
        }
    }
}