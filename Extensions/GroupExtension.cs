using System;
using System.Collections.Generic;
using System.Text;
using UserDataLayer.Models;

namespace UserDataLayer.Extensions
{
    public static class GroupExtension
    {
        public static Group ToActualGroup(this Models.UI.Group group)
        {
            return new Group
            {
                GroupName = group.Name,
                Id = Guid.NewGuid()
            };
        }
    }
}
