using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataLayer.Models.DTO
{
    public class UsersSearchPaginationModel
    {
        public ICollection<Guid> OwnIds { get; set; }
        public string NameFragment { get; set; }
        public int SkippingResults { get; set; }
    }
}
