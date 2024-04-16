namespace DataTransfer.Model.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
