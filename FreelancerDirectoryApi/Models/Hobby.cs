using System.Text.Json.Serialization;

namespace FreelancerDirectoryApi.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FreelancerId { get; set; }

        [JsonIgnore]  
        public Freelancer Freelancer { get; set; }
    }
}


