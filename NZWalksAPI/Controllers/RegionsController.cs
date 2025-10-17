using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;


[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _context;
    private readonly IRegionRepository _regionRepository;
    public RegionsController(NZWalksDbContext context, IRegionRepository regionRepository)
    {
        _context = context;
        _regionRepository = regionRepository;

    }

    // Listar todas as regiões
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetAllRegions()
    {
        var regions = await _regionRepository.GetAllAsync();

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
        var regionDomain = await _regionRepository.GetByIdAsync(id);
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

        regionModel = await _regionRepository.CreateAsync(regionModel);

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

        var regionModel = new Region()
        {
            Code = updateRegion.Code,
            Name = updateRegion.Name,
            RegionImageUrl = updateRegion.RegionImageUrl
        };

        await _regionRepository.UpdateAsync(id, regionModel);

        return NoContent();

    }

    // Deletar região
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        await _regionRepository.DeleteAsync(id);

        return NoContent();
    }
}