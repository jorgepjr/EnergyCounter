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

        public bool UltimoValorEhMaior(int kwh)
        {
            var leituras = db.LeiturasDoRelogio.ToList();
            if (leituras.Any())
            {
                return true;
            }

            foreach (var item in leituras)
            {
                if (item.Kwh > kwh)
                {
                    return true;
                }
            }
            return false;
        }
        public bool LeituraDoDiaRealizada()
        {
            var hoje = DateTime.Now.Date;
            var ultimaLeitura = db.LeiturasDoRelogio.OrderBy(x => x.Registro).LastOrDefault();
            if (ultimaLeitura != null)
                return ultimaLeitura.Registro.Date == hoje;
            return false;
        }
        public void ZerarUltimoConsumoDoDia()
        {
            var leitura = db.LeiturasDoRelogio.OrderBy(x => x.Registro).LastOrDefault();
            if (leitura != null)
                leitura.ZerarConsumoDoDia();
            db.SaveChanges();
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