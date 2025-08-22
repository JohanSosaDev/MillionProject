using PropertyApi.Models;

namespace PropertyApi.Repositories
{
  public interface IPropertyRepository
  {
    Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize, CancellationToken ct);
  }
}