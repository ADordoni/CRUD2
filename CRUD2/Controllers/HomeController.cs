using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CRUD2.Models;

namespace CRUD2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home 
        public ActionResult Index()
        {
            Mantenimiento mant = new Mantenimiento();
            return View(mant.RecuperarTodos());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            Mantenimiento mant = new Mantenimiento();
            Articulo art = mant.Recuperar(id);
            return View(art);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Mantenimiento mant = new Mantenimiento();
            Articulo art = new Articulo()
            {
                Codigo = int.Parse(collection["codigo"]),
                Descripcion = collection["descripcion"],
                Precio = float.Parse(collection["precio"])
            };
            mant.Alta(art);
            return RedirectToAction("Index");
        }
        // GET: Home/Edit/5
        public ActionResult Edit (int id)
        {
            return View();
        }
        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit (int id, FormCollection collection)
        {
            Mantenimiento mant = new Mantenimiento();
            Articulo art = new Articulo
            {
                Codigo = id,
                Descripcion = collection["descripcion"].ToString(),
                Precio = float.Parse(collection["precio"].ToString())
            };
            mant.Modificar(art);
            return RedirectToAction("Index");
        }
        // GET: Home/Delete
        public ActionResult Delete (int id)
        {
            Mantenimiento mant = new Mantenimiento();
            Articulo art = mant.Recuperar(id);
            return View(art);
        }
        // POST: Home/Delete
        [HttpPost]
        public ActionResult Delete (int id, FormCollection collection)
        {
            Mantenimiento mant = new Mantenimiento();
            mant.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}