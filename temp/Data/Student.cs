using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudAPI.Data
{
    [Table("student")]
    public class Student
    {
        [Key]
        [Column("studnumber")]
        public int StudNumber { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("birth")]
        public string Birth { get; set; }

        [Column("sex")]
        public bool Sex { get; set; }

        [Column("scholarship")]
        public int? Scholarship { get; set; }

        [Column("univgroup")]
        public string Univgroup { get; set; }
    }
}