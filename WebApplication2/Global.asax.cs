using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApplication2.Models;

namespace WebApplication2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //  Revisar si existe un rol de administrador, si no lo crea
            //if (!Roles.RoleExists("Administrador"))
            //{
            //Roles.CreateRole("Administrador");
            //}

            // IdentityResult ir;

            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!rm.RoleExists("Administrador"))
            {
                rm.Create(new IdentityRole("Administrador"));
            }

            if (!rm.RoleExists("Digitador"))
            {
                rm.Create(new IdentityRole("Digitador"));
            }

            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (um.FindByName("admin") == null)
            {
                var user = new ApplicationUser() { UserName = "admin" , Email = "admin@ejemplo.com" };
                um.Create(user, "123456");
                um.AddToRole(user.Id, "Administrador");
            }
            
            

            // Revisar si existe el usuario adminstrador, si no lo crea
            //if (Membership.GetUser("admin") == null)
            //{
            //Membership.CreateUser("admin", "123456Magdalena!", "admin@ejemplo.com");
            //Roles.AddUserToRole("admin", "Administrador");
            //}
        }
    }
}