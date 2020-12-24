using System;
using App.Service;

namespace App.Models
{
    public class LeituraDoRelogio
    {
        private readonly Medidor medidor;
        protected LeituraDoRelogio() { }
        public LeituraDoRelogio(int kwh)
        {
            Kwh = kwh;
            Registro = DateTime.Now;
        }

        public int Id { get; private set; }
        public int Kwh { get; set; }
        public DateTime Registro { get; private set; }
        public int Consumo { get; private set; }

        public void CalcularConsumo(int ultimaLeitura, int novaLeitura)
        {
            this.Consumo = novaLeitura - ultimaLeitura;
        }
    }
}