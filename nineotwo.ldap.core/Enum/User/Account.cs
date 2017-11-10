using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum AccountEnum 
    {
        USER_LOGON_NAME = 0,
        PRE_WINDOWS_2000_LOGON_NAME = 2,
        ACCOUNT_DISABLED = 3,
        LOGON_HOURS = 4,
        LOGON_ON_TO = 5,
        USER_MUST_CHANGE_PASSWORD_AT_NEXT_LOGON = 6,
        USER_CANNOT_CHANGE_PASSWORD = 7,
        PASSWORD_NEVER_EXPIRES = 8,
        STORE_PASSWORD_USING_REVERSIBLE_ENCRYPTION = 9,
        ACCOUNT_EXPIRES_END_OF_DATE = 10
    }

    public static class AccountEnumExtensions
    {
        public static string ToAttribute(this AccountEnum value)
        {
            switch (value)
            {
                case AccountEnum.USER_LOGON_NAME:
                    return "userPrincipalName";
                case AccountEnum.PRE_WINDOWS_2000_LOGON_NAME:
                    return "sAMAccountName";
                case AccountEnum.ACCOUNT_DISABLED:
                    return "AccountDisabled";
                case AccountEnum.LOGON_HOURS:
                    return "logonHours";
                case AccountEnum.LOGON_ON_TO:
                    return "userWorkstations";
                case AccountEnum.USER_MUST_CHANGE_PASSWORD_AT_NEXT_LOGON:
                    return "pwdLastSet";
                case AccountEnum.USER_CANNOT_CHANGE_PASSWORD:
                    return "userAccountControl";
                case AccountEnum.PASSWORD_NEVER_EXPIRES:
                    return "userAccountControl";
                case AccountEnum.STORE_PASSWORD_USING_REVERSIBLE_ENCRYPTION:
                    return "userAccountControl";
                case AccountEnum.ACCOUNT_EXPIRES_END_OF_DATE:
                    return "AccountExpirationdate";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");

            }
        }
    }
}
