using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoletosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BoletosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoletoCreateDto dto)
        {
            var bancoExiste = await _context.Bancos.AnyAsync(b => b.Id == dto.BancoId);
            if (!bancoExiste) return BadRequest("O Banco informado não existe.");

            var boleto = _mapper.Map<Boleto>(dto);
            _context.Boletos.Add(boleto);
            await _context.SaveChangesAsync();

            return Created($"api/boletos/{boleto.Id}", boleto.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // É fundamental dar Include(b => b.Banco) para ter o PercentualJuros na memória!
            var boleto = await _context.Boletos
                .Include(b => b.Banco)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (boleto == null) return NotFound("Boleto não encontrado.");

            // O AutoMapper acionará o método CalcularValorComJuros configurado no Profile
            var response = _mapper.Map<BoletoResponseDto>(boleto);
            return Ok(response);
        }
    }
}
