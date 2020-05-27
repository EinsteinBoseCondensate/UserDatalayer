namespace UserDataLayer.Models
{
    public class GroupUser
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
        
        public virtual User Member { get; set; }
        public virtual Group Group { get; set; }
        public bool? IsAdmin { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupUserId")]
        public virtual System.Collections.Generic.ICollection<MessageGroupUser> Messages { get; set; }
    }
}
