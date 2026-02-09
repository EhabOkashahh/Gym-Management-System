using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GymSystemDAL.Data.Contexts
{
    public class AppContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // get the enviroment var
            var GetConnectionString = Environment.GetEnvironmentVariable("GYM_CONNECTION")?? throw new Exception("Invalid Connection");
           
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}