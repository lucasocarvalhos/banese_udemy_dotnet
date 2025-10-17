using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models;
using NZWalksAPI.Models.DTO;


[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _context;
    public RegionsController(NZWalksDbContext context)
    {
        _context = context;
    }

    // Listar todas as regiões
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetAllRegions()
    {
        var regions = await _context.Regions.ToListAsync();

        // Mapear Model para DTO 
        var regionsDto = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            });
        }

        return Ok(regionsDto);
    }

    // Listar região por id
    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegionById(Guid id)
    {
        var regionDomain = await _context.Regions.FindAsync(id);
        if (regionDomain == null)
        {
            return NotFound();
        }

        // Map Model para DTO 
        var regionDto = new RegionDto()
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            RegionImageUrl = regionDomain.RegionImageUrl
        };


        return Ok(regionDto);
    }

    // Criar região
    [HttpPost]
    public async Task<ActionResult<Region>> AddRegionRequestDto(AddRegionRequestDto region)
    {

        // DTO para Model
        var regionModel = new Region()
        {
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        await _context.Regions.AddAsync(regionModel);
        await _context.SaveChangesAsync();

        // Model para DTO 
        var regionDto = new RegionDto()
        {
            Id = regionModel.Id,
            Code = regionModel.Code,
            Name = regionModel.Name,
            RegionImageUrl = regionModel.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
    }

    // Atualizar região
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegionRequestDto(Guid id, UpdateRegionRequestDto updateRegion)
    {

        var regionModel = await _context.Regions.FindAsync(id);
        if (regionModel == null)
        {
            return NotFound();
        }

        // Map DTO para Model
        regionModel.Code = updateRegion.Code;
        regionModel.Name = updateRegion.Name;
        regionModel.RegionImageUrl = updateRegion.RegionImageUrl;

        await _context.SaveChangesAsync();

        return NoContent();

    }

    // Deletar região
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        var regionModel = await _context.Regions.FindAsync(id);
        if (regionModel == null)
        {
            return NotFound();
        }

        _context.Regions.Remove(regionModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}