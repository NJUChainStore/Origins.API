using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Origins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Models
{
    public class ApplicationContext : IdentityDbContext<UserModel, UserRole, string>
    {

    }
}
