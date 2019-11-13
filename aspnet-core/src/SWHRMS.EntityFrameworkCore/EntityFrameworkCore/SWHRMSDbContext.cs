using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using SWHRMS.Authorization.Roles;
using SWHRMS.Authorization.Users;
using SWHRMS.Branches;
using SWHRMS.MultiTenancy;
using SWHRMS.Absences;
using SWHRMS.Attendances;
using SWHRMS.Positions;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.QRCodes;
using SWHRMS.Levels;
using SWHRMS.Skills;

namespace SWHRMS.EntityFrameworkCore
{
    public class SWHRMSDbContext : AbpZeroDbContext<Tenant, Role, User, SWHRMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UserMetaInfo> UserMetaInfos { get; set; }
        public DbSet<UserMetaInfoDetail> UserMetaInfoDetails { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }

        public SWHRMSDbContext(DbContextOptions<SWHRMSDbContext> options)
            : base(options)
        {
        }
    }
}
