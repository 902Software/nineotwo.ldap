using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum GeneralEnum
    {
        FIRST_NAME = 0,
        INITIALS = 1,
        LAST_NAME = 2,
        DISPLAY_NAME = 3,
        DESCRIPTION = 4,
        OFFICE = 5,
        TELEPHONE_NUMBER = 6,
        OTHER_TELEPHONE_NUMBERS = 7,
        EMAIL = 8,
        WEB_PAGE = 9,
        OTHER_WEB_PAGES = 10

    }

    public static class GeneralEnumExtensions
    {
        public static string ToAttribute(this GeneralEnum value)
        {
            switch (value)
            {
                case GeneralEnum.FIRST_NAME:
                    return "givenName";                    
                case GeneralEnum.INITIALS:
                    return "initials";
                case GeneralEnum.LAST_NAME:
                    return "sn";
                case GeneralEnum.DISPLAY_NAME:
                    return "displayName";
                case GeneralEnum.DESCRIPTION:
                    return "description";
                case GeneralEnum.OFFICE:
                    return "physicalDeliveryOfficeName";
                case GeneralEnum.TELEPHONE_NUMBER:
                    return "telephoneNumber";
                case GeneralEnum.OTHER_TELEPHONE_NUMBERS:
                    return "otherTelephone";
                case GeneralEnum.EMAIL:
                    return "mail";
                case GeneralEnum.WEB_PAGE:
                    return "wWWHomePage";
                case GeneralEnum.OTHER_WEB_PAGES:
                    return "url";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");

            }
        }
    }
}
