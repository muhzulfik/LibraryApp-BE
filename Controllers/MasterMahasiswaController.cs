using library_be.Data;
using library_be.Dtos.MasterBukuDto;
using library_be.Dtos.MasterMahasiswaDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("B. Master Mahasiswa")]
    public class MasterMahasiswaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMasterMahasiswaRepo _masterMahasiswaRepo;

        public MasterMahasiswaController(ApplicationDbContext context, IMasterMahasiswaRepo masterMahasiswaRepo)
        {
            _context = context;
            _masterMahasiswaRepo = masterMahasiswaRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMahasiswaRequestDto mahasiswaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mahasiswaModel = MasterMahasiswaMappers.ToMahasiswaFromCreateDTO(mahasiswaRequestDto);
            await _masterMahasiswaRepo.CreateAsync(mahasiswaModel);
            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateMahasiswaRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mahasiswaModel = await _masterMahasiswaRepo.UpdateAsync(id, updateDto);

            if (mahasiswaModel == null)
            {
                return NotFound();
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mahasiswaModel = await _masterMahasiswaRepo.DeleteAsync(id);

            if (mahasiswaModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mahasiswa = await _masterMahasiswaRepo.GetByIdAsync(id);

            if (mahasiswa == null)
            {
                return NotFound();
            }

            return Ok(mahasiswa.ToMahasiswaDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetMasterMahasiswa([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (mahasiswa, totalCount) = await _masterMahasiswaRepo.GetAllAsync(query);
            var mahasiswaDtos = mahasiswa.Select(s => s.ToMahasiswaDto()).ToList();

            var paginatedDto = new Paginated<MahasiswaDto>
            {
                TotalCount = totalCount,
                Data = mahasiswaDtos
            };

            return Ok(paginatedDto);
        }
    }
}
