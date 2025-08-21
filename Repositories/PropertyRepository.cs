using MongoDB.Bson;
using MongoDB.Driver;
using PropertyApi.Models;
using SharpCompress.Compressors.PPMd;

namespace PropertyApi.Repositories;

public class PropertyRepository
{
  private readonly IMongoCollection<Property> _properties;
  private readonly IMongoCollection<PropertyImage> _images;

  public PropertyRepository(IConfiguration config)
  {
    var client = new MongoClient(config["MongoDB:ConnectionString"]);
    var database = client.GetDatabase(config["MongoDb:DataBase"]);
    _properties = database.GetCollection<Property>("properties");
    _images = database.GetCollection<PropertyImage>("propertyImages");
  }

  public async Task<List<(Property, PropertyImage?)>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
  {
    var filter = Builders<Property>.Filter.Empty;

    if (!string.IsNullOrEmpty(name))
    {
      filter &= Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i"));
    }

    if (!string.IsNullOrEmpty(address))
    {
      filter &= Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(address, "i"));
    }

    if (minPrice.HasValue)
    {
      filter &= Builders<Property>.Filter.Gte(p => p.Price, minPrice.Value);
    }

    if (maxPrice.HasValue)
    {
      filter &= Builders<Property>.Filter.Gte(p => p.Price, maxPrice.Value);
    }

    var properties = await _properties.Find(filter).ToListAsync();

    var results = new List<(Property, PropertyImage?)>();

    foreach (var property in properties)
    {
      var image = await _images.Find(i => i.IdProperty == property.IdProperty && i.Enabled).FirstOrDefaultAsync();
      results.Add((property, image));
    }

    return results;
  }

}