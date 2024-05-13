namespace DataTransfer.Model.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MachineGroupId { get; set; }
        public Group MachineGroup { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
