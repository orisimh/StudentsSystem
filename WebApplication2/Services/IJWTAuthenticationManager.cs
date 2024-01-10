using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string password);

    }
}
