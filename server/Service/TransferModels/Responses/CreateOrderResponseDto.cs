namespace Service.TransferModels.Responses
{
    public class CreateOrderResponseDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<CreateOrderEntryResponseDto> OrderEntries { get; set; }
    }
}