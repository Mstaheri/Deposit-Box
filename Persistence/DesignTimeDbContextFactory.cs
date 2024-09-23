using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DbContextEF>
    {
        public DbContextEF CreateDbContext(string[] args)
        {
            string DbConnectionString = "Data Source=.;Initial Catalog=DB_Sandogh;Integrated Security=True;TrustServerCertificate=True";
            var options = new DbContextOptionsBuilder<DbContextEF>()
                .UseSqlServer(DbConnectionString)
                .Options;
            return new DbContextEF(options);
        }
    }


}
