using System;
using System.Threading;
using temp.Data;

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

		public async Task DeleteStudentAsync(Student student, CancellationToken cancellationToken)
		{
			_dataContext.Students.Remove(student);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
	}
}

