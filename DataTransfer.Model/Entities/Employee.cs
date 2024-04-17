using System.ComponentModel;

namespace DataTransfer.Model.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [DisplayName("Ad Soyad")]
        public string FullName { get; set; }
        public bool IsWork { get; set; } = true;
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int JobId { get; set; }
        public int ExpenseTypeId { get; set; }//çalışan gider tipi
        public int? SourceId { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
