using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum ProfileEnum
    {
        USER_PROFILE_PATH = 0,
        LOGON_SCRIPT = 1,
        HOME_FOLDER_LOCAL_PATH = 2,
        HOME_FOLDER_CONNECT_DRIVE = 3,
        HOME_FOLDER_CONNECT_TO = 4
    }

    public static class ProfileEnumExtensions
    {
        public static string ToAttribute(this ProfileEnum value)
        {
            switch (value)
            {
                case ProfileEnum.USER_PROFILE_PATH:
                    return "profilePath";
                case ProfileEnum.LOGON_SCRIPT:
                    return "scriptPath";
                case ProfileEnum.HOME_FOLDER_LOCAL_PATH:
                    return "homeDirectory";
                case ProfileEnum.HOME_FOLDER_CONNECT_DRIVE:
                    return "homeDrive";
                case ProfileEnum.HOME_FOLDER_CONNECT_TO:
                    return "homeDirectory";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");
            }
        }
    }
}
