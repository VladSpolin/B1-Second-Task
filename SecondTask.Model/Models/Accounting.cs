using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondTask.Model.Models
{
    public class Accounting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Account { get; set; }
        public double OpenBalance_A { get; set; }
        public double OpenBalance_P { get; set; }
        public double Turnover_Db { get; set; }
        public double TurnOver_Ct { get; set; }
        public double ClosBalance_A { get; set; }
        public double ClosBalance_P { get; set; }
        public int? FileId { get; set; }
        public Models.File ItsFile { get; set; }
    }
}