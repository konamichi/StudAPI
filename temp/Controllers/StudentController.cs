using System;
using Microsoft.AspNetCore.Mvc;
using temp.Data;
using temp.Services;

namespace temp.Controllers
{
	[ApiController]
	[Route("api")]
	public class StudentController : Controller
	{
		private readonly MyService _myService;

		public StudentController(MyService myService)
		{
			_myService = myService;
		}

		[HttpPost]
		[Route("become-stud")]
		public async Task<IActionResult> Become(
			[FromBody] HumanModel human,
			[FromQuery] int fiveXfive,
			[FromQuery] int yourExamMark,
			CancellationToken cancellationToken)
		{
			try
			{
				return Ok(await _myService.BecomeStudentAsync(human, fiveXfive, yourExamMark, cancellationToken));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("exam")]
		public async Task<IActionResult> Exam([FromQuery] int studNumber, [FromQuery] int fiveXfive, CancellationToken cancellationToken)
		{
			try
			{
				return Ok(await _myService.ExamPassAsync(studNumber, fiveXfive, cancellationToken));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

