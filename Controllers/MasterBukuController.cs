using library_be.Data;
using library_be.Dtos.MasterBukuDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("A. Master Buku")]
    public class MasterBukuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMasterBukuRepo _masterBukuRepo;

        public MasterBukuController(ApplicationDbContext context, IMasterBukuRepo masterBukuRepo)
        {
            _context = context;
            _masterBukuRepo = masterBukuRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBukuRequestDto bukuRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bukuModel = MasterBukuMappers.ToBukuFromCreateDTO(bukuRequestDto);
            await _masterBukuRepo.CreateAsync(bukuModel);
            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBukuRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bukuModel = await _masterBukuRepo.UpdateAsync(id, updateDto);

            if (bukuModel == null)
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

            var bukuModel = await _masterBukuRepo.DeleteAsync(id);

            if (bukuModel == null)
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

            var masterBuku = await _masterBukuRepo.GetByIdAsync(id);

            if (masterBuku == null)
            {
                return NotFound();
            }

            return Ok(masterBuku.ToBukuDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetMasterBuku([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (buku, totalCount) = await _masterBukuRepo.GetAllAsync(query);
            var bukuDtos = buku.Select(s => s.ToBukuDto()).ToList();

            var paginatedDto = new Paginated<BukuDto>
            {
                TotalCount = totalCount,
                Data = bukuDtos
            };

            return Ok(paginatedDto);
        }
    }
}
