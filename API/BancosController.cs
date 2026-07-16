using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Infrastructure.Data;
using Application.DTOs;
using Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BancosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BancosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BancoCreateDto dto)
    {
        var banco = _mapper.Map<Banco>(dto);
        _context.Bancos.Add(banco);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByCodigo), new { codigo = banco.Codigo }, _mapper.Map<BancoResponseDto>(banco));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bancos = await _context.Bancos.AsNoTracking().ToListAsync();
        return Ok(_mapper.Map<IEnumerable<BancoResponseDto>>(bancos));
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> GetByCodigo(string codigo)
    {
        var banco = await _context.Bancos.AsNoTracking().FirstOrDefaultAsync(b => b.Codigo == codigo);
        if (banco == null) return NotFound("Banco não encontrado.");
        return Ok(_mapper.Map<BancoResponseDto>(banco));
    }
}