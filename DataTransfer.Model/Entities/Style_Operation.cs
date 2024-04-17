using System.ComponentModel;

namespace DataTransfer.Model.Entities
{
    public class Style_Operation
    {
        public int Id { get; set; }
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
        [DisplayName("İş Ögesi")]
        public int? EntityOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
