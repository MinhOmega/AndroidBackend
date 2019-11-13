using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SWHRMS.Configuration;
using SWHRMS.Web;

namespace SWHRMS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SWHRMSDbContextFactory : IDesignTimeDbContextFactory<SWHRMSDbContext>
    {
        public SWHRMSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SWHRMSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SWHRMSDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SWHRMSConsts.ConnectionStringName));

            return new SWHRMSDbContext(builder.Options);
        }
    }
}
