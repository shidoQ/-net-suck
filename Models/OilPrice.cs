namespace GasStationAPI.Models
{
    public class OilPrice
    {
        public int Id { get; set; }
        public int OilId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}
