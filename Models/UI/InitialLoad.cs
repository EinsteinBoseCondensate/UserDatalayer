using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI
{
    public class InitialLoad
    {
        public ICollection<Group> Groups { get; set; }
        public ICollection<Friendship> Frienships { get; set; }
    }
}
