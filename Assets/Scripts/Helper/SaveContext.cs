using System.Data.Entity;
using System.Data.SQLite;
using Ig.Helpers.Db;

namespace Ig.Helpers
{
    public class SaveContext : DbContext
    {
        public SaveContext() : base(@"DefaultConnection")
        {
        }

        public DbSet<PlayerSave> PlayerSave { get; set; } 
    }
}