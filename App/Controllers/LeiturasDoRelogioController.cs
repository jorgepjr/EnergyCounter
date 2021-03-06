﻿using System;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Service;
using EnergyCounter.Controllers;
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
            ViewData["ConsumoMensal"] = medidor.ConsumoMensal();
            var leituras = await db.LeiturasDoRelogio
            .Where(x => x.Registro.Value.Month == DateTime.Now.Month).OrderBy(x => x.Registro).ToListAsync();
            return View(leituras);
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(int kwh)
        {
            if (kwh <= 0)
            {
                TempData["Mensagem"] = "*valor inválido ";
                return RedirectToAction(nameof(Index));
            }
            if (medidor.LeituraDoDiaJaRealizada())
            {
                TempData["Mensagem"] = $"O registro de hoje já foi realizado!";
                return RedirectToAction(nameof(Index));
            }

            if (medidor.ValorEhMenorDoQueOsQueJaForamRegistrados(kwh))
            {
                TempData["Mensagem"] = $"Digite um valor maior que o último registrado!";
                return RedirectToAction(nameof(Index));
            }
            var leitura = new LeituraDoRelogio(kwh);
            db.Add(leitura);
            await medidor.RegistrarConsumo(leitura.Kwh);
            db.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var leitura = db.LeiturasDoRelogio.SingleOrDefault(x => x.Id == id);

            if (leitura is null)
            {
                return NotFound();
            }
            db.Remove(leitura);
            medidor.ZerarConsumoDoDiaAnterior();
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}