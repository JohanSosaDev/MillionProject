
using Microsoft.AspNetCore.Mvc;
using PropertyApi.Services;

namespace PropertyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
  private readonly PropertyService _service;

  public PropertiesController(PropertyService service)
  {
    _service = service;
  }


  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
  {
    var result = await _service.GetPropertiesAsync(name, address, minPrice, maxPrice);

    return Ok(result);
  }
}