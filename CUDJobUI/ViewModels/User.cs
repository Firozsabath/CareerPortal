using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CudJobUI.ViewModels
{
    public class User
    {
        [Required]
        [Display(Name = "User Name")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string LoginType { get; set; }

        
    }

    public class UserRegister
    {
        [Display(Name = "User Name")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password",ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class Passwordconfirmation
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
        public string token { get; set; }
    }

    public class Accesstoken
    {
        public int ID { get; set; }
        public string token { get; set; }
    }
}
