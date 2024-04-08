using System.ComponentModel;

namespace DataTransfer.Model.Entities
{
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Hedef Verimlilik")]
        public decimal TargetProductivity { get; set; }
        [DisplayName("Lcd No")]
        public int LCDNo { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
