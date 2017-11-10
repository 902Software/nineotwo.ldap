using nineotwo.ldap.core;
using nineotwo.ldap.core.Enum.User;
using Novell.Directory.Ldap;

namespace nineotwo.ldap
{
    internal class LDAPUser : IUser
    {
        public LDAPUser(LdapEntry entry)
        {

            ID = entry.getAttribute(AccountEnum.PRE_WINDOWS_2000_LOGON_NAME);
            Email = entry.getAttribute(GeneralEnum.EMAIL);
            Name = entry.getAttribute(GeneralEnum.FIRST_NAME);
            LastName = entry.getAttribute(GeneralEnum.LAST_NAME);
            DisplayName = entry.getAttribute(GeneralEnum.DISPLAY_NAME);
            LogonName = entry.getAttribute(AccountEnum.USER_LOGON_NAME);
            PhoneNumber = entry.getAttribute(GeneralEnum.TELEPHONE_NUMBER);
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string LogonName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ID { get; set; }
        public string[] Groups { get; set; }
    }


}
