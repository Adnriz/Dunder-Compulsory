namespace DataAccess.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public ICollection<PaperProperty> PaperProperties { get; set; }
    }
}