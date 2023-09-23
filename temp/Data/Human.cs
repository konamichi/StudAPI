using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace temp.Data
{
	[Table("human")]
	public class Human
	{
		[Key]
		[Column("passnum")]
		public int PassNumber { get; set; }

		[Column("fullname")]
		public string FullName { get; set; }

		[Column("birth")]
		public string Birth { get; set; }

		[Column("sex")]
		public bool Sex { get; set; }
	}
}

