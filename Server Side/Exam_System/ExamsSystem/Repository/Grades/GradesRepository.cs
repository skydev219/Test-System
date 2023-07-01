using ExamsSystem.Models;
using ExamsSystem.Repository.IEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamsSystem.Repository.Grades
{
    public class GradesRepository:IGrades
    {

        #region Fileds
        readonly SchoolContext _dbcontext;
        #endregion

        #region Constructors
        public GradesRepository(SchoolContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Methods

        #region Get
        public async Task<List<Grade>> GetAll()
        {
            return await _dbcontext.Grades.Include(g => g.St).Include(g => g.Exam).ToListAsync();
        }

        public async Task<Grade> GetById(int st_id,int ex_id)
        {
            return await _dbcontext.Grades.Include(g => g.St).Include(g => g.Exam).FirstOrDefaultAsync(a => a.St_ID == st_id && a.Exam_ID == ex_id);
        }
        #endregion

        #region Add
        public async Task Add(Grade entity)
        {
            await _dbcontext.Grades.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task Update( Grade entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteById(int st_id, int ex_id)
        {
            if (await GetById(st_id,ex_id) != null)
            {
                _dbcontext.Grades.Remove(await GetById(st_id, ex_id));
                await _dbcontext.SaveChangesAsync();
            }
        }

        #endregion


        #endregion
    }
}
