namespace UserDataLayer.Models
{
    public class MessageGroupUser
    {
        public System.Guid Id { get; set; }
        public virtual Message Message { get; set; }
        public virtual GroupUser GroupUser { get; set; }
        public string CreatorName { get; set; }
        public bool IsCreator { get; set; }
        public bool AlreadyReceived { get; set; }
    }
}
