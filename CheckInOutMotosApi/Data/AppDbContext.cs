using Microsoft.EntityFrameworkCore;
using CheckInOutMotosApi.Models;

namespace CheckInOutMotosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
    }
}