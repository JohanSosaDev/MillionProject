using PropertyApi.Models;

namespace PropertyApi.Services
{
  public interface IPropertyService
  {
      Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(
          string? name, string? address, decimal? minPrice, decimal? maxPrice,
          int page, int pageSize, CancellationToken ct);
  }
}