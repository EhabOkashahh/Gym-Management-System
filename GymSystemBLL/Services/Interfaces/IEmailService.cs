using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string username, string Email, string password);
    }
}