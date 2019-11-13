using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SWHRMS.EntityFrameworkCore
{
    public static class SWHRMSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SWHRMSDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SWHRMSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
