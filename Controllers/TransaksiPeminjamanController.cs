using library_be.Data;
using library_be.Dtos.HistoryPeminjamanDto;
using library_be.Dtos.TransaksiPeminjamanDto;
using library_be.Helper;
using library_be.Interfaces;
using library_be.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("D. Transaksi Peminjaman")]
    public class TransaksiPeminjamanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITransaksiPeminjamanRepo _transaksiPeminjamanRepo;

        public TransaksiPeminjamanController(ApplicationDbContext context, ITransaksiPeminjamanRepo transaksiPeminjamanRepo)
        {
            _context = context;
            _transaksiPeminjamanRepo = transaksiPeminjamanRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransaksiRequestDto transaksiRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaksiModel = TransaksiPeminjamanMappers.ToTransaksiFromCreateDTO(transaksiRequestDto);
            await _transaksiPeminjamanRepo.CreateAsync(transaksiModel);
            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTransaksiRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaksiModel = await _transaksiPeminjamanRepo.UpdateAsync(id, updateDto);

            if (transaksiModel == null)
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

            var transaksiModel = await _transaksiPeminjamanRepo.DeleteAsync(id);

            if (transaksiModel == null)
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

            var transaksiPeminjaman = await _transaksiPeminjamanRepo.GetByIdAsync(id);

            if (transaksiPeminjaman == null)
            {
                return NotFound();
            }

            return Ok(transaksiPeminjaman.ToTransaksiDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaksiPeminjaman([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (transaksi, totalCount) = await _transaksiPeminjamanRepo.GetAllAsync(query);
            var transaksiDtos = transaksi.Select(s => s.ToTransaksiDto()).ToList();

            var paginatedDto = new Paginated<TransaksiDto>
            {
                TotalCount = totalCount,
                Data = transaksiDtos
            };

            return Ok(paginatedDto);
        }
    }
}
