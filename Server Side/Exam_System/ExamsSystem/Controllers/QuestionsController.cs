using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        #region Fields
        private readonly IEntityRepository<Question> _context;
        #endregion

        #region Constructors
        public QuestionsController(IEntityRepository<Question> context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        #region Get
        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            IEnumerable<Question> questions = await _context.GetAll();
            if (questions == null) return NotFound();

            return Ok(questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            Question? question = await _context.GetById(id);
            if (question == null) return NotFound();

            return Ok(question);
        }
        #endregion

        #region Update
        // PUT: api/Questions/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, AddQuestionDTO question)
        {
            Question que = await _context.GetById(id);
            if (que == null) return NotFound();
            if (id != question.ID) return BadRequest();

            try
            {
                que.Name = question.Name;
                que.Answer.Name = question.Answer.Name;
                await _context.Update(id, que);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetQuestion(id) == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        #endregion

        #region Add
        // POST: api/Questions
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AddQuestionDTO>> PostQuestion(AddQuestionDTO question)
        {
            if (question == null) return BadRequest();
            try
            {
                Answer a = new Answer()
                {
                    Name = question.Answer.Name,
                };
                Question q = new Question()
                {
                    Name = question.Name,
                    Exam_ID = question.Exam_ID,
                    Answer = a
                };
                await _context.Add(q);
                return CreatedAtAction("GetQuestion", new { id = q.ID }, q);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region Delete
        // DELETE: api/Questions/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            Question? question = await _context.GetById(id);

            if (question == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    question
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
        #endregion
    }
}
