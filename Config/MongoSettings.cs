namespace PropertyApi.Config
{
  public class MongoSettings
  {
    public string ConnectionString { get; set; } = default!;
    public string Database { get; set; } = default!;
    public string PropertiesCollection { get; set; } = "properties";
  }
}