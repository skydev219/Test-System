using Newtonsoft.Json;

namespace ExamsSystem.DTO
{
    public class AddExamDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
