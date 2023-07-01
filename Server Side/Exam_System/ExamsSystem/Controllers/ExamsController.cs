using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        #region Fileds
        IEntityRepository<Exam> _context;
        #endregion

        #region Constructors
        public ExamsController(IEntityRepository<Exam> context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Get
        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            IEnumerable<Exam> exams = await _context.GetAll();
            if (exams == null) return NotFound();

            #region Exams DTO init
            List<ExamDTO> exs = new List<ExamDTO>();

            foreach (Exam exam in exams)
            {
                exs.Add(item: new ExamDTO { ID = exam.ID, Name = exam.Name });
            }
            #endregion

            return Ok(exs);
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            Exam? exam = await _context.GetById(id);
            if (exam == null) return NotFound();

            return Ok(exam);
        }
        #endregion

        #region Add
        // POST: api/Exams
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(ExamDTO exam)
        {
            if (exam == null) return BadRequest();
            try
            {
                Exam e = new Exam()
                {
                    ID = exam.ID,
                    Name = exam.Name,
                };
                await _context.Add(e);
                return CreatedAtAction("GetExam", new { id = exam.ID }, exam);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Update
        // PUT: api/Exams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (exam == null) return NotFound();
            if (id != exam.ID) return BadRequest();

            try
            {
                await _context.Update(id, exam);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetExam(id) == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }
        #endregion

        #region Delete
        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            #region Comments
            //if (_context.GetAll() == null) return NotFound();

            //Exam? exam = await _context.GetById(id);
            //if (exam == null) return NotFound();

            //await _context.DeleteById(id);
            //return NoContent();
            #endregion

            Exam? exam = await _context.GetById(id);

            if (exam == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    exam
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