using UserDataLayer.Models;
namespace UserDataLayer
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("RequestedFriendId")]
        public virtual System.Collections.Generic.ICollection<Friendship> RequestedFrienships { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("RequesterFriendId")]
        public virtual System.Collections.Generic.ICollection<Friendship> ExternalFrienships { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("MemberId")]
        public virtual System.Collections.Generic.ICollection<GroupUser> GroupsBridge { get; set; }
    }
}