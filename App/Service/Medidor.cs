using System;
using System.Linq;

namespace App.Service
{
    public class Medidor
    {
        private readonly Contexto db;

        public Medidor(Contexto db)
        {
            this.db = db;
        }
        public void RegistrarConsumo(int kwh)
        {
            int novaLeitura = kwh;

            var ultimaLeitura = db.LeiturasDoRelogio.OrderBy(x => x.Kwh).LastOrDefault();

            if (ultimaLeitura != null)
            {
                if (ultimaLeitura.Kwh < novaLeitura || ultimaLeitura != null)
                {
                    ultimaLeitura.CalcularConsumo(ultimaLeitura.Kwh, novaLeitura);
                }
            }
        }

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

        public bool LeituraDoDiaJaRealizada()
        {
            var hoje = DateTime.Now.Date;
            var leitura = db.LeiturasDoRelogio.FirstOrDefault(x => x.Registro.Date == hoje);
            return leitura != null;
        }
        public void ZerarConsumoDoDiaAnterior()
        {
            var diaAnterior = DateTime.Now.Date.AddDays(-1);
            var leitura = db.LeiturasDoRelogio.SingleOrDefault(x => x.Registro.Date == diaAnterior);
            if (leitura != null)
                leitura.ZerarConsumo();
        }

        public int ConsumoMensal()
        {
            int total = 0;
            var leituras = db.LeiturasDoRelogio.ToList();

            foreach (var leitura in leituras)
            {
                total += leitura.Consumo;
            }
            return total;
        }
    }
}