using ECommerce_Project.Repository;
using ECommerceAPP.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {

        private readonly ITerritoryRepository _territoryRepository;

        public TerritoryController(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerritoryDto>>> GetTerritories()
        {
            var territories = await _territoryRepository.GetAllTerritory();
            return Ok(territories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TerritoryDto>> GetTerritory(string id)
        {
            var territory = await _territoryRepository.GetTerritoryById(id);
            if (territory == null)
            {
                return NotFound();
            }

            return Ok(territory);
        }

        [HttpPost]
        public async Task<ActionResult<TerritoryDto>> CreateTerritory(TerritoryDto territoryDto)
        {
            var createdTerritory = await _territoryRepository.CreateTerritory(territoryDto);
            return CreatedAtAction(nameof(GetTerritory), new { id = createdTerritory.TerritoryID }, createdTerritory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTerritory(string id, TerritoryDto territoryDto)
        {
            if (id != territoryDto.TerritoryID)
            {
                return BadRequest("ID in the URL does not match ID in the body.");
            }

            var result = await _territoryRepository.UpdateTerritory(id, territoryDto);
            if (result == null)
            {
                return NotFound("Territory not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerritory(string id)
        {
            var result = await _territoryRepository.DeleteTerritoryById(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
    

