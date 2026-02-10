using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
namespace JobMarket.Ef.AspUsers
{
    public class UserAuthenticator : JobMarket.Data.AspUsers.IUserAuthenticator
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
