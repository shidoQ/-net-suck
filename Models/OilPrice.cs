namespace GasStationAPI.Models
{
    public class OilPrice
    {
        public int Id { get; set; }
        public int OilId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        // Navigation property
        public OilType OilType { get; set; }
    }
}
