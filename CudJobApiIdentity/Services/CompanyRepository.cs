using AutoMapper;
using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.DTOs;
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Data;
using CUDJobAPiIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _Mapper;
        private readonly ISupportFunction _SupportFunction;
        private readonly UserManager<IdentityUser> _userManager;

        public CompanyRepository(ApplicationDbContext db, IMapper Mapper, ISupportFunction SupportFunction, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _Mapper = Mapper;
            _SupportFunction = SupportFunction;
            _userManager = userManager;
        }        
        public async Task<CompanyViewModel> Create(CompanyViewModel entity)
        {

            Companies company = new Companies();
            company = _Mapper.Map<Companies>(entity.Companies);
            company.CompanyContacts.Add(_Mapper.Map<CompanyContacts>(entity.CompanyContacts));
            company.CompanyOptional = _Mapper.Map<CompanyOptional>(entity.CompanyOptional);
            company.StatusesNotes = new StatusesNotes();
            Address addresses = new Address();
            addresses = _Mapper.Map<Address>(entity.CompanyAddress);
            CompanyAddressCollection Cmp = new CompanyAddressCollection { Address = addresses, Companies = company };
            try
            {
                await _db.Companies.AddAsync(company);
                await _db.Address.AddAsync(addresses);
                await _db.AddAsync(Cmp);                    
                var isSuccess = await Save();
                if (!isSuccess)
                {

                } 
            }
            catch (Exception Ex)
            {
               Console.WriteLine($"{Ex.Message} - {Ex.InnerException}");
                throw;
            }
            var response = _SupportFunction.ConvertToCompanyViewModel(company);
            return response;
        }

        public async Task<bool> Delete(CompanyViewModel entity)
        {
            //Companies company = new Companies();
            var company = _Mapper.Map<Companies>(entity.Companies);
            company.CompanyContacts.Add(_Mapper.Map<CompanyContacts>(entity.CompanyContacts));

            _db.Companies.Remove(company);
            return await Save();
        }

        //public Task<IList<CompanyViewModel>> FindAll()
        //{
        //    throw new NotImplementedException();
        //}        
        public async Task<IList<CompanyViewModel>> FindAll()
        {
            
            var Companies = await _db.Companies
                .Include(a=>a.CompanyContacts).Include(a => a.companycategory).Include(a=>a.CompanySectors)
                .Include(a=>a.addresses)
                .ThenInclude(a => a.Address).ToListAsync();            

            var response = _SupportFunction.ConvertToCompanyViewModelList(Companies);
            return response;
        }

        public async Task<CompanyViewModel> FindById(int id)
        {
            //var Company = await _db.Companies.FindAsync(id);
            var Company = _db.Companies
                .Include(a => a.CompanyContacts)
                .Include(a => a.companycategory).Include(a=>a.CompanySectors)
                .Include(a=>a.CompanyOptional)
                .Include(a=>a.addresses).ThenInclude(a=>a.Address).ThenInclude(a=>a.CountryCode)
                .Include(a=>a.Jobs)
                .Where(a => a.CompanyID == id).FirstOrDefault();
            var response = _SupportFunction.ConvertToCompanyViewModel(Company);
            return response;
        }

        public async Task<CompanyViewModel> FindBystring(string id)
        {
            CompanyViewModel response = new CompanyViewModel();
            var Company = _db.Companies
                .Include(a => a.CompanyContacts)
                .Include(a => a.companycategory)
                .Include(a => a.CompanyOptional)
                .Include(a => a.addresses).ThenInclude(a => a.Address).ThenInclude(a => a.CountryCode)
                .Include(a => a.Jobs)
                .Where(a => a.UserEmailID == id).FirstOrDefault();
            if(Company != null)
            {
                response = _SupportFunction.ConvertToCompanyViewModel(Company);
            }
            else
            {
                CompanyDTO companydata = new CompanyDTO { CompanyID = 0 };
                response.Companies = companydata;
            }
            return response;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Companies.AnyAsync(o => o.CompanyID == id);
        }

        public async Task<bool> Save()
        {
            var chaanges = await _db.SaveChangesAsync();
            return chaanges > 0;
        }

        public async Task<bool> Update(CompanyViewModel entity)
        {

            Companies company = new Companies();
            company = _Mapper.Map<Companies>(entity.Companies);
            company.CompanyContacts.Add(_Mapper.Map<CompanyContacts>(entity.CompanyContacts));
            company.CompanyOptional = _Mapper.Map<CompanyOptional>(entity.CompanyOptional);
            company.StatusesNotes = new StatusesNotes();
            Address addresses = new Address();
            addresses = _Mapper.Map<Address>(entity.CompanyAddress);
            CompanyAddressCollection Cmp = new CompanyAddressCollection { Address = addresses, Companies = company };

            //Companies company = new Companies();
            //company = _Mapper.Map<Companies>(entity.Companies);            
            //company.CompanyContacts.Add(_Mapper.Map<CompanyContacts>(entity.CompanyContacts));

            //Address addresses = new Address();
            //addresses = _Mapper.Map<Address>(entity.CompanyAddress);

            //CompanyAddressCollection Cmp = new CompanyAddressCollection { Address = addresses, Companies = company };

            _db.Companies.Update(company);
            _db.Address.Update(addresses);
            return  await Save();

        }

        //public async Task<bool> CreateIdentityUser(UserDTO user)
        //{
        //    var username = user.UserName;
        //    var password = user.Password;

            
        //}

    }
}
