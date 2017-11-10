using nineotwo.ldap.core;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Text;
using nineotwo.ldap.core.Enum.User;
using System.Linq;

namespace nineotwo.ldap
{
    public class Service : ILDAPService
    {
        private LDAPConfig _config = new LDAPConfig();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host">The </param>
        /// <param name="loginId"></param>
        /// <param name="loginPassword"></param>
        /// <param name="container"></param>
        public Service(string host, string loginId, string loginPassword, string container):this(new LDAPConfig()
        {
            Host = host,
            UserName= loginId,
            Password = loginPassword,
            Container = container,
        })
        {
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="loginId"></param>
        /// <param name="loginPassword"></param>
        /// <param name="ldapPort"></param>
        public Service(LDAPConfig config)
        {
            _config = config;
        }


        /// <summary>
        /// Retrieve 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IUser GetUser(string username)
        {
            LDAPUser retVal = null;

            if (string.IsNullOrEmpty(username))
                return null;

            var userAttribute = AccountEnum.PRE_WINDOWS_2000_LOGON_NAME.ToAttribute();
            //Find User Based On Property Value
            if (Exists(userAttribute, username))
            {
                GeneralEnum[] generalInfo = { GeneralEnum.TELEPHONE_NUMBER, GeneralEnum.FIRST_NAME, GeneralEnum.LAST_NAME, GeneralEnum.DISPLAY_NAME, GeneralEnum.EMAIL };
                AccountEnum[] accountInfo = { AccountEnum.USER_LOGON_NAME, AccountEnum.PRE_WINDOWS_2000_LOGON_NAME};
                var attributes = generalInfo.Select(g => g.ToAttribute()).Concat(accountInfo.Select(a => a.ToAttribute()));
                LdapEntry user = ForUser(userAttribute, username, attributes.ToArray());
                retVal = new LDAPUser(user);
            }

            return retVal;
        }

        /// <summary>
        /// Validate user credentials agains AD
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsUserValid(ILogin login)
        {
            if (login == null)
                return false;

            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                return false;

            using (LdapConnection conn = new LdapConnection())
            {
                try
                {
                    conn.Connect(_config.Host, _config.LDAPPort);
                    conn.Bind(_config.LDAPVersion, $"{login.Username}", login.Password);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

       

        /// <summary>
        /// Determines the existence of an object
        /// </summary>
        /// <param name="key">The property</param>
        /// <param name="value">The property value</param>
        /// <returns></returns>
        private bool Exists(string key, string value)
        {
            LdapEntry obj = ForLdapEntry(key, value);
            return obj != null;
        }

        /// <summary>
        /// Finds the LDAP Entry for the specific user
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private LdapEntry ForUser(string key, string value)
        {
            return ForLdapEntry(null, null, String.Format("(&(&(objectClass=user)(!(objectClass=computer)))({0}={1}))", key, value));
        }

        /// <summary>
        /// Return the LDAP Entry for the specific user
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="PropertiesToLoad"></param>
        /// <returns></returns>
        private LdapEntry ForUser(string key, string value, string[] PropertiesToLoad)
        {
            return ForLdapEntry(null, null, String.Format("(&(&(objectClass=user)(!(objectClass=computer)))({0}={1}))", key, value), PropertiesToLoad);
        }

        /// <summary>
        /// Finds Users
        /// </summary>
        /// <returns>List</returns>
        private List<LdapEntry> ForUsers()
        {
            return ForLdapEntries(null, null, "(&(objectClass=user)(!(objectClass=computer)))");
        }

        /// <summary>
        /// Find Users and load the specific attributes
        /// </summary>
        /// <param name="PropertiesToLoad"></param>
        /// <returns></returns>
        private List<LdapEntry> ForUsers(string[] PropertiesToLoad)
        {
            return ForLdapEntries(null, null, "(&(objectClass=user)(!(objectClass=computer)))", PropertiesToLoad);
        }


        /// <summary>
        /// Find Groups with specific property
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private LdapEntry ForGroup(string key, string value)
        {
            return ForLdapEntry(null, null, $"(&(objectClass=group)({key}={value}))");
        }

        /// <summary>
        /// Return users(?)
        /// </summary>
        /// <returns></returns>
        private List<LdapEntry> ForGroups()
        {
            return ForLdapEntries(null, null, "objectClass=user");
        }

        /// <summary>
        /// Finds Users
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<LdapEntry> ForUsers(string key, string value)
        {
            return ForLdapEntries(null, null, "(&(&(objectClass=user)(!(objectClass=computer)))(" + key + "=" + value + "))");
        }

        /// <summary>
        /// Finds ldap entry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private LdapEntry ForLdapEntry(string key, string value)
        {
            return ForLdapEntry(key, value, string.Format("{0}={1}", key, value));
        }

        /// <summary>
        /// Finds ldap entry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private LdapEntry ForLdapEntry(string key, string value, string filter)
        {
            return ForLdapEntry(key, value, filter, null);
        }

        /// <summary>
        /// Finds ldap entry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filter">The ADO query filter</param>
        /// <param name="attributes">Properties to load</param>
        /// <returns></returns>
        private LdapEntry ForLdapEntry(string key, string value, string filter, string[] attributes)
        {

            LdapEntry searchResult = null;
            using (LdapConnection conn = new LdapConnection())
            {
                conn.Connect(_config.Host, _config.LDAPPort);
                conn.Bind(_config.LDAPVersion, _config.UserName, _config.Password);

                //Search
                LdapSearchResults results = conn.Search(_config.Container, //search base
                                                        LdapConnection.SCOPE_SUB, //scope 
                                                        filter, //filter
                                                        attributes, //attributes 
                                                        false); //types only 

                while (results.hasMore())
                {
                    try
                    {
                        searchResult = results.next();
                        break;
                    }
                    catch (LdapException e)
                    {
                        throw e;
                    }
                }
            }
            return searchResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<LdapEntry> ForLdapEntries(string key, string value)
        {
            return ForLdapEntries(key, value, String.Format("{0}={1}"), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private List<LdapEntry> ForLdapEntries(string key, string value, string filter)
        {
            return ForLdapEntries(key, value, filter, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filter"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private List<LdapEntry> ForLdapEntries(string key, string value, string filter, string[] attributes)
        {

            List<LdapEntry> searchResults = new List<LdapEntry>();

            using (LdapConnection conn = new LdapConnection())
            {
                conn.Connect(_config.Host, _config.LDAPPort);
                conn.Bind(_config.LDAPVersion, _config.UserName, _config.Password);

                LdapSearchResults results = conn.Search(_config.Container, //search base
                                                        LdapConnection.SCOPE_SUB, //scope 
                                                        filter, //filter
                                                        attributes, //attributes 
                                                        false); //types only 

                while (results.hasMore())
                {
                    LdapEntry nextEntry = null;
                    try
                    {
                        nextEntry = results.next();
                        if (nextEntry != null)
                            searchResults.Add(nextEntry);
                    }
                    catch (LdapException)
                    {

                        continue;
                    }
                }
            }
            return searchResults;
        }

        /// <summary>
        /// Validate the passed user before retrieve any attributes
        /// </summary>
        /// <param name="user"></param>
        private Dictionary<string,string> GetUserAttributes(IUser user, IEnumerable<string> attributes)
        {
            if (attributes == null)
                return null;
            if (user.ID == null)
                throw new ArgumentException("Invalid User");
            if (user == null || user.ID == null)
                throw new ArgumentException("Invalid User");

            string[] attribs = attributes.ToArray();
            LdapEntry ldapUser = ForUser(AccountEnum.PRE_WINDOWS_2000_LOGON_NAME.ToAttribute(), user.ID, attribs);
            var sets = ldapUser.getAttributeSet();

            Dictionary<string, string> RetVal = new Dictionary<string, string>();

            foreach (var item in attributes)
            {
                RetVal.Add(item, sets.getAttribute(item)?.StringValue);
            }
            return RetVal;

        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params AccountEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params AddressEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params GeneralEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params OrganizationEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params ProfileEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }

        public Dictionary<string, string> GetUserAttributes(IUser user, params TelephonesEnum[] attribute)
        {
            return GetUserAttributes(user, attribute?.Select(a => a.ToAttribute()));
        }


       

    }
}
