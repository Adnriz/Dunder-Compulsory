namespace Service.TransferModels.Requests
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<CreateOrderEntryDto> OrderEntries { get; set; }
    }
}