using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Repository
{
    public class AnswerRepository : IEntityRepository<Answer>
    {
        #region Fileds
        readonly SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public AnswerRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Get
        public async Task<List<Answer>> GetAll()
        {
            return await _dbcontext.Answers.ToListAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            return await _dbcontext.Answers.FirstOrDefaultAsync(a => a.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Answer entity)
        {
            _dbcontext.Answers.Add(entity);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update(int id, Answer entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteById(int id)
        {
            _dbcontext.Remove(GetById(id));
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}
