using UserDataLayer.Models;

namespace UserDataLayer
{
    public class Message
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
        public string Content { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime? Modified { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("MessageId")]
        public virtual System.Collections.Generic.ICollection<MessageGroupUser> GroupUsers { get; set; }

    }
}
