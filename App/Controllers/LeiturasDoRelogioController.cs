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
            var leituras = await db.LeiturasDoRelogio.OrderBy(x => x.Kwh).ToListAsync();
            return View(leituras);
        }

        [HttpPost]
        public IActionResult Registrar(int kwh)
        {
            if (kwh == 0)
            {
                return BadRequest();
            }
            var leituras = db.LeiturasDoRelogio.ToList();

            foreach (var item in leituras)
            {
                if (item.Kwh > kwh)
                {
                    ViewBag.Message = "Nao e possivel";
                    return RedirectToAction(nameof(Index));
                }
            }
            var leitura = new LeituraDoRelogio(kwh);
            medidor.RegistrarConsumo(kwh);
            db.Add(leitura);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}