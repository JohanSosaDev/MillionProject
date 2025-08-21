using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PropertyApi.Models;

public class PropertyImage
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string IdPropertyImage { get; set; } = string.Empty;


  [BsonRepresentation(BsonType.ObjectId)]
  public string IdProperty { get; set; } = string.Empty;
  public string File { get; set; } = string.Empty;
  public bool Enabled { get; set; }
}