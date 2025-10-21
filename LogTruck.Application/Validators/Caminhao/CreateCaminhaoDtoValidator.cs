using FluentValidation;
using LogTruck.Application.DTOs.Caminhao;

namespace LogTruck.Application.Validators.Caminhao
{
    public class CreateCaminhaoDtoValidator : AbstractValidator<CreateCaminhaoDto>
    {
        public CreateCaminhaoDtoValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("A placa é obrigatória.")
                .Length(7).WithMessage("A placa deve conter exatamente 7 caracteres.")
                .Matches("^[A-Z0-9]+$").WithMessage("A placa deve conter apenas letras maiúsculas e números.");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("A marca é obrigatória.")
                .MaximumLength(30).WithMessage("A marca deve ter no máximo 30 caracteres.");

            RuleFor(x => x.Ano)
                .InclusiveBetween(1980, DateTime.UtcNow.Year + 1)
                .WithMessage($"O ano deve estar entre 1980 e {DateTime.UtcNow.Year + 1}.");

            RuleFor(x => x.CapacidadeToneladas)
                .GreaterThan(0).WithMessage("A capacidade em toneladas deve ser maior que 0.");
        }
    }
}
