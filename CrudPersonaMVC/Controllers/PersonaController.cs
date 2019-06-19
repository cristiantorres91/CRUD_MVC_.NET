using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudPersonaMVC.Models;
using CrudPersonaMVC.Models.ViewModels;

namespace CrudPersonaMVC.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            //creamos una lista de nuestra clase ListarPersona
            List<ListarPersonas> list;
            using (PersonaEntities bd = new PersonaEntities())
            {
               //hacemos un select a nuestra tabla con los campos que queremos mostrar
                list = (from b in bd.Persona
                        select new ListarPersonas
                        {
                            Id = b.Id,
                            Nombre = b.Nombre,
                            Apellido = b.Apellido,
                            Edad = b.Edad,
                        }).ToList();
            }
                return View(list);
        }

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(PersonaViewModel persona)
        {
            try
            {
                //validar datos
                if(ModelState.IsValid)
                {
                    //guardamos datos en bd
                    using (PersonaEntities bd = new PersonaEntities())
                    {
                        var opersona = new Persona();
                        opersona.Nombre = persona.Nombre;
                        opersona.Apellido = persona.Apellido;
                        opersona.Edad = persona.Edad;

                        bd.Persona.Add(opersona);
                        bd.SaveChanges();
                    }
                    //luego de agregar registro redirecionamos hacia nuestro index
                    return Redirect("~/Persona/");
                }

                return View(persona);
            }

            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            PersonaViewModel model = new PersonaViewModel();
            using (PersonaEntities bd = new PersonaEntities())
            {
                var opersona = bd.Persona.Find(id);//obtenemos registro por medio de id
                model.Id = opersona.Id;
                model.Nombre = opersona.Nombre;
                model.Apellido = opersona.Apellido;
                model.Edad = opersona.Edad;
            }

            return View(model);

        }
        [HttpPost]
        public ActionResult Editar(PersonaViewModel persona)
        {
            try
            {
                //validar datos
                if (ModelState.IsValid)
                {
                    //guardamos datos en bd
                    using (PersonaEntities bd = new PersonaEntities())
                    {
                        var opersona = bd.Persona.Find(persona.Id);//obtenemos registro por medio de id
                        opersona.Nombre = persona.Nombre;
                        opersona.Apellido = persona.Apellido;
                        opersona.Edad = persona.Edad;

                        //editamos registro
                        bd.Entry(opersona).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }

                    return Redirect("~/Persona/");
                }

                return View(persona);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {

            PersonaViewModel model = new PersonaViewModel();
            using (PersonaEntities bd = new PersonaEntities())
            {
                var opersona = bd.Persona.Find(id);//obtengo entidad con id

                bd.Persona.Remove(opersona);

                bd.SaveChanges();

            }
            return Content("ok");
        }

    }
}