using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.VIewModels
{
    public class CompanyViewModel
    {
        //public UserDTO CompanyUserName { get; set; }

        public CompanyDTO Companies { get; set; }

        public CompanyContactDTO CompanyContacts { get; set; }

        public AddressDTO CompanyAddress { get; set; }

        public CompanyOptionalDTO CompanyOptional { get; set; }

        public CompanyCategory Companycategory { get; set; }

        public CompanySectors CompanySectors { get; set; }

        public CountryCode Country { get; set; }
        
    }
}
