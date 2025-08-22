using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PropertyApi.Config;
using PropertyApi.Models;

namespace PropertyApi.Repositories;

public class PropertyRepository : IPropertyRepository
{
  private readonly IMongoCollection<Property> _col;

  public PropertyRepository(IMongoDatabase db, IOptions<MongoSettings> options)
  { 
    _col = db.GetCollection<Property>(options.Value.PropertiesCollection);
  }

  public async Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int page, int pageSize, CancellationToken ct)
  {
    var filters = new List<FilterDefinition<Property>>();
    var builder = Builders<Property>.Filter;

    if (!string.IsNullOrEmpty(name))
      filters.Add(builder.Regex(p => p.Name, new BsonRegularExpression(name, "i")));

    if (!string.IsNullOrEmpty(address))
      filters.Add(builder.Regex(p => p.Name, new BsonRegularExpression(address, "i")));

    if (minPrice.HasValue)
      filters.Add(builder.Gte(p => p.Price, minPrice.Value));

    if (maxPrice.HasValue)
      filters.Add(builder.Lte(p => p.Price, maxPrice.Value));

    var filter = filters.Count > 0 ? builder.And(filters) : FilterDefinition<Property>.Empty;

    var find = _col.Find(filter)
      .Project(Builders<Property>.Projection
        .Include(x => x.IdOwner)
        .Include(x => x.Name)
        .Include(x => x.Address)
        .Include(x => x.Price)
        .Include(x => x.CodeInternal)
        .Include(x => x.Year)).Sort(Builders<Property>.Sort.Ascending(x => x.Name)).Skip((page - 1) * pageSize).Limit(pageSize);

    var items = find.ToListAsync(ct);
    var total = _col.CountDocumentsAsync(filter, cancellationToken: ct);
    await Task.WhenAll(items, total);

    var typedItems = await _col.Find(filter).SortBy(x => x.Name)
      .Skip((page - 1) * pageSize)
      .Limit(pageSize)
      .ToListAsync(ct);

    return (typedItems, total.Result);
  }
}