namespace SWHRMS.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly SWHRMSDbContext _context;

        public InitialHostDbBuilder(SWHRMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultLevelsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
