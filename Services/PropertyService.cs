
using PropertyApi.Dtos;
using PropertyApi.Repositories;

namespace PropertyApi.Services;

public class PropertyService
{
  private readonly PropertyRepository _repository;

  public PropertyService(PropertyRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<PropertyDto>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
  {
    var entities = await _repository.GetPropertiesAsync(name, address, minPrice, maxPrice);

    return entities.Select(p => new PropertyDto
    {
      IdOwner = p.Item1.IdOwner,
      Name = p.Item1.Name,
      Address = p.Item1.Address,
      Price = p.Item1.Price,
      Image = p.Item2?.File ?? string.Empty
    }).ToList();
  }
}