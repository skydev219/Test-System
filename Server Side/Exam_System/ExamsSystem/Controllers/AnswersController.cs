﻿using System;
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
    public class AnswersController : ControllerBase
    {
        #region Fields
        private readonly IEntityRepository<Answer> _context;
        #endregion

        #region Constructors
        public AnswersController(IEntityRepository<Answer> context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        #region Get
        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            IEnumerable<Answer> answers = await _context.GetAll();
            if (answers == null) return NotFound();

            return Ok(answers);
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            Answer? answer = await _context.GetById(id);
            if (answer == null) return NotFound();

            return Ok(answer);
        }
        #endregion

        #region Update
        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, AddAnswerDTO answer)
        {
            Answer ans = await _context.GetById(id);
            if (ans == null) return NotFound();
            if (id != answer.ID) return BadRequest();

            try
            {
                ans.Name= answer.Name;
                
                await _context.Update(id, ans);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetAnswer(id) == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }
        #endregion

        #region Add
        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddAnswerDTO>> PostAnswer(AddAnswerDTO answer)
        {
            if (answer == null) return BadRequest();
            try
            {
                Answer a = new Answer()
                {

                    Name = answer.Name,
                    Q_ID = answer.Q_ID
                };
                await _context.Add(a);
                return CreatedAtAction("GetAnswer", new { id = a.ID }, a);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        #endregion

        #region Delete
        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            Answer? answer = await _context.GetById(id);

            if (answer == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    answer
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
