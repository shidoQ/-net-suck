namespace GasStationAPI.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string MachineNumber { get; set; }
        public string CompanyName { get; set; }
        public string MachineDescription { get; set; }
        public int OilId { get; set; }
        
        public OilType OilType { get; set; }
    }
}
