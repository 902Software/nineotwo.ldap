using System;

namespace nineotwo.ldap.core
{
    public interface IUser
    {
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string ID { get; set; }
        string[] Groups { get; set; }
    }

}
