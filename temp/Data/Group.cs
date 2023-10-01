using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudAPI.Data
{
	[Table("group")] 
	public class Group
	{
        [Key]
        [Column("gr_name")]
        public string Gr_name { get; set; }
    }
}

