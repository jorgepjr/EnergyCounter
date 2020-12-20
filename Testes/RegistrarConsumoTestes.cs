using App;
using Xunit;
using App.Models;
using App.Service;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Testes
{
    public class RegistrarConsumoTestes
    {
        [Fact]
        public void DeveRegistrarConsumoAoRegistrarNovaLeitura()
        {
            //Given
            var novaLeitura = new LeituraDoRelogio(360);

            var options = new DbContextOptionsBuilder<Contexto>()
            .UseInMemoryDatabase("Contexto")
            .Options;

            var db = new Contexto(options);
            var medidor = new Medidor(db);

            var leituras = LeiturasDoRelogio();
            db.AddRange(leituras);
            db.SaveChanges();

            //When
            medidor.RegistrarConsumo(novaLeitura.Kwh);

            //Then
            Assert.Equal(260, leituras[0].Consumo);
        }
        public static List<LeituraDoRelogio> LeiturasDoRelogio()
        {
            var kwh100 = new LeituraDoRelogio(100);

            var leituras = new List<LeituraDoRelogio> { kwh100 };
            return leituras;
        }
    }
}