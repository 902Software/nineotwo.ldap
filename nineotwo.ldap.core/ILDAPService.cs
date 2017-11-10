using System.Collections.Generic;

namespace nineotwo.ldap.core
{
    public interface ILDAPService
    {
        bool IsUserValid(ILogin login);
        IUser GetUser(string username);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.AccountEnum[] attribute);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.AddressEnum[] attribute);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.GeneralEnum[] attribute);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.OrganizationEnum[] attribute);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.ProfileEnum[] attribute);
        Dictionary<string,string> GetUserAttributes(IUser user, params Enum.User.TelephonesEnum[] attribute);
    }

}
