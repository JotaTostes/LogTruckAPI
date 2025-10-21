using FluentValidation;
using LogTruck.Application.DTOs.Viagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Validators.Viagem
{
    public class UpdateViagemDtoValidator : AbstractValidator<UpdateViagemDto>
    {
        public UpdateViagemDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id da viagem é obrigatório.");

            RuleFor(x => x.Quilometragem)
                .GreaterThan(0).WithMessage("A quilometragem deve ser maior que zero.");

            RuleFor(x => x.ValorFrete)
                .GreaterThan(0).WithMessage("O valor do frete deve ser maior que zero.");

            RuleFor(x => x.DataSaida)
                .NotEmpty().WithMessage("Data de saída é obrigatória.");

            RuleFor(x => x.Origem)
                .NotEmpty().WithMessage("Origem é obrigatória.")
                .MaximumLength(100);

            RuleFor(x => x.Destino)
                .NotEmpty().WithMessage("Destino é obrigatória.")
                .MaximumLength(100);
        }
    }
}
