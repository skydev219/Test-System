using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (question == null) return NotFound();
            if (id != question.ID) return BadRequest();

            try
            {
                await _context.Update(id, question);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            if (question == null) return BadRequest();
            try
            {
                Question q = new Question()
                {
                    Name = question.Name,
                    Exam_ID = question.Exam_ID,
                };
                await _context.Add(q);
                return CreatedAtAction("GetQuestion", new { id = q.ID }, question);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        #endregion

        #region Delete
        // DELETE: api/Questions/5
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
