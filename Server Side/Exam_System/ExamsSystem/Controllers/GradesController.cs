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
using System.Diagnostics;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        #region Fields
        private readonly IGrades _context;
        #endregion

        #region Constructors
        public GradesController(IGrades context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        #region Get

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradesDTO>>> GetGrades()
        {

            IEnumerable<Grade> grades = await _context.GetAll();
            if (grades == null) return NotFound();

            #region Exams DTO init
            List<GradesDTO> grs = new List<GradesDTO>();

            foreach (var grade in grades)
            {
                grs.Add(item: new GradesDTO
                {
                    Student = grade.St.Name,
                    Exam = grade.Exam.Name,
                    Grade = grade.Grade1
                });
            }
            #endregion

            return Ok(grs);
        }

        // GET: api/Grades/5
        [HttpGet("{st_id},{ex_id}")]
        public async Task<ActionResult<Grade>> GetGrade(int st_id, int ex_id)
        {
            Grade? gr = await _context.GetById(st_id,ex_id);
            if (gr == null) return NotFound();
            GradesDTO grade = new GradesDTO()
            {
                Student = gr.St.Name,
                Exam = gr.Exam.Name,
                Grade = gr.Grade1
            };
            return Ok(grade);
        }
        #endregion

        #region Update
        // PUT: api/Grades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutGrade(Grade grade)
        {
            if (grade == null) return NotFound();
            try
            {
                await _context.Update(grade);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetGrade(grade.St_ID,grade.Exam_ID) == null)
                {
                    return NotFound();
                }
            }
            return NoContent();
        }
        #endregion

        #region Add
        // POST: api/Grades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            if (grade == null) return BadRequest();
            try
            {
                Grade gr = new Grade()
                {
                    St_ID = grade.St_ID,
                    Exam_ID = grade.Exam_ID,
                    Grade1 = grade.Grade1
                };
                await _context.Add(gr);
                return CreatedAtAction("GetGrade", new {st_id = gr.St_ID,ex_id=gr.Exam_ID}, gr);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Delete
        // DELETE: api/Grades/5
        [HttpDelete("{st_id},{ex_id}")]
        public async Task<IActionResult> DeleteGrade(int st_id,int ex_id)
        {
            Grade? gr = await _context.GetById(st_id,ex_id);

            if (gr == null) return NotFound();
            try
            {
                await _context.DeleteById(st_id,ex_id);
                var response = new
                {
                    message = "Deleted Success",
                    gr
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
