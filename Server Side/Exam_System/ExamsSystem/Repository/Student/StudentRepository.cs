using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Repository
{
    public class StudentRepository : IEntityRepository<Student>, IStudentAuth<Student>
    {
        #region Fileds
        readonly SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public StudentRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Authentication
        #region Login
        public Task<Student> Login(LoginDTO login)
        {
            return _dbcontext
                .Students
                .FirstOrDefaultAsync(s => s.UserName == login.Username && s.Pass == login.Password);
        }
        #endregion

        #region Register
        public async Task Register(Student student)
        {
            await _dbcontext.Students.AddAsync(student);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion


        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _dbcontext.Students.AnyAsync(u => u.UserName == username);
        }
        #endregion

        #region Get
        public async Task<List<Student>> GetAll()
        {
            return await _dbcontext.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _dbcontext
                .Students
                .Include(s => s.Grades)
                .ThenInclude(g => g.Exam)
                .FirstOrDefaultAsync(s => s.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Student std)
        {
            await _dbcontext.Students.AddAsync(std);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update(int id, Student std)
        {
            _dbcontext.Entry(std).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteById(int id)
        {
            if (await IsExisted(id) != null)
            {
                _dbcontext.Students.Remove(await IsExisted(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
        #endregion

        private async Task<Student> IsExisted(int id)
        {
            return await _dbcontext.Students.FirstOrDefaultAsync(q => q.ID == id);
        }



        #endregion
    }
}
