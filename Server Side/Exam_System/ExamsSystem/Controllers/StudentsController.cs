using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using ExamsSystem.DTO;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        #region Fields
        private readonly IEntityRepository<Student> _context;
        #endregion

        #region Constructors
        public StudentsController(IEntityRepository<Student> context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        #region Get
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            IEnumerable<Student> stds = await _context.GetAll();
            if ( stds == null) return NotFound();

            return Ok(stds);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            Student? std = await _context.GetById(id);
            if (std == null) return NotFound();

            return Ok(std);
        }
        #endregion

        #region Update
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, AddStudentDTO student)
        {
            Student? std = await _context.GetById(student.ID);
            if (id != std.ID) return BadRequest();
            if (std == null) return NotFound();


            try
            {
                std.Name = student.Name;
                std.UserName = student.UserName;
                std.Pass = student.Pass;
                await _context.Update(id, std);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetStudent(id) == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }
        #endregion

        #region Add
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddStudentDTO>> PostStudent(AddStudentDTO student)
        {

            if (student == null) return BadRequest();
            try
            {
                Student st = new Student()
                {

                    Name = student.Name,
                    UserName = student.UserName,
                    Pass = student.Pass

                };
                await _context.Add(st);
                return CreatedAtAction("GetStudent", new { id = st.ID }, st);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region Delete
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student? std = await _context.GetById(id);

            if (std == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    std
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
