using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RecibosAxosPersistence.Context;
using RecibosAxosPersistence.Models;

[assembly: OwinStartup(typeof(RecibosAxosApi.Startup))]

namespace RecibosAxosApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configuracion global para que no ocurran errores de referencias circulares en serealización
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            ConfigureAuth(app);
            CreacionRoles();
        }
        /// <summary>
        /// Metodo para crear usuarios y roles (Siempre y cuando no existan roles ni usuarios)
        /// </summary>
        public void CreacionRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Administracion"))
                {
                    var role = new IdentityRole
                    {
                        Name = "Administracion"
                    };
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Root"))
                {
                    var role = new IdentityRole
                    {
                        Name = "Root"
                    };
                    roleManager.Create(role);

                    var user = new ApplicationUser
                    {
                        Email = "sa",
                        UserName = "sa",
                        //FechaRegistro = DateTime.Now
                    };
                    string password = "Aa123456";
                    var registrado = userManager.Create(user, password);
                    if (registrado.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Root");
                    }
                }
            }
        }
    }
}
