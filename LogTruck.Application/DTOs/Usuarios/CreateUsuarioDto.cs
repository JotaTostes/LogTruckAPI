namespace LogTruck.Application.DTOs.Usuarios
{
    public class CreateUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string? Senha { get; set; }
    }
}
