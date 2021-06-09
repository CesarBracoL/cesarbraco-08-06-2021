using System.Runtime.Serialization;

namespace Domain.Models
{
    public class Comment
    {
        [DataMember(Name = "postId")]
        public int PostId { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }
    }
}
