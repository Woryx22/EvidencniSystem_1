using EvidencniSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EvidencniSystem.Data
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
            Database.EnsureCreated();// Zajistí vytvoření tabulky
        }

        public DbSet<Odberatel> Odberatele { get; set; }

        public DbSet<VydaneFaktury> VydaneFaktury_ { get; set; }

        public DbSet<PrijateFaktury> PrijateFaktury_ { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MY.db");
            optionsBuilder.UseSqlite($"Data Source = {path}");
        }
    }
}
