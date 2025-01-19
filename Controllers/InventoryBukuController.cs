using library_be.Data;
using library_be.Dtos.InventoryBukuDto;
using library_be.Dtos.MasterBukuDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("C. Inventory Buku")]
    public class InventoryBukuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IInventoryBukuRepository _inventoryBukuRepo;

        public InventoryBukuController(ApplicationDbContext context, IInventoryBukuRepository inventoryBukuRepo)
        {
            _context = context;
            _inventoryBukuRepo = inventoryBukuRepo;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryRequestDto inventoryRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventoryModel = InventoryBukuMappers.ToInventoryFromCreateDTO(inventoryRequestDto);
            await _inventoryBukuRepo.CreateAsync(inventoryModel);
            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateInventoryRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryModel = await _inventoryBukuRepo.UpdateAsync(id, updateDto);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryModel = await _inventoryBukuRepo.DeleteAsync(id);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryBuku = await _inventoryBukuRepo.GetByIdAsync(id);

            if (inventoryBuku == null)
            {
                return NotFound();
            }

            return Ok(inventoryBuku.ToInventoryDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryBuku([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (inventory, totalCount) = await _inventoryBukuRepo.GetAllAsync(query);
            var inventoryDtos = inventory.Select(s => s.ToInventoryDto()).ToList();

            var paginatedDto = new Paginated<InventoryDto>
            {
                TotalCount = totalCount,
                Data = inventoryDtos
            };

            return Ok(paginatedDto);
        }
    }
}
