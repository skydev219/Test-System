﻿using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Policy = "Student,Admin")]
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

        [Authorize(Policy = "Student,Admin")]
        [HttpGet("{st_id},{ex_id}")]
        public async Task<ActionResult<Grade>> GetGrade(int st_id, int ex_id)
        {
            Grade? gr = await _context.GetById(st_id, ex_id);
            if (gr == null) return NotFound();
            GradesDTO grade = new GradesDTO()
            {
                Student = gr.St.Name,
                Exam = gr.Exam.Name,
                Grade = gr.Grade1
            };
            return Ok(grade);
        }

        // GET: api/Grades/"5"
        [HttpGet]
        [Route("Student/{st_id}")]
        public async Task<ActionResult<GradesDTO>> GetByStudent(int st_id)
        {

            IEnumerable<Grade> grades = await _context.GetByStudent(st_id);
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
        [HttpGet]
        [Route("exam/{ex_id}")]

        public async Task<ActionResult<GradesDTO>> GetByExam(int ex_id)
        {
            IEnumerable<Grade> grades = await _context.GetByExam(ex_id);
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


        #endregion

        #region Update
        // PUT: api/Grades/5
        [Authorize(Policy = "Student,Admin")]
        [HttpPut]
        public async Task<IActionResult> PutGrade(AddGradeDTO grade)
        {
            Grade gr = await _context.GetById(grade.St_ID, grade.Exam_ID);
            if (gr == null) return NotFound();
            try
            {
                gr.Grade1 = grade.Grade1;
                await _context.Update(gr);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetGrade(grade.St_ID, grade.Exam_ID) == null)
                {
                    return NotFound();
                }
            }
            return NoContent();
        }
        #endregion

        #region Add
        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<AddGradeDTO>> PostGrade(AddGradeDTO grade)
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
                return CreatedAtAction("GetGrade", new { st_id = gr.St_ID, ex_id = gr.Exam_ID }, gr);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Delete
        // DELETE: api/Grades/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{st_id},{ex_id}")]
        public async Task<IActionResult> DeleteGrade(int st_id, int ex_id)
        {
            Grade? gr = await _context.GetById(st_id, ex_id);

            if (gr == null) return NotFound();
            try
            {
                await _context.DeleteById(st_id, ex_id);
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
