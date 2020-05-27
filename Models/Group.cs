using UserDataLayer.Models;
namespace UserDataLayer
{
    public class Group
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
        public string GroupName { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public virtual System.Collections.Generic.ICollection<GroupUser> Users { get; set; }

    }
}
