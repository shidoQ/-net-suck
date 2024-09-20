namespace GasStationAPI.Models
{
    public class SalesReport
    {
        public int Id { get; set; }
        public string MachineNumber { get; set; }
        public int OilId { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        
        public User User { get; set; }
        public OilType OilType { get; set; }
    }
}
