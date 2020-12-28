using App;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace Testes
{
    public class BancoEmMemoria
    {
        private readonly DbContextOptions db;

        public BancoEmMemoria(DbContextOptions<Contexto> db)
        {
            this.db = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase("Contexto")
            .Options;
        }

        public LeituraDoRelogio UltimaLeitura()
        {
            var kwh200 = new LeituraDoRelogio(200);
            var kwh500 = new LeituraDoRelogio(500);

            db.AddRange(kwh200, kwh500);
            db.SaveChanges();

            var ultimaLeitura = db.LeiturasDoRelogio.LastOrDefault();
            return ultimaLeitura;
        }
    }
}