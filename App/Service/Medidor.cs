using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Service
{
    public class Medidor
    {
        private readonly Contexto db;

        public Medidor(Contexto db)
        {
            this.db = db;
        }
        //TODO: Testar
        public async Task RegistrarConsumo(int kwh)
        {
            int novaLeitura = kwh;

            var ultimaLeitura = await db.LeiturasDoRelogio.OrderBy(x => x.Kwh).LastOrDefaultAsync();

            if (ultimaLeitura != null)
            {
                if (ultimaLeitura.Kwh < novaLeitura || ultimaLeitura != null)
                {
                     ultimaLeitura.CalcularConsumo(ultimaLeitura.Kwh, novaLeitura);
                }
            }
        }
        //TODO: Testar
        public bool ValorEhMenorDoQueOsQueJaForamRegistrados(int kwh)
        {
            var leituras = db.LeiturasDoRelogio.ToList();
            if (leituras.Any())

                foreach (var item in leituras)
                {
                    if (kwh < item.Kwh)
                    {
                        return true;
                    }
                }
            return false;
        }
        //TODO: Testar
        public bool LeituraDoDiaJaRealizada()
        {
            var hoje = DateTime.Now.Date;
            var leitura = db.LeiturasDoRelogio.FirstOrDefault(x => x.Registro.Value.Date == hoje);
            return leitura != null;
        }
        //TODO: Testar
        public void ZerarConsumoDoDiaAnterior()
        {
            var hoje = DateTime.Now;
            var leitura = db.LeiturasDoRelogio.SingleOrDefault(x => x.Registro.Value.Date.Day < hoje.Date.Day);
            if (leitura != null)
                leitura.ZerarConsumo();
        }
        //TODO: Testar
        public int ConsumoMensal()
        {
            int total = 0;
            var leituras = db.LeiturasDoRelogio.Where(x => x.Registro.Value.Date == DateTime.Now.Date).ToList();

            foreach (var leitura in leituras)
            {
                total += leitura.Consumo;
            }
            return total;
        }
    }
}