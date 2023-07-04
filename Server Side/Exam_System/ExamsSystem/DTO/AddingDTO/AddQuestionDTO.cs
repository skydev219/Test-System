using ExamsSystem.Models;
using Newtonsoft.Json;

namespace ExamsSystem.DTO
{
    public class AddQuestionDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Exam_ID { get; set; }
        public AnswerDTO Answer { get; set; }
    }
}
