namespace temp.Data
{
    public class StudentModel
    {
        public int StudNumber { get; set; }

        public string FullName { get; set; }

        public string Birth { get; set; }

        public bool Sex { get; set; }

        public int? Scholarship { get; set; }

        public string? Message { get; set; } 

        public static StudentModel? Map(Student student) => student == null ? null : new StudentModel
        {
            StudNumber = student.StudNumber,
            FullName = student.FullName,
            Birth = student.Birth,
            Scholarship = student.Scholarship,
            Sex = student.Sex
        };
    }
}