using Evaluaciones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.Administracion.Controllers
{
    public class UserController : Evaluaciones.Web.Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult MisDatos()
        {
            Evaluaciones.Membresia.Usuario usuario = this.CurrentUsuario;

            Evaluaciones.Persona persona = usuario.Persona;

            Evaluaciones.Web.UI.Areas.Administracion.Models.Persona model = new Evaluaciones.Web.UI.Areas.Administracion.Models.Persona
            {
                Id = persona.Id,
                Run = persona.Run,
                RunCuerpo = persona.RunCuerpo,
                RunDigito = persona.RunDigito,
                Nombres = persona.Nombres,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                Email = persona.Email,
                SexoCodigo = persona.SexoCodigo,
                FechaNacimiento = persona.FechaNacimiento,
                NacionalidadCodigo = persona.NacionalidadCodigo,
                EstadoCivilCodigo = persona.EstadoCivilCodigo,
                NivelEducacionalCodigo = persona.NivelEducacionalCodigo,
                RegionCodigo = persona.RegionCodigo,
                CiudadCodigo = persona.CiudadCodigo,
                ComunaCodigo = persona.ComunaCodigo,
                VillaPoblacion = persona.VillaPoblacion,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono,
                Celular = persona.Celular,
                Observaciones = persona.Observaciones
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MisDatos(Evaluaciones.Web.UI.Areas.Administracion.Models.Persona model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string run = model.Run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo;

            if (!int.TryParse(run.Substring(0, run.Length - 1), out runCuerpo))
            {
                this.ModelState.AddModelError("errorRunCuerpo", "El R.U.N. es erróneo");

                return this.View(model);
            }

            char runDigito = char.Parse(run.Replace(runCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(runCuerpo, runDigito))
            {
                this.ModelState.AddModelError("errorRunCuerpo", "El R.U.N. es erróneo");

                return this.View(model);
            }

            Evaluaciones.Persona persona = Evaluaciones.Persona.Get(runCuerpo, runDigito);

            using (Evaluaciones.Context context = new Evaluaciones.Context())
            {
                new Evaluaciones.Persona
                {
                    Id = persona == null ? Guid.NewGuid() : persona.Id,
                    RunCuerpo = runCuerpo,
                    RunDigito = runDigito,
                    Nombres = model.Nombres,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = string.IsNullOrEmpty(model.ApellidoMaterno) ? default(string) : model.ApellidoMaterno.Trim(),
                    Email = string.IsNullOrEmpty(model.Email) ? default(string) : model.Email.Trim(),
                    SexoCodigo = model.SexoCodigo,
                    FechaNacimiento = model.FechaNacimiento.HasValue ? model.FechaNacimiento.Value : default(DateTime),
                    NacionalidadCodigo = model.NacionalidadCodigo.HasValue ? model.NacionalidadCodigo.Value : default(short),
                    EstadoCivilCodigo = model.EstadoCivilCodigo.HasValue ? model.EstadoCivilCodigo.Value : default(short),
                    NivelEducacionalCodigo = model.NivelEducacionalCodigo.HasValue ? model.NivelEducacionalCodigo.Value : default(int),
                    RegionCodigo = model.RegionCodigo.HasValue ? model.RegionCodigo.Value : default(short),
                    CiudadCodigo = model.CiudadCodigo.HasValue ? model.CiudadCodigo.Value : default(short),
                    ComunaCodigo = model.ComunaCodigo.HasValue ? model.ComunaCodigo.Value : default(short),
                    VillaPoblacion = string.IsNullOrEmpty(model.VillaPoblacion) ? default(string) : model.VillaPoblacion.Trim(),
                    Direccion = string.IsNullOrEmpty(model.Direccion) ? default(string) : model.Direccion.Trim(),
                    Telefono = model.Telefono.HasValue ? model.Telefono.Value : default(int),
                    Celular = model.Celular.HasValue ? model.Celular.Value : default(int),
                    Observaciones = string.IsNullOrEmpty(model.Observaciones) ? default(string) : model.Observaciones.Trim()
                }.Save(context);

                context.SubmitChanges();
            }

            SwalAlert message = new SwalAlert("OK", "Su información ha sido guardada correctamente!", SwalTypeEvent.success);

            this.ViewBag.Message = message;

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(Evaluaciones.Web.UI.Areas.Administracion.Models.ChangePassword model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string password = Evaluaciones.Membresia.Account.EncryptPassword(model.Password);

            if (!this.CurrentUsuario.Password.Equals(password))
            {
                this.ModelState.AddModelError("errorPassword", "La contraseña actual no es correcta");

                return this.View(model);
            }

            Evaluaciones.Membresia.Account.DoChangePassword(this.CurrentUsuario, model.Password1, model.Password2);

            SwalAlert message = new SwalAlert("OK", "Sus contraseña fue cambiada correctamente!", SwalTypeEvent.success);

            this.ViewBag.Message = message;

            return this.View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            this.Session.Abandon();

            return this.RedirectToAction("Login", "Account", new { area = "Home" });
        }
    }
}