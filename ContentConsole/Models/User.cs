using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole.Models
{
    /// <summary>
    /// User class maintains the current user and security level
    /// </summary>
    public class User
    {
        public string FirstName = "";
        public string LastName = "";
        public string UserName = "";
        public SecurityType SecType = 0;

        public User(string firstName, string lastName, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            SecType = SecurityType.UnAuthedUser;
        }

        public SecurityType GetSecType()
        {
            //TODO: Add call to RoleManager to verify the role of the user
            return SecType;
        }

    }
}
