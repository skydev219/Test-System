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
            return _dbcontext.Questions.Include(q => q.Answer).FirstOrDefaultAsync(q => q.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Question entity)
        {
            await _dbcontext.Questions.AddAsync(entity);
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
            if (await IsExisted(id) != null)
            {
                _dbcontext.Questions.Remove(await IsExisted(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
        #endregion

        private async Task<Question> IsExisted(int id)
        {
            return await _dbcontext.Questions.FirstOrDefaultAsync(q => q.ID == id);
        }

        #endregion
    }
}
