using System.Runtime.Serialization;

namespace Domain.Models
{
    public class Album
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
