namespace GasStationAPI.Models
{
    public class Tanker
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int OilId { get; set; }
        public string TankerNumber { get; set; }
        public DateTime TankerDate { get; set; }
        public float Quantity { get; set; }
        public string TankerDescription { get; set; }
        
        public Supplier Supplier { get; set; }
        public OilType OilType { get; set; }
    }
}
