
using PropertyApi.Dtos;
using PropertyApi.Models;
using PropertyApi.Repositories;

namespace PropertyApi.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _repo;
    public PropertyService(IPropertyRepository repo) => _repo = repo;

    public Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(
        string? name, string? address, decimal? minPrice, decimal? maxPrice,
        int page, int pageSize, CancellationToken ct)
        => _repo.SearchAsync(name, address, minPrice, maxPrice, page, pageSize, ct);
}