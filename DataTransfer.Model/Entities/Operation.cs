using System.ComponentModel;

namespace DataTransfer.Model.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int OperationGroupId { get; set; }
        public Group OperationGroup { get; set; }
        public int? MachineId { get; set; }
        public Machine? Machine { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [DisplayName("Süre(sn)")]
        public decimal TimeSecond { get; set; }
        [DisplayName("Tolerans")]
        public decimal Tolerance { get; set; } = 0;
        [DisplayName("Süre(dk)")]
        public decimal TimeMinute
        {
            get { return TimeSecond / 60 * (1 + Tolerance / 100); }
            set { /* set metodunu gerekirse implemente edebilirsiniz */ }
        }
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
