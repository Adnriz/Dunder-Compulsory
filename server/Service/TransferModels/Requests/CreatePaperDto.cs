namespace Service.TransferModels.Requests
{
    public class CreatePaperDto
    {
        public string Name { get; set; }
        public bool Discontinued { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}