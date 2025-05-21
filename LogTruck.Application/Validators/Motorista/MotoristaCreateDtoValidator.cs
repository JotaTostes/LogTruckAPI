using FluentValidation;
using LogTruck.Application.DTOs.Motorista;

namespace LogTruck.Application.Validators.Motorista
{
    public class CreateMotoristaDtoValidator : AbstractValidator<CreateMotoristaDto>
    {
        public CreateMotoristaDtoValidator()
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("Usuário é obrigatório.");

            //RuleFor(x => x.Nome)
            //    .NotEmpty().WithMessage("Nome é obrigatório.")
            //    .MaximumLength(150).WithMessage("Nome pode ter no máximo 150 caracteres.");

            //RuleFor(x => x.CPF)
            //    .NotEmpty().WithMessage("CPF é obrigatório.")
            //    .Length(11).WithMessage("CPF deve conter 11 dígitos.")
            //    .Matches(@"^\d{11}$").WithMessage("CPF deve conter apenas números.");

            RuleFor(x => x.CNH)
                .NotEmpty().WithMessage("CNH é obrigatória.")
                .MaximumLength(20).WithMessage("CNH pode ter no máximo 20 caracteres.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser no passado.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.")
                .MaximumLength(15).WithMessage("Telefone pode ter no máximo 15 caracteres.");
            // Pode ajustar o formato conforme sua regra local de telefone
        }
    }
}
