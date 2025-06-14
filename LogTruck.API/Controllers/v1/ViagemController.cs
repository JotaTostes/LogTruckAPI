using LogTruck.Application.Common.Notifications;
using LogTruck.Application.DTOs.Usuarios;
using LogTruck.Application.DTOs.Viagem;
using LogTruck.Application.Interfaces.Services;
using LogTruck.Application.Services;
using LogTruck.Domain.Entities;
using LogTruck.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogTruck.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Administrador,Operador")]
    public class ViagemController : ApiControllerBase
    {
        private readonly IViagemService _viagemService;

        public ViagemController(INotifier notifier, IViagemService viagemService) : base(notifier)
        {
            _viagemService = viagemService;
        }

        /// <summary>
        /// Retrieves all available trips.
        /// </summary>
        /// <remarks>This method fetches a collection of trips asynchronously and returns them in the
        /// response. The response will include all trips currently stored in the system.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing the collection of trips.  If no trips are available, the response
        /// will contain an empty collection.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var viagens = await _viagemService.ObterTodasAsync();
            return CustomResponse(viagens);
        }

        /// <summary>
        /// Retrieves a list of complete travel records.
        /// </summary>
        /// <remarks>This method returns a collection of travel records, each represented as a <see
        /// cref="ViagemCompletaDto"/> object.  The response will include all relevant details for each travel
        /// record.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a collection of <see cref="ViagemCompletaDto"/> objects with a
        /// status code of 200 (OK).</returns>
        [HttpGet("completa")]
        [ProducesResponseType(typeof(IEnumerable<ViagemCompletaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetViagensCompletas()
        {
            var viagens = await _viagemService.ObterViagensCompletas();
            return CustomResponse(viagens);
        }

        /// <summary>
        /// Retrieves a specific resource by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch the resource. Ensure the
        /// <paramref name="id"/> is a valid GUID.</remarks>
        /// <param name="id">The unique identifier of the resource to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the resource if found, or a <see cref="NotFoundResult"/> if the
        /// resource does not exist.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var viagem = await _viagemService.ObterPorIdAsync(id);
            if (viagem is null)
                return NotFound();

            return CustomResponse(viagem);
        }

        /// <summary>
        /// Creates a new "Viagem" (trip) resource based on the provided data.
        /// </summary>
        /// <param name="dto">The data transfer object containing the details of the "Viagem" to be created.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation. Typically, this will indicate
        /// success or provide error details if the creation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateViagemDto dto)
        {
            await _viagemService.CriarAsync(dto);
            return CustomResponse();
        }

        [HttpPut("{id:guid}/status/{statusViagem:int}")]
        public async Task<IActionResult> Update(Guid id, int statusViagem)
        {
            await _viagemService.AtualizarStatusViagem(id, statusViagem);
            return CustomResponse();
        }

        /// <summary>
        /// Updates an existing "Viagem" entity with the specified data.
        /// </summary>
        /// <param name="id">The unique identifier of the "Viagem" entity to update.</param>
        /// <param name="dto">The data transfer object containing the updated information for the "Viagem" entity.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.  Returns <see
        /// cref="BadRequestObjectResult"/> if the <paramref name="id"/> does not match the <c>Id</c> in <paramref
        /// name="dto"/>. Otherwise, returns a custom response indicating the outcome of the update operation.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateViagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id do caminho não confere com o corpo da requisição.");

            await _viagemService.AtualizarAsync(dto);
            return CustomResponse();
        }

        /// <summary>
        /// Cancels a trip with the specified identifier.
        /// </summary>
        /// <remarks>This method sends a cancellation request to the trip service and returns a custom
        /// response indicating the outcome. The trip must exist and be eligible for cancellation.</remarks>
        /// <param name="id">The unique identifier of the trip to cancel.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the cancellation operation.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _viagemService.CancelarAsync(id);
            return CustomResponse();
        }
        /// <summary>
        /// Approves a trip by updating its status to "In Progress".
        /// </summary>
        /// <remarks>This method updates the status of the specified trip to "In Progress" and returns a
        /// custom response.</remarks>
        /// <param name="id">The unique identifier of the trip to be approved.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{id:guid}/aprovar")]
        public async Task<IActionResult> Aprovar(Guid id)
        {
            await _viagemService.AtualizarStatusViagem(id, (int)StatusViagem.EmAndamento);
            return CustomResponse();
        }
    }
}
