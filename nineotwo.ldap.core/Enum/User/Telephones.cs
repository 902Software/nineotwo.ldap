using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum TelephonesEnum:int
    {
        HOME = 0,
        OTHER_HOME_PHONE_NUMBERS = 1,
        PAGER = 2,
        OTHER_PAGER_NUMBERS = 3,
        MOBILE = 4,
        OTHER_MOBILE_NUMBERS = 5,
        FAX = 6,
        OTHER_FAX_NUMBERS = 7,
        IP_PHONE = 8,
        OTHER_IP_PHONE_NUMBERS = 9,
        NOTES = 10
    }

    public static class TelephonesEnumExtensions
    {
        public static string ToAttribute(this TelephonesEnum value)
        {
            switch (value)
            {
                case TelephonesEnum.HOME:
                    return "homePhone";
                case TelephonesEnum.OTHER_HOME_PHONE_NUMBERS:
                    return "otherHomePhone";
                case TelephonesEnum.PAGER:
                    return "pager";
                case TelephonesEnum.OTHER_PAGER_NUMBERS:
                    return "otherPager";
                case TelephonesEnum.MOBILE:
                    return "mobile";
                case TelephonesEnum.OTHER_MOBILE_NUMBERS:
                    return "otherMobile";
                case TelephonesEnum.FAX:
                    return "facsimileTelephoneNumber";
                case TelephonesEnum.OTHER_FAX_NUMBERS:
                    return "otherFacsimileTelephoneNumber";
                case TelephonesEnum.IP_PHONE:
                    return "ipPhone";
                case TelephonesEnum.OTHER_IP_PHONE_NUMBERS:
                    return "otherIpPhone";
                case TelephonesEnum.NOTES:
                    return "info";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");
            }

        }
    }
}
