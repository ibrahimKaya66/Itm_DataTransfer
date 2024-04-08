namespace DataTransfer.Model.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupCodeId { get; set; }
        public GroupCode GroupCode { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
