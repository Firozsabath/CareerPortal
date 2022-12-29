using CUDJobAPiIdentity.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Contracts
{
    public interface IAuthenticationService
    {
       IAppUser Login(string Username, string Password);
    }
}
