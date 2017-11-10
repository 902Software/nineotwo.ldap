using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;

namespace nineotwo.ldap
{
    public class LDAPConfig
    {
        public LDAPConfig()
        {
            LDAPPort = LdapConnection.DEFAULT_PORT;
            LDAPVersion= LdapConnection.Ldap_V3;
        }

        public string  Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Container { get; set; }
        public int LDAPPort { get; set; }
        public int LDAPVersion { get; set; }
    }
}
