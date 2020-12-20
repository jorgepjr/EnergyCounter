using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App {
    public class Contexto : DbContext {
        public Contexto (DbContextOptions<Contexto> options) : base (options) { }
        public DbSet<LeituraDoRelogio> LeiturasDoRelogio { get; set; }
    }
}