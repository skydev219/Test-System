using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.EntityFrameworkCore;

namespace ExamsSystem.Repository
{
    public class ExamRepository : IEntityRepository<Exam>, IExam
    {
        #region Fileds
        SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public ExamRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Get
        public async Task<List<Exam>> GetAll()
        {
            return await _dbcontext.Exams.ToListAsync();
        }

        public async Task<Exam> GetById(int id)
        {
            return await _dbcontext
                .Exams
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answer)
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<Exam> GetWithAdmin(int id)
        {
            return await _dbcontext.Exams.Include(e => e.Admin).FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<Exam> GetWithQuetions(int id)
        {
            return await _dbcontext
                .Exams
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answer)
                .FirstOrDefaultAsync(e => e.ID == id);
        }
        #endregion

        #region Add
        public async Task Add(Exam exam)
        {

            await _dbcontext.Exams.AddAsync(exam);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update(int id, Exam exam)
        {
            _dbcontext.Entry(exam).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }

        #endregion

        #region Delete
        public async Task DeleteById(int id)
        {
            if (await IsExisted(id) != null)
            {
                _dbcontext.Exams.Remove(await IsExisted(id));
                await _dbcontext.SaveChangesAsync();
            }
        }
        #endregion

        private async Task<Exam> IsExisted(int id)
        {
            return await _dbcontext.Exams.FirstOrDefaultAsync(dep => dep.ID == id);
        }
        #endregion
    }
}
