using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        public string GradeName { get; set; } = "";
        public string Section { get; set; } = "";
        public ICollection<Student>? Students { get; set; }
    }
}
