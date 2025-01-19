using library_be.Data;
using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.InventoryBukuDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("E. History Peminjaman")]
    public class HistoryPeminjamanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHistoryPeminjamanRepo _historyPeminjamanRepo;

        public HistoryPeminjamanController(ApplicationDbContext context, IHistoryPeminjamanRepo historyPeminjamanRepo)
        {
            _context = context;
            _historyPeminjamanRepo = historyPeminjamanRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHistoryRequestDto historyRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var historyModel = HistoryPeminjamanMappers.ToHistoryFromCreateDTO(historyRequestDto);
            await _historyPeminjamanRepo.CreateAsync(historyModel);
            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateHistoryRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var historyModel = await _historyPeminjamanRepo.UpdateAsync(id, updateDto);

            if (historyModel == null)
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

            var historyModel = await _historyPeminjamanRepo.DeleteAsync(id);

            if (historyModel == null)
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

            var historyPeminjaman = await _historyPeminjamanRepo.GetByIdAsync(id);

            if (historyPeminjaman == null)
            {
                return NotFound();
            }

            return Ok(historyPeminjaman.ToHistoryDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetHistoryPeminjaman([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (history, totalCount) = await _historyPeminjamanRepo.GetAllAsync(query);
            var historyDtos = history.Select(s => s.ToHistoryDto()).ToList();

            var paginatedDto = new Paginated<HistoryDto>
            {
                TotalCount = totalCount,
                Data = historyDtos
            };

            return Ok(paginatedDto);
        }
    }
}
