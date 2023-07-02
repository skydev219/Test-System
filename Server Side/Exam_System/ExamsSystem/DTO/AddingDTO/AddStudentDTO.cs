using Newtonsoft.Json;

namespace ExamsSystem.DTO
{
    public class AddStudentDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }

    }
}
