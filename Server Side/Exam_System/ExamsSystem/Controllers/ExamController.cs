using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Mvc;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        #region Fileds
        readonly IEntityRepository<Exam> _entityRepository;
        readonly IExam _entity;
        #endregion

        #region Constructor
        public ExamController(IEntityRepository<Exam> _entityRepository, IExam _entity)
        {
            this._entityRepository = _entityRepository;
            this._entity = _entity;
        }
        #endregion

        #region Methods

        #region Get

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            IEnumerable<Exam> exams = await _entityRepository.GetAll();
            if (exams == null) return NotFound();

            List<ExamDTO> exs = new List<ExamDTO>();

            foreach (Exam exam in exams)
            {
                exs.Add(item: new ExamDTO { ID = exam.ID, Name = exam.Name });
            }
            return Ok(exs);
        }

        [HttpGet("{id}")]
        public IActionResult getwithquetions(int id)
        {
            Task<Exam>? exam = _entity.GetWithQuetions(id);

            if (exam == null) return BadRequest();

            #region Quetion Answer DTO
            List<QuetionAnswerDTO> questions = new List<QuetionAnswerDTO>();
            foreach (Question quetion in exam.Result.Questions)
            {
                QuetionAnswerDTO quetionAnswerDTO = new QuetionAnswerDTO()
                {
                    Quetion = quetion.Name,
                    Answer = quetion.Answer.Name
                };
                questions.Add(quetionAnswerDTO);
            }
            #endregion

            #region  Exam DTO init
            ExamQuetionsDTO examQuetionsDTO = new ExamQuetionsDTO()
            {
                ID = exam.Result.ID,
                Name = exam.Result.Name,
                Answers = questions
            };
            #endregion

            return Ok(examQuetionsDTO);

        }
        #endregion

        #region Add

        #endregion

        #region Update

        #endregion

        #region Delete
        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {

            if (_entityRepository.GetAll() == null) return NotFound();

            Exam? exam = await _entityRepository.GetById(id);
            if (exam == null) return NotFound();

            await _entityRepository.DeleteById(id);

            return NoContent();
        }
        #endregion

        #endregion
    }
}
