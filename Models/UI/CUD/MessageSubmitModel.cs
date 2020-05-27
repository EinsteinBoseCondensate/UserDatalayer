using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI.CUD
{
    public class MessageSubmitModel
    {
        public Guid GroupId { get; set; }
        public Guid CreatorGroupUserId { get; set; }    
        public Message MessageUI { get; set; }
    }
}
