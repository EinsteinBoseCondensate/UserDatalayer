using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI.CUD
{
    public class FrienshipRequestModel
    {
        public Guid RequesterId { get; set; }
        public Guid RequestedId { get; set; }
    }
}
