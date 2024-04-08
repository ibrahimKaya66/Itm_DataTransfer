namespace DataTransfer.Model.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FactoryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
