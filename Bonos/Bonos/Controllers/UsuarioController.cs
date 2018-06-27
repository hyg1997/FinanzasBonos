using Bonos.Models;
using Bonos.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonos.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            using (var db = new BonosModel())
            {
                var aux = db.Usuario.FirstOrDefault(x => x.username == usuario.username && x.password == usuario.password);
                if (aux != null)
                {
                    SessionHelper.userID = aux.Id;
                    SessionHelper.nombre = aux.nombre;
                    SessionHelper.apellido = aux.apellido;
                    return RedirectToAction("Index", "Bono"); 
                }
                ModelState.AddModelError("notFound", "Nombre de usuario y/o contraseña incorrectos");
            }
            
            return View(usuario);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario usuario)
        {
            using (var db = new BonosModel())
            {
                if (ModelState.IsValid)
                {
                    var aux = db.Usuario.FirstOrDefault(x => x.username == usuario.username && x.password == usuario.password);
                    if (aux == null)
                    {
                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(usuario);
        }
    }
}