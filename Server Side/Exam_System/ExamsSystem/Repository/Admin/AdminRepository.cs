using ExamsSystem.DTO;
using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Repository
{
    public class AdminRepository : IEntityRepository<Admin>, IAuthentication<Admin>
    {
        #region Fileds
        readonly SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public AdminRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Authentication
        public async Task<Admin> Login(LoginDTO login)
        {
            return await _dbcontext
                .Admins
                .FirstOrDefaultAsync(a => a.UserName == login.Username && a.Pass == login.Password);
        }
        #endregion

        #region Get
        public async Task<List<Admin>> GetAll()
        {
            return await _dbcontext.Admins.ToListAsync();
        }

        public async Task<Admin> GetById(int id)
        {
            return await _dbcontext.Admins.FirstOrDefaultAsync(s => s.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Admin admin)
        {
            await _dbcontext.Admins.AddAsync(admin);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update(int id, Admin admin)
        {
            _dbcontext.Entry(admin).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteById(int id)
        {
            if (await IsExisted(id) != null)
            {
                _dbcontext.Admins.Remove(await IsExisted(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
        #endregion

        private async Task<Admin> IsExisted(int id)
        {
            return await _dbcontext.Admins.FirstOrDefaultAsync(q => q.ID == id);
        }



        #endregion
    }
}
