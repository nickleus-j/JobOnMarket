using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data.AspUsers
{
    public interface IUserAuthenticator
    {
        string HashPassword(string password);
    }
}
