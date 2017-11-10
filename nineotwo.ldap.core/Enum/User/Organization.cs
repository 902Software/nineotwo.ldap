using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap.core.Enum.User
{
    public enum OrganizationEnum
    {
        TITLE = 0,
        DEPARTMENT = 1,
        COMPANY = 2,
        MANAGER = 3
    }

    public static class OrganizationEnumExtensions
    {
        public static string ToAttribute(this OrganizationEnum value)
        {
            switch (value)
            {
                case OrganizationEnum.TITLE:
                    return "title";
                case OrganizationEnum.DEPARTMENT:
                    return "department";
                case OrganizationEnum.COMPANY:
                    return "company";
                case OrganizationEnum.MANAGER:
                    return "manager";
                default:
                    throw new ArgumentException($"Invalid Enum:{value.ToString()}");
            }

        }
    }
}
