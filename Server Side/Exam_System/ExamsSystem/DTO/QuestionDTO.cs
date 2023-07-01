namespace ExamsSystem.DTO
{
    public class QuestionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public AnswerDTO Answer { get; set; }
    }
}
