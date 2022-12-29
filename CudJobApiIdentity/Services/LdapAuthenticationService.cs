using CUDJobAPiIdentity.Contracts;
using CUDJobAPiIdentity.Ldap;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CUDJobAPiIdentity.Services
{
    public class LdapAuthenticationService : IAuthenticationService
    {

        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";

        private readonly LdapConfig _config;
        private readonly LdapConnection _connection;
        public LdapAuthenticationService(IOptions<LdapConfig> config)
        {
            _config = config.Value;
            _connection = new LdapConnection();// { SecureSocketLayer = true };
        }
        public IAppUser Login(string Username, string Password)
        {           

            try
            {
                //_connection.Connect(_config.Url, LdapConnection.DefaultPort);
                //_connection.Bind(_config.Username, _config.Password);

                var conn = Authenticate(_config.Username, _config.Password);
                string[] addressElements = Username.Split('@');
                var searchFilter = String.Format(_config.SearchFilter, addressElements[0]);
                var result = _connection.Search(
                    _config.SearchBase,
                    LdapConnection.ScopeSub,
                    searchFilter,
                    new[] {
                    MemberOfAttribute,
                    DisplayNameAttribute,
                    SAMAccountNameAttribute,
                    MailAttribute
                    },
                    false
                );

                var user = result.Next();
                if (user != null)
                {
                    _connection.Bind(user.Dn, Password);
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.GetAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr == null)
                        {
                            throw new Exception("Your account is missing the account name.");
                        }

                        var displayNameAttr = user.GetAttribute(DisplayNameAttribute);
                        if (displayNameAttr == null)
                        {
                            throw new Exception("Your account is missing the display name.");
                        }

                        var emailAttr = user.GetAttribute(MailAttribute);
                        if (emailAttr == null)
                        {
                            throw new Exception("Your account is missing an email.");
                        }

                        //var memberAttr = user.GetAttribute(MemberOfAttribute);
                        //if (memberAttr == null)
                        //{
                        //    throw new Exception("Your account is missing roles.");
                        //}

                        return new AppUser
                        {
                            DisplayName = displayNameAttr.StringValue,
                            Username = accountNameAttr.StringValue,
                            Email = emailAttr.StringValue
                            //Roles = memberAttr.StringValueArray
                            //    .Select(x => GetGroup(x))
                            //    .Where(x => x != null)
                            //    .Distinct()
                            //    .ToArray()
                        };
                    }
                }
            }            
            catch(Exception Ex)
            {
               // return $"{Ex.Message} + {Ex.InnerException}";
            }
            finally
            {
                _connection.Disconnect();
            }

            return null;

            //try {
            //    using (DirectoryEntry entry = new DirectoryEntry())
            //}
        }
        public bool Authenticate(string distinguishedName, string password)
        {
            try
                {
                _connection.Connect(_config.Url, LdapConnection.DefaultPort);
                _connection.Bind(distinguishedName, password);

                    return true;
                }
                catch (Exception Ex)
                {
                     Console.WriteLine($"{Ex.Message} + {Ex.InnerException}");
                    return false;
                }
            
        }
        private string GetGroup(string value)
        {
            Match match = Regex.Match(value, "^CN=([^,]*)");
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }
    }
}
