using FluentValidation;
using LogTruck.Application.DTOs.CustoViagem;
using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Validators.CustoViagem
{
    public class CreateCustoViagemDtoValidator : AbstractValidator<CreateCustoViagemDto>
    {
        public CreateCustoViagemDtoValidator()
        {
            RuleFor(x => x.ViagemId)
                .NotEmpty().WithMessage("Viagem é obrigatória");
            RuleFor(x => x.Tipo)
                .Must(tipo => Enum.IsDefined(typeof(TipoCusto), tipo));
            RuleFor(x => x.Tipo)
            .Must(BeAValidType)
            .WithMessage(_ => $"O tipo informado é inválido. Valores válidos: {GetEnumValues<TipoCusto>()}");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("O valor do custo deve ser maior que 0."); ;
            RuleFor(x => x.Descricao)
                .MaximumLength(200).WithMessage("Descrição pode ter no máximo 200 caracteres.");
        }

        private bool BeAValidType(int tipo)
        {
            return Enum.IsDefined(typeof(TipoCusto), tipo);
        }

        private string GetEnumValues<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<int>();
            var names = Enum.GetNames(typeof(TEnum));
            return string.Join(", ", values.Select((v, i) => $"{v} ({names[i]})"));
        }
    }
}
