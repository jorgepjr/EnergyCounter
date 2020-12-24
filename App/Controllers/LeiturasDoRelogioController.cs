using System;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class LeiturasDoRelogioController : Controller
    {
        private readonly Contexto db;
        private readonly Medidor medidor;
        public LeiturasDoRelogioController(Contexto db, Medidor medidor)
        {
            this.db = db;
            this.medidor = medidor;
        }
        public async Task<IActionResult> Index()
        {
            var leituras = await db.LeiturasDoRelogio
            .Where(x => x.Registro.Month == DateTime.Now.Month).OrderBy(x => x.Kwh).ToListAsync();
            return View(leituras);
        }

        [HttpPost]
        public IActionResult Registrar(int kwh)
        {
            var ultimoValor = db.LeiturasDoRelogio.OrderBy(x => x.Kwh).LastOrDefault();
            if (kwh <= 0)
            {
                TempData["Mensagem"] = "*valor inválido ";
                return RedirectToAction(nameof(Index));
            }
            if (medidor.LeituraDoDiaRealizada())
            {
                TempData["Mensagem"] = $"O registro de hoje já foi realizado!";
                return RedirectToAction(nameof(Index));
            }

            if (medidor.UltimoValorEhMenor(kwh))
            {
                TempData["Mensagem"] = $"Digite um valor maior que {ultimoValor.Kwh} Kw/h";
                return RedirectToAction(nameof(Index));
            }
            var leitura = new LeituraDoRelogio(kwh);
            medidor.RegistrarConsumo(kwh);
            db.Add(leitura);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}