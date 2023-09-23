using temp.Data;
using temp.Repositories;

namespace temp.Services
{
    public class MyService
	{
		private readonly MyRepository _myRepository;

		public MyService(MyRepository myRepository)
		{
			_myRepository = myRepository;
		}

		public async Task<StudentModel?> BecomeStudentAsync(HumanModel human, int response, int examMark, CancellationToken cancellationToken)
		{
            _ = await _myRepository.CreateHumanAsync(human, cancellationToken);

			if (response != 25)
				return new StudentModel { Message = "Ответ неверный, вы не будете студентом!" };

			if (examMark < 0)
				throw new Exception("Неверный ввод");

			var random = new Random();

			var newStudent = await _myRepository.CreateStudentAsync(new StudentModel
			{
				FullName = human.FullName,
				Birth = human.Birth,
				Sex = human.Sex,
				StudNumber = random.Next(100000, 200000),
				Scholarship = examMark > 200 ? 2000 : null
			}, cancellationToken);

			return StudentModel.Map(newStudent);
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
				await _myRepository.DeleteStudentAsync(student, cancellationToken);

				return "Поздравляем с отчислением!<3";
			}

			return "Поздравляем с успешной сдачей экзамена!";
		}
	}
}

