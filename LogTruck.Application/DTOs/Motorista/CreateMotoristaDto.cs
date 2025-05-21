namespace LogTruck.Application.DTOs.Motorista
{
    public class CreateMotoristaDto
    {
        public Guid UsuarioId { get; set; }
        public string CNH { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; } = null!;
    }
}
