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
    public class StudentsController : ControllerBase
    {
        #region Fields
        readonly IJWT _jwt;
        readonly IConfiguration _configuration;
        readonly IStudentAuth<Student> _authentication;

        IEntityRepository<Student> _context;

        #endregion

        #region Constructors
        public StudentsController(IJWT jWT, IConfiguration configuration, IEntityRepository<Student> context, IStudentAuth<Student> studentAuth)
        {
            _jwt = jWT;
            _configuration = configuration;
            _context = context;
            _authentication = studentAuth;
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

            Student? student = await _authentication.Login(login);

            #region Check is Existed
            var InvalidCredentialObj = new
            {
                StatusCode = 400,
                message = "Invalid Credential",
            };

            if (student == null)
                return BadRequest(InvalidCredentialObj);
            #endregion

            #region Define Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration[key: "Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(type: "name", student.Name),
                new Claim("username",student.UserName),
                new Claim(ClaimTypes.Role , "Student")
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
                    role = "Student",
                    student = new
                    {
                        id = student.ID,
                        name = student.Name,
                        username = student.UserName,
                    }
                }

            };
            #endregion

            return Ok(response);
        }

        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromForm] Student student)
        {
            if (student == null) return BadRequest();
            if (await _authentication.IsUsernameTakenAsync(student.UserName))
                return BadRequest(new { StatusCode = 400, message = "This Username Had Taken" });
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
        #endregion

        #region Get
        // GET: api/Students
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            IEnumerable<Student> stds = await _context.GetAll();
            if (stds == null) return NotFound();

            return Ok(stds);
        }

        // GET: api/Students/5
        [Authorize(Roles = "Student,Admin")]
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
        [Authorize(Policy = "Student,Admin")]
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
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AddStudentDTO>> PostStudent(AddStudentDTO student)
        {

            if (student == null) return BadRequest();
            if (await _authentication.IsUsernameTakenAsync(student.UserName))
                return BadRequest(new { StatusCode = 401, message = "This Username Had Taken" });
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
        [Authorize(Roles = "Student,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student? student = await _context.GetById(id);

            if (student == null) return NotFound();
            try
            {
                await _context.DeleteById(id);
                var response = new
                {
                    message = "Deleted Success",
                    student
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
