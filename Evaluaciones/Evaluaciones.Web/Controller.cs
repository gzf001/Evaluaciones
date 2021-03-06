﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Web
{
    //[AuthorizeSessionAttribute]
    public abstract class Controller : System.Web.Mvc.Controller
    {
        public Evaluaciones.Membresia.MenuItem CurrentMenuItem
        {
            get
            {
                Evaluaciones.Membresia.MenuItem menuItem;

                if (this.Request.UrlReferrer == null)
                {
                    menuItem = Evaluaciones.Membresia.MenuItem.Get(this.Request.CurrentExecutionFilePath, true);
                }
                else
                {
                    menuItem = Evaluaciones.Membresia.MenuItem.Get(this.Request.UrlReferrer.AbsolutePath, true);
                }

                return menuItem;
            }
        }

        public Evaluaciones.Persona CurrentPersona
        {
            get
            {
                if (string.IsNullOrEmpty(this.User.Identity.Name))
                {
                    return null;
                }

                Evaluaciones.Persona persona = Evaluaciones.Persona.Get(new Guid(this.User.Identity.Name));

                return persona;
            }
        }

        public Evaluaciones.Membresia.Usuario CurrentUsuario
        {
            get
            {
                if (string.IsNullOrEmpty(this.User.Identity.Name))
                {
                    return null;
                }

                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(new Guid(this.User.Identity.Name));

                return usuario;
            }
        }

        public Evaluaciones.Empresa CurrentEmpresa
        {
            get
            {
                Evaluaciones.Membresia.Perfil perfil = Evaluaciones.Membresia.Perfil.Get(Membresia.PerfilType.Sostenedor);

                Evaluaciones.Membresia.PerfilUsuario perfilUsuario = Evaluaciones.Membresia.PerfilUsuario.Get(perfil, this.CurrentUsuario);

                if (perfilUsuario == null)
                {
                    return null;
                }
                else
                {
                    Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(new Guid(perfilUsuario.Valor));

                    return empresa;
                }
            }
        }

        public Evaluaciones.CentroCosto CurrentCentroCosto
        {
            get
            {
                Evaluaciones.Membresia.Perfil perfil = Evaluaciones.Membresia.Perfil.Get(Membresia.PerfilType.Establecimiento);

                Evaluaciones.Membresia.PerfilUsuario perfilUsuario = Evaluaciones.Membresia.PerfilUsuario.Get(perfil, this.CurrentUsuario);

                if (perfilUsuario == null)
                {
                    return null;
                }
                else
                {
                    Evaluaciones.CentroCosto centroCosto = Evaluaciones.CentroCosto.Get(this.CurrentEmpresa.Id, new Guid(perfilUsuario.Valor));

                    return centroCosto;
                }
            }
        }

        public string GetError()
        {
            string text = string.Empty;

            foreach (ModelError error in this.ModelState.Values.Where<ModelState>(x => x.Errors.Any<ModelError>()).SelectMany<ModelState, ModelError>(x => x.Errors).Distinct())
            {
                text += error.ErrorMessage + Environment.NewLine;
            }

            return text;
        }
    }
}