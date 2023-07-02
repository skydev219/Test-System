using Newtonsoft.Json;

namespace ExamsSystem.DTO
{
    public class AddAnswerDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Q_ID { get; set; }

    }
}
