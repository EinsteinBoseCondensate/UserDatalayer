using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI
{
    public class Friendship
    {
        public Guid Id { get; set; }
        public string FriendName { get; set; }
        public bool Pending { get; set; }
        public bool External { get; set; }

    }
}
