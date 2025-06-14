using LogTruck.Domain.Enums;

namespace LogTruck.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Cpf { get; set; }
        public RoleUsuario Role { get; set; }
        public bool Ativo { get; set; }
        public Motorista? Motorista { get; set; }

        public Usuario() { }

        public Usuario(string nome, string email, string senhaHash, RoleUsuario role)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
            Ativo = true;
        }

        public void Atualizar(string nome, string email, RoleUsuario role, string CPF, string Senha, Guid? usuarioAlteracao)
        {
            Nome = nome;
            Email = email;
            Role = role;
            Cpf = CPF;
            SenhaHash = Senha;
            AtualizadoEm = DateTime.UtcNow;
            UsuarioAlteracaoId = usuarioAlteracao;
        }

        public void Desativar(Guid? usuarioAlteracao)
        {
            Ativo = false;
            AtualizadoEm = DateTime.UtcNow;
            UsuarioAlteracaoId = usuarioAlteracao;
        }

        public void Reativar(Guid? usuarioAlteracao)
        {
            Ativo = true;
            AtualizadoEm = DateTime.UtcNow;
            UsuarioAlteracaoId = usuarioAlteracao;
        }
    }
}
