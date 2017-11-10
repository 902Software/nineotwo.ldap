using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum AddressEnum
    {
        STREET = 0,
        PO_BOX = 1,
        CITY = 2,
        STATE_PROVINCE = 3,
        ZIP_POSTAL_CODE = 4,
        COUNTRY_REGION = 5
    }
    public static class AddressEnumExtensions
    {
        public static string ToAttribute(this AddressEnum value)
        {
            switch (value)
            {
                case AddressEnum.STREET:
                    return "streetAddress";
                case AddressEnum.PO_BOX:
                    return "postOfficeBox";
                case AddressEnum.CITY:
                    return "l";
                case AddressEnum.STATE_PROVINCE:
                    return "st";
                case AddressEnum.ZIP_POSTAL_CODE:
                    return "postalCode";
                case AddressEnum.COUNTRY_REGION:
                    return "co";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");

            }
        }
    }
}
