using System.Runtime.Serialization;

namespace Domain.Models
{
    public class Photo
    {
        [DataMember(Name = "albumId")]
        public int AlbumId { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
