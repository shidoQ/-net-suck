namespace GasStationAPI.Models
{
    public class OilType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public decimal Price { get; set; }
    }
}
