using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExamsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        #region Fields
        readonly IJWT _jwt;
        readonly IConfiguration _configuration;
        readonly IAuthentication<Admin> _authentication;

        IEntityRepository<Admin> _context;
        #endregion

        #region Constructors
        public AdminsController(IJWT jWT, IConfiguration configuration, IAuthentication<Admin> authentication, IEntityRepository<Admin> context)
        {
            _jwt = jWT;
            _configuration = configuration;
            _authentication = authentication;
            _context = context;
        }
        #endregion

        #region Methods

        #region Authentication
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {

            #region Check Parameters 
            var EmptyParametersObj = new
            {
                StatusCode = 400,
                message = "Empty Parameters",
            };
            if (login.Username == null || login.Password == null) return BadRequest(EmptyParametersObj);
            #endregion

            Admin? admin = await _authentication.Login(login);

            #region Check is Existed
            var InvalidCredentialObj = new
            {
                StatusCode = 400,
                message = "Invalid Credential",
            };

            if (admin == null)
                return BadRequest(InvalidCredentialObj);
            #endregion

            #region Define Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration[key: "Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(type: "name", admin.Name),
                new Claim(ClaimTypes.Role , "Admin")
            };
            #endregion

            #region Response Formatter
            var response = new
            {

                StatusCode = 200,
                message = "Login successful",
                response = new
                {
                    token = _jwt.GenentateToken(claims, 1),
                    role = "Admin",
                    admin = new
                    {
                        id = admin.ID,
                        name = admin.Name,
                        username = admin.UserName,
                    }
                }

            };
            #endregion

            return Ok(response);
        }
        #endregion

        #region Register

        #endregion

        #region Logout
        //[Authorize(Policy = "Admin")]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {

            // Retrieve the current JWT token from the authentication header
            string? token = HttpContext.Request
                .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            string newToken = _jwt.ClearerToken(token);

            HttpContext.Response.Headers.Add("Authorization", "Bearer ");


            #region Response Formatter
            var response = new
            {

                StatusCode = 200,
                message = "Logout successful",
                response = new
                {
                    token = newToken,
                    role = "Admin",
                }

            };
            #endregion

            return Ok(response);
        }
        #endregion
        #endregion

        #region Get
        // GET: api/Admins
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            IEnumerable<Admin> admin = await _context.GetAll();
            if (admin == null) return NotFound();
            return Ok(admin);
        }

        // GET: api/Admins/5
        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            Admin? admin = await _context.GetById(id);
            if (_context.GetById(id) == null) return NotFound();
            return admin;
        }
        #endregion

        #region Update
        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            if (User == null) return BadRequest();
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
        [Authorize(Policy = "Admin")]
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
