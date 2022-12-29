using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.DTOs
{
    public class UserDTO
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string LoginType { get; set; }
    }

    public class UserRegisterDTO
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class PasswordReset
    {
        public string Email { get; set; }
    }

    public class Passwordconfirmation
    {       
        public string Email { get; set; }       
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string token { get; set; }
    }
}
