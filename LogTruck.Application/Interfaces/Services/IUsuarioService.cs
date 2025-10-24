using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
        Task UpdateAsync(Guid id, UpdateUsuarioDto dto);
        Task Desativar(Guid id);
        Task<UsuarioDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<UsuarioDto>> GetUsuariosMotoristas();
    }
}
