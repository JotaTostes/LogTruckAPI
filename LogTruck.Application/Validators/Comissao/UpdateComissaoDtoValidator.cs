using FluentValidation;
using LogTruck.Application.DTOs.Comissao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Validators.Comissao
{
    public class UpdateComissaoDtoValidator : AbstractValidator<UpdateComissaoDto>
    {
        public UpdateComissaoDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O campo Id é obrigatório.");

            RuleFor(x => x.Percentual)
                .GreaterThan(0).WithMessage("O percentual deve ser maior que zero.")
                .LessThanOrEqualTo(100).WithMessage("O percentual não pode ser maior que 100.");
        }
    }
}
