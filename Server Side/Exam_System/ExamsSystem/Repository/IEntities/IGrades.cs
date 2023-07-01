using ExamsSystem.Models;

namespace ExamsSystem.Repository.IEntities
{
    public interface IGrades
    {
        public Task<List<Grade>> GetAll();
        public Task Add(Grade entity);
        public Task Update(Grade entity);
        public Task DeleteById(int st_id, int ex_id);
        public Task<Grade> GetById(int st_id, int ex_id);

    }
}
