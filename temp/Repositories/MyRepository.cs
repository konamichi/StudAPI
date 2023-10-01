using StudAPI.Data;
using StudAPI.Models;

namespace StudAPI.Repositories
{
    public class MyRepository
	{
		private readonly DataContext _dataContext;

		public MyRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<Human> CreateHumanAsync(HumanModel human, CancellationToken cancellationToken)
		{
			var newHuman = new Human
			{
				FullName = human.FullName,
				Birth = human.Birth,
				Sex = human.Sex
			};

            await _dataContext.People.AddAsync(newHuman, cancellationToken);
			await _dataContext.SaveChangesAsync(cancellationToken);

			return newHuman;
		}

		public async Task<Student> CreateStudentAsync(StudentModel student, CancellationToken cancellationToken)
		{
			var newStudent = new Student
			{
				FullName = student.FullName,
				Birth = student.Birth,
				StudNumber = student.StudNumber,
				Scholarship = student.Scholarship,
				Sex = student.Sex
			};

            await _dataContext.Students.AddAsync(newStudent, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return newStudent;
		}

		public Student? FindStudent(int studNumber)
		{
			return _dataContext.Students.FirstOrDefault(s => s.StudNumber == studNumber);
		}

		public async Task DeleteStudentAsync(StudentModel student, CancellationToken cancellationToken)
		{
			_dataContext.Students.Remove(new Student
			{
				StudNumber = student.StudNumber,
				FullName = student.FullName,
				Birth = student.Birth,
				Scholarship = student.Scholarship,
				Sex = student.Sex
			});

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

		public async Task ChangeStudentAsync(Student existStudent, StudentModel student, CancellationToken cancellationToken)
		{
			existStudent.StudNumber = student.StudNumber;
			existStudent.Birth = student.Birth;
			existStudent.Scholarship = student.Scholarship;
			existStudent.Sex = student.Sex;

			await _dataContext.SaveChangesAsync(cancellationToken);
		}

		public string? GetFirstEmptyGroup()
		{
			return _dataContext.Students
				.GroupBy(student => student.Univgroup)
				.Where(s => s.Count() < 25)
				.Select(s => s.Key)
				.FirstOrDefault();
		}

		public async Task<string> CreateGroupAsync(CancellationToken cancellationToken)
		{
			var random = new Random();
			string groupName = null;

			do
			{
				groupName = $"ИДБ-{random.Next(10, 21)}-{random.Next(random.Next(1, 10))}";
			}
			while (!CheckExistingGroup(groupName));

			await _dataContext.Groups.AddAsync(new Group
			{
				Gr_name = groupName
			});
			await _dataContext.SaveChangesAsync(cancellationToken);

			return groupName;
		}

		private bool CheckExistingGroup(string groupName)
		{
			return _dataContext.Groups.FirstOrDefault(g => g.Gr_name == groupName) == null ? false : true;
		}
	}
}

