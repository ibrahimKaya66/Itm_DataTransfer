namespace DataTransfer.Model.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int FactoryId { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
