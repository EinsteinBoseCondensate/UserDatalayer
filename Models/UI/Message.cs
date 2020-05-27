using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.UI
{
    public class Message
    {
        public string Content { get; set; }
        public string Created { get; set; }
        public string CreatorName { get; set; }
        public bool IsMine { get; set; }
        public Guid Id { get; set; }//Future ack purposes
    }
}
