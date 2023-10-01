using StudAPI.Data;
using StudAPI.Models;
using StudAPI.Repositories;

namespace StudAPI.Services
{
    public class MyService
	{
		private readonly MyRepository _myRepository;

		public MyService(MyRepository myRepository)
		{
			_myRepository = myRepository;
		}

		public async Task<StudentMessageModel?> BecomeStudentAsync(HumanModel human, int response, int examMark, CancellationToken cancellationToken)
		{
            _ = await _myRepository.CreateHumanAsync(human, cancellationToken);

			if (response != 25)
				return new StudentMessageModel { Message = "Ответ неверный, вы не будете студентом!" };

			if (examMark < 0)
				throw new Exception("Неверный ввод");

			var random = new Random();

            var createdStudent = await _myRepository.CreateStudentAsync(new StudentModel
            {
                FullName = human.FullName,
                Birth = human.Birth,
                Sex = human.Sex,
                StudNumber = random.Next(100000, 200000),
                Scholarship = examMark > 200 ? 2000 : null,
				Univgroup = await GetGroupAsync(cancellationToken)
            }, cancellationToken);

			return new StudentMessageModel
			{
				FullName = createdStudent.FullName,
				Birth = createdStudent.Birth,
				Scholarship = createdStudent.Scholarship,
				StudNumber = createdStudent.StudNumber,
				Sex = createdStudent.Sex,
				Univgroup = createdStudent.Univgroup
			};
		}

		public async Task<string> ExamPassAsync(int studNumber, int response, CancellationToken cancellationToken)
		{
			if (studNumber < 0)
				throw new Exception("Студенческий номер введен неверно");

			var student = _myRepository.FindStudent(studNumber);

			if (student == null)
				throw new Exception("Студент не существует");

			if (response != 25)
			{
				await _myRepository.DeleteStudentAsync(new StudentModel
                {
                    FullName = student.FullName,
                    Birth = student.Birth,
                    Sex = student.Sex,
                    StudNumber = student.StudNumber,
                    Scholarship = student.Scholarship,
					Univgroup = student.Univgroup
                }, cancellationToken);

				return "Поздравляем с отчислением!<3";
			}

			return "Поздравляем с успешной сдачей экзамена!";
		}

		public async Task<StudentMessageModel> ChangeStudentAsync(int studNumber, StudentModel student, CancellationToken cancellationToken)
		{
			if (studNumber < 100000 || studNumber > 999999)
				throw new Exception("Некорректно введен студенческий номер");

			if (student == null)
				throw new Exception("Студент не может быть null");

			var existStudent = _myRepository.FindStudent(studNumber);

			if (existStudent == null)
				return new StudentMessageModel { Message = "Студента не существует" };

			await _myRepository.ChangeStudentAsync(existStudent, student, cancellationToken);

			return new StudentMessageModel
			{
				Message = "Данные успешно обновлены",
				FullName = student.FullName,
				Birth = student.Birth,
				Sex = student.Sex,
				Scholarship = student.Scholarship,
				StudNumber = student.StudNumber,
				Univgroup = student.Univgroup
			};
		}

		private async Task<string> GetGroupAsync(CancellationToken cancellationToken)
		{
			string groupName = null;

            if (_myRepository.GetFirstEmptyGroup() == null)
			{
				groupName = await _myRepository.CreateGroupAsync(cancellationToken);
			}

			return groupName!;
		}
	}
}

