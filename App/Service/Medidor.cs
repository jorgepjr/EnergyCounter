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
                if (ultimaLeitura.Kwh < novaLeitura)
                {
                    ultimaLeitura.CalcularConsumo(ultimaLeitura.Kwh, novaLeitura);
                }
            }
        }
    }
}