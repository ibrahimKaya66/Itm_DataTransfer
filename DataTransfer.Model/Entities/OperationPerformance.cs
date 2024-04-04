using System.ComponentModel;

namespace DataTransfer.Model.Entities
{
    public class OperationPerformance
    {
        [DisplayName("Tarih")]
        public DateTime Date_ { get; set; } = DateTime.Now;
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
        public int OperatorId { get; set; }
        public Employee Operator { get; set; }
        [DisplayName("Performans")]
        public decimal Performance { get; set; }
        public int LineId { get; set; }
        public Line Line { get; set; }
    }
}
