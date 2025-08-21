using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PropertyApi.Models
{
  public class PropertyTrace
  {
    [BsonId]
    public ObjectId Id { get; set; }

    public ObjectId IdProperty { get; set; }

    public string Name { get; set; } = string.Empty;
    public DateTime DateSale { get; set; }
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
  }
}
