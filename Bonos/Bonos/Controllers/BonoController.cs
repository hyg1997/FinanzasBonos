﻿using Bonos.Helpers;
using Bonos.Finance;
using Bonos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Bonos.Controllers
{
    public class BonoController : Controller
    {
        // GET: Bono
        public ActionResult Index()
        {
            ViewBag.nombre = SessionHelper.nombre;
            ViewBag.apellido = SessionHelper.apellido;
            return View();
        }

        public ActionResult Calcular()
        {
            Selects.llenarDatos();
            return View();
        }

        [HttpPost]
        public ActionResult Calcular(Bono bono)
        {
            if (ModelState.IsValid)
            {
                bono.impuestoRenta = bono.impuestoRenta / 100;
                bono.tasaInteres = bono.tasaInteres / 100;
                bono.tasaDescuento = bono.tasaDescuento / 100;
                bono.pPrima = bono.pPrima / 100;
                bono.pFlota = bono.pFlota / 100;
                bono.pEstructura = bono.pEstructura / 100;
                bono.pColoca = bono.pColoca / 100;
                bono.pCAVALI = bono.pCAVALI / 100;
                using (var db = new BonosModel())
                {
                    var user = db.Usuario.FirstOrDefault(x => x.Id == SessionHelper.userID);
                    bono.Usuario = user;
                    bono.Calculo = MathCal.Resultados(bono, null);
                    bono.periodos = MathCal.ResultadosPeriodos(bono, bono.Calculo, new List<Periodo>());
                    db.Bono.Add(bono);
                    db.SaveChanges();
                    return RedirectToAction("Flujo", new { calculoID = bono.Calculo.Id });
                }
            }
            return View(bono);
        }
        public ActionResult Flujo(int calculoID)
        {
            List<Periodo> aux;
            Bono bono;
            using (var db = new BonosModel())
            {
                bono = db.Bono.Include(x => x.Calculo).Include(x => x.periodos).FirstOrDefault(x => x.Calculo.Id == calculoID);
                aux = bono.periodos;
                bono.periodos = MathCal.ResultadosPeriodos(bono, bono.Calculo, bono.periodos);
                bono.Calculo = MathCal.Resultados(bono, bono.periodos);
                db.SaveChanges();
            }

            if (bono.periodos.Any(x => x.plazoGracia != "S"))
            {
                Selects.llenarDatos();
            }

            SessionHelper.calculoID = bono.Calculo.Id;
            return View(aux);
        }

        [HttpPost]
        public ActionResult Flujo(List<Periodo> periodos)
        {
            Bono bono;
            using (var db = new BonosModel())
            {
                bono = db.Bono.Include(x => x.Calculo).Include(x => x.periodos).FirstOrDefault(x => x.Calculo.Id == SessionHelper.calculoID);
                foreach (var item in bono.periodos)
                {
                    item.plazoGracia = periodos[item.N].plazoGracia;
                }
                bono.periodos = MathCal.ResultadosPeriodos(bono, bono.Calculo, bono.periodos);
                bono.Calculo = MathCal.Resultados(bono, bono.periodos);
                db.SaveChanges();
            }
            return RedirectToAction("Flujo", new { calculoID = bono.Calculo.Id });
        }

        public ActionResult Detalle()
        {
            Bono bono;
            using (var db = new BonosModel())
            {
                bono = db.Bono.Include(x => x.Calculo).Include(x => x.periodos).FirstOrDefault(x => x.Calculo.Id == SessionHelper.calculoID);
            }
            return View(bono.Calculo);
        }

        public ActionResult Listar()
        {
            List<Bono> Bonos;
            using (var db = new BonosModel())
            {
                Bonos = db.Bono.Include(x => x.Usuario).Where(x => x.Usuario.Id == SessionHelper.userID).ToList();

            }
            return View(Bonos);
        }

        public ActionResult Eliminar(int bonoId)
        {
            using (var db = new BonosModel())
            {
                var aux = db.Bono.Include(x=>x.periodos).Include(x=>x.Calculo)
                    .FirstOrDefault(x => x.Id == bonoId);
                db.Bono.Remove(aux);
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
        }

    }
}