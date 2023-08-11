using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BechelorContext>
    {
        public BechelorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BechelorContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Bechelordb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BechelorContext(optionsBuilder.Options);
        }
    }
}
