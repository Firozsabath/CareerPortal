using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Ldap
{
    public interface IAppUser
    {
       string Username { get; }
        string DisplayName { get; }
        string Email { get; }
        string[] Roles { get; }
    }
}

