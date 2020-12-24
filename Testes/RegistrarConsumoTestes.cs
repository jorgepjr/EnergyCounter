using App;
using Xunit;
using App.Models;
using App.Service;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Testes
{
    public class RegistrarConsumoTestes
    {
        [Fact]
        public void DeveRegistrarConsumoAoRegistrarNovaLeitura()
        {
            //Given
            var novaLeitura = new LeituraDoRelogio(360);

            var db = BancoEmMemoria();
            var medidor = new Medidor(db);

            var leituras = LeiturasDoRelogio();
            db.AddRange(leituras);
            db.SaveChanges();

            //When
            medidor.RegistrarConsumo(novaLeitura.Kwh);

            //Then
            Assert.Equal(260, leituras[0].Consumo);
        }

        [Fact]
        public void DeveRegistrarUmLeituraPorDia()
        {
            //Given
            var ultimaLeitura = UltimaLeitura();

            //When
            var novaLeitura = new LeituraDoRelogio(360);

            //Then
            Assert.True(ultimaLeitura.Registro < novaLeitura.Registro);
        }
        public static List<LeituraDoRelogio> LeiturasDoRelogio()
        {
            var kwh100 = new LeituraDoRelogio(100);

            var leituras = new List<LeituraDoRelogio> { kwh100 };
            return leituras;
        }
        public static LeituraDoRelogio UltimaLeitura()
        {
            var kwh200 = new LeituraDoRelogio(200);
            var kwh500 = new LeituraDoRelogio(500);

            var db = BancoEmMemoria();
            db.AddRange(kwh200, kwh500);
            db.SaveChanges();

            var ultimaLeitura = db.LeiturasDoRelogio.LastOrDefault();
            return ultimaLeitura;
        }

        public static Contexto BancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase("Contexto")
            .Options;

            var db = new Contexto(options);

            return db;
        }
    }
}