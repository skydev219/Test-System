using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Repository
{
    public class QuestionRepository : IEntityRepository<Question>
    {
        #region Fileds
        readonly SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public QuestionRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Get
        public Task<List<Question>> GetAll()
        {
            return _dbcontext.Questions.ToListAsync();
        }

        public Task<Question> GetById(int id)
        {
            return _dbcontext.Questions.FirstOrDefaultAsync(q => q.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Question entity)
        {
            _dbcontext.Questions.Add(entity);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update(int id, Question entity)
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
