using FluentValidation;
using LogTruck.Application.DTOs.Viagem;

namespace LogTruck.Application.Validators.Viagem
{
    public class CreateViagemDtoValidator : AbstractValidator<CreateViagemDto>
    {
        public CreateViagemDtoValidator()
        {
            RuleFor(x => x.MotoristaId)
                .NotEmpty().WithMessage("Motorista é obrigatório.");

            RuleFor(x => x.CaminhaoId)
                .NotEmpty().WithMessage("Caminhão é obrigatório.");

            RuleFor(x => x.Origem)
                .NotEmpty().WithMessage("Origem é obrigatória.")
                .MaximumLength(100);

            RuleFor(x => x.Destino)
                .NotEmpty().WithMessage("Destino é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Quilometragem)
                .GreaterThan(0).WithMessage("A quilometragem deve ser maior que zero.");

            RuleFor(x => x.ValorFrete)
                .GreaterThan(0).WithMessage("O valor do frete deve ser maior que zero.");

            RuleFor(x => x.DataSaida)
                .NotEmpty().WithMessage("Data de saída é obrigatória.")
                .Must(data => data > DateTime.MinValue).WithMessage("Data de saída inválida.");
        }
    }
}
