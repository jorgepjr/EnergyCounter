using App;
using App.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Service;

namespace EnergyCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto db;
        private readonly Medidor medidor;

        public HomeController(Contexto db, Medidor medidor)
        {
            this.db = db;
            this.medidor = medidor;
        }
        public IActionResult Index()
        {
            ViewData["ConsumoMensal"] = medidor.ConsumoMensal();
            var ultimaLeitura = db.LeiturasDoRelogio.OrderBy(x=>x.Registro.Value.Date).Last();
            return View(ultimaLeitura);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
