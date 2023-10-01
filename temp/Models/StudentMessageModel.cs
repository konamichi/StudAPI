namespace StudAPI.Data
{
    public class StudentMessageModel
    {
        public int StudNumber { get; set; }

        public string FullName { get; set; }

        public string Birth { get; set; }

        public bool Sex { get; set; }

        public int? Scholarship { get; set; }

        public string Univgroup { get; set; }

        public string? Message { get; set; } 
    }
}