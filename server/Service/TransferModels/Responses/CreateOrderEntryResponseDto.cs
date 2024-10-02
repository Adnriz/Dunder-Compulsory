namespace Service.TransferModels.Responses
{
    public class CreateOrderEntryResponseDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}