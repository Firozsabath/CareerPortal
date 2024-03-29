﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Ldap
{
    public class AppUser : IAppUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }
    }
}
