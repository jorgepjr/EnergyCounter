using System;

namespace App.Models
{
    public class LeituraDoRelogio
    {
        protected LeituraDoRelogio() { }
        public LeituraDoRelogio(int kwh) => Kwh = kwh;

        public int Id { get; private set; }
        public int Kwh { get; set; }
        public DateTime Registro { get; private set; } = DateTime.Now;
        public int Consumo { get; private set; }

        public void CalcularConsumo(int ultimaLeitura, int novaLeitura)
        {
            this.Consumo = novaLeitura - ultimaLeitura;
        }
    }
}