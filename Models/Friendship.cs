namespace UserDataLayer.Models
{
    public class Friendship
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }
        public bool IsPending { get; set; }
        public virtual User RequesterFriend { get; set; }
        public virtual User RequestedFriend { get; set; }

    }
}
