using FluentValidation;
using LogTruck.Application.DTOs.Comissao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Validators.Comissao
{
    public class CreateComissaoDtoValidator : AbstractValidator<CreateComissaoDto>
    {
        public CreateComissaoDtoValidator()
        {
            RuleFor(x => x.ViagemId)
                .NotEmpty().WithMessage("O ID da viagem é obrigatório.");

            RuleFor(x => x.Percentual)
                .GreaterThan(0).WithMessage("O percentual deve ser maior que zero.")
                .LessThanOrEqualTo(100).WithMessage("O percentual não pode ser maior que 100.");
        }
    }
}
