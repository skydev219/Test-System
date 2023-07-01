using ExamsSystem.Models;

namespace ExamsSystem.Repository.IEntities
{
    public interface IExam
    {
        public Task<Exam> GetWithQuetions(int id);
        public Task<Exam> GetWithAdmin(int id);
    }
}
