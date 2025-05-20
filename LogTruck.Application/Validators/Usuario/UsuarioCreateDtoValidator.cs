using FluentValidation;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Validators.Usuario
{
    public class CreateUsuarioDtoValidator : AbstractValidator<CreateUsuarioDto>
    {
        public CreateUsuarioDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.");
            //.EmailAddress().WithMessage("O e-mail informado não é válido.")
            //.MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Role)
            .Must(role => Enum.IsDefined(typeof(RoleUsuario), role));
            RuleFor(x => x.Role)
            .Must(BeAValidRole)
            .WithMessage(_ => $"O perfil informado é inválido. Valores válidos: {GetEnumValues<RoleUsuario>()}");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }

        private bool BeAValidRole(int role)
        {
            return Enum.IsDefined(typeof(RoleUsuario), role);
        }

        private string GetEnumValues<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<int>();
            var names = Enum.GetNames(typeof(TEnum));
            return string.Join(", ", values.Select((v, i) => $"{v} ({names[i]})"));
        }
    }
}
