using temp.Data;
using temp.Models;

namespace temp.Repositories
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
	}
}

