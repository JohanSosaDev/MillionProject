
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertyApi.Dtos;
using PropertyApi.Services;

namespace PropertyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
  private readonly IPropertyService _service;
  private readonly IMapper _mapper;

  public PropertiesController(IPropertyService service, IMapper mapper)
  {
    _service = service;
    _mapper = mapper;
  }


  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
  {
    if (page <= 0 || pageSize <= 0)
      return BadRequest("Page and pageSize must be greater than zero.");

    var (items, total) = await _service.SearchAsync(name, address, minPrice, maxPrice, page, pageSize, ct);

    if (items == null || !items.Any())
      return NotFound("No properties found matching the criteria.");

    var itemsResponse = items.Select(_mapper.Map<PropertyDto>);

    return Ok(new
    {
      Items = itemsResponse,
      Total = total,
      Page = page,
      PageSize = pageSize
    });
  }
}