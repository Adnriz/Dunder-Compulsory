namespace Service.TransferModels.Requests
{
    public class CreateOrderEntryDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}