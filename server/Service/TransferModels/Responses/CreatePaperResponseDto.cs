namespace Service.TransferModels.Responses
{
    public class CreatePaperResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Discontinued { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}