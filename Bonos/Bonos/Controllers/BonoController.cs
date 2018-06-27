using Bonos.Helpers;
using Bonos.Finance;
using Bonos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    bono.Calculo = MathCal.ResultadosEstructura(bono);
                    db.Bono.Add(bono);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(bono);
        }
    }
}