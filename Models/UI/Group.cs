using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI
{
    public class Group
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public ICollection<User> GroupUsers { get; set; }
        public ICollection<MessageUser> MessageUsers { get; set; }
    }
}
