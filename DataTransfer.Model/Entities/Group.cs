namespace DataTransfer.Model.Entities
{
    public class Group
    {
        public string Name { get; set; }
        public int GroupCodeId { get; set; }
        public GroupCode GroupCode { get; set; }

    }
}
