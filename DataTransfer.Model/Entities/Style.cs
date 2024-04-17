namespace DataTransfer.Model.Entities
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ReferanceNo { get; set; }
        public int StyleGroupId { get; set; }
        public Group StyleGroup { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int SeasonGroupId { get; set; }
        public Group SeasonGroup { get; set; }
        public int CatalogGroupId { get; set; }
        public Group CatalogGroup { get; set; }
        public int SetGroupId { get; set; }
        public Group SetGroup { get; set; }
        public bool IsArchived { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
