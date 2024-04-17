namespace DataTransfer.Model.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UtcOffset { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
