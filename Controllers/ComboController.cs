using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComboController : ControllerBase
    {
        private readonly IComboRepository _comboRepository;

        public ComboController(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var combos = await _comboRepository.GetAllAsync();
            return Ok(combos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var combo = await _comboRepository.GetByIdAsync(id);
            if (combo == null)
                return NotFound();

            return Ok(combo);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ComboCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            await _comboRepository.AddAsync(dto);
            return Ok("Combo agregado correctamente.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComboCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            await _comboRepository.UpdateAsync(id, dto);
            return Ok("Combo actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _comboRepository.DeleteAsync(id);
            return Ok("Combo eliminado correctamente.");
        }
    }
}
