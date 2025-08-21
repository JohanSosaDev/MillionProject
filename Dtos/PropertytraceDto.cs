namespace RealEstateApi.Dtos
{
    public class PropertyTraceDto
    {
        public string Id { get; set; } = string.Empty;
        public string IdProperty { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DateSale { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}
