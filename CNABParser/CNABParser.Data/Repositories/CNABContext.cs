using CNABParser.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CNABParser.Data.Repositories
{
    public class CNABContext : DbContext
    {
        public CNABContext(DbContextOptions<CNABContext> options) : base(options) { }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<TipoTransacao> Tipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transacao>().ToTable("Transacao");
            modelBuilder.Entity<TipoTransacao>().ToTable("TipoTransacao");
        }
    }
}
