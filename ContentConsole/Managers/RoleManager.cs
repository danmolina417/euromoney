using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentConsole.Models;

namespace ContentConsole.Managers
{
    public class RoleManager
    {
        public SecurityType GetUserRole(User user)
        {
            // TODO: Remove hardcoded secTypes - used for testing purposes
            if (user.UserName == "dmolina")
            {
                return SecurityType.AdminUser;
            }
            if (user.UserName == "jdoe")
            {
                return SecurityType.Curator;
            }
            if (user.UserName == "bsmith")
            {
                return SecurityType.Reader;
            }

            // TODO: Call Authentication/Permissions retrieval service
            return SecurityType.UnAuthedUser;
        }
    }
}
