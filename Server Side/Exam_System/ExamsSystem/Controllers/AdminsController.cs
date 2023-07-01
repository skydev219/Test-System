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
    public class AdminsController : ControllerBase
    {
        #region Fields
        IEntityRepository<Admin> _context;
        #endregion
        #region Constructors
        public AdminsController(IEntityRepository<Admin> context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        #region Get
        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            IEnumerable<Admin> ads = await _context.GetAll();
            if (ads == null) return NotFound();
            return Ok(ads);
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            Admin? ad = await _context.GetById(id);
            if (_context.GetById(id) == null) return NotFound();
            return ad;
        }
        #endregion

        #region Update
        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (admin == null) return NotFound();
            if (id != admin.ID) return BadRequest();

            try
            {
                await _context.Update(id, admin);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetAdmin(id) == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }
        #endregion

        #region Add
        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            if (admin == null) return BadRequest();
            try
            {
                Admin a = new Admin()
                {
                    ID = admin.ID,
                    Name = admin.Name,
                    UserName = admin.UserName,
                    Pass = admin.Pass
                };
                await _context.Add(a);
                return CreatedAtAction("GetAdmin", new { id = admin.ID }, admin);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        #endregion

        #region Delete
        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            Admin? admin = await _context.GetById(id);

            if (admin == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    admin
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


//#region Fields
//#endregion
//#region Constructors
//#endregion
//#region Methods
//#endregion
