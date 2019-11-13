using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Localization;
using SWHRMS.Levels;

namespace SWHRMS.EntityFrameworkCore.Seed.Host
{
    public class DefaultLevelsCreator
    {
        public static List<Level> InitialLevels => GetInitialLevels();

        private readonly SWHRMSDbContext _context;

        private static List<Level> GetInitialLevels()
        {
            return new List<Level>
            {
                new Level("Fresher","New to the work",10),
                new Level("Junior","Little experience",30),
                new Level("Senior","Experienced",50)
            };
        }

        public DefaultLevelsCreator(SWHRMSDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLevels();
        }

        private void CreateLevels()
        {
            foreach (var Level in InitialLevels)
            {
                AddLevelIfNotExists(Level);
            }
        }

        private void AddLevelIfNotExists(Level Level)
        {
            if (_context.Levels.IgnoreQueryFilters().Any(l => l.Name == Level.Name))
            {
                return;
            }

            _context.Levels.Add(Level);
            _context.SaveChanges();
        }
    }
}
