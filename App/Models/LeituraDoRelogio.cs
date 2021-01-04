using System;

namespace App.Models
{
    public class LeituraDoRelogio
    {
        protected LeituraDoRelogio() { }
        public LeituraDoRelogio(int kwh, DateTime? registro = null)
        {
            Kwh = kwh;
            RegistrarData(registro);
        }
        public int Id { get; private set; }
        public int Kwh { get; set; }
        public DateTime? Registro { get; private set; }
        public int Consumo { get; private set; }
        public void CalcularConsumo(int ultimaLeitura, int novaLeitura)
        {
            this.Consumo = novaLeitura - ultimaLeitura;
        }

        public void ZerarConsumo()
        {
            this.Consumo = 0;
        }
        public void EditarRegistro(DateTime registro)
        {
            this.Registro = registro;
        }
        private void RegistrarData(DateTime? registro)
        {
            this.Registro = registro is null ? DateTime.Now : registro;
        }
    }
}