using ExamsSystem.Models;

namespace ExamsSystem.DTO
{
    public class ExamDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Question>? Questions { get; set; }
    }
}
