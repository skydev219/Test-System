namespace ExamsSystem.DTO
{
    public class ExamQuetionsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<QuetionAnswerDTO> Answers { get; set; }
    }
}
