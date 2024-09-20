namespace GasStationAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int OilId { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
        public DateTime ExpenseDate { get; set; }

        public Supplier Supplier { get; set; }
        public OilType OilType { get; set; }
    }
}
