using nineotwo.ldap.core.Enum.User;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap
{
    internal static class LdapEntryExtension
    {
        internal static string getAttribute(this LdapEntry entry, AccountEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }
        internal static string getAttribute(this LdapEntry entry, AddressEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }
        internal static string getAttribute(this LdapEntry entry, GeneralEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }
        internal static string getAttribute(this LdapEntry entry, OrganizationEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }
        internal static string getAttribute(this LdapEntry entry, ProfileEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }

        internal static string getAttribute(this LdapEntry entry, TelephonesEnum value)
        {
            var attr = value.ToAttribute();
            return entry.getAttribute(attr)?.ToString();
        }
    }
}
