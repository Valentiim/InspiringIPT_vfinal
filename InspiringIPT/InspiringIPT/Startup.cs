using InspiringIPT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InspiringIPT.Startup))]
namespace InspiringIPT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //invocaçã de um método que vai configurar e criar as Roles e os primeiros Utilizadoes
            iniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //********************************************** INICIO COLABORADOR1 ************************************************
            // criar a Role 'Colaborador'
            if (!roleManager.RoleExists("Colaboradores")){
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Colaboradores";
                roleManager.Create(role);

                //user.UserName = "Rita Cruz";
                string userPWD = "kl096Ab:";

                // criar o utilizador 'Rita Cruz'
                var user = new ApplicationUser();
                user.UserName = "rita@ipt.pt";
                user.Email = "rita@ipt.pt";
                // user.EmailConfirmed = true;
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva ROLE-COLABORADORES
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Colaboradores");
                }
            }
            //********************************************* FIM ********************************************************

            //********************************************* INICIO COLABORADOR2 ***************************************
            // criar a Role 'COLABORADOR'
            if (!roleManager.RoleExists("Colaboradores"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Colaboradores";
                roleManager.Create(role);

                //user.UserName = "Artur Gaspar";
                string userPWD = "kl096Ab:";

                // criar o utilizador 'Artur Gaspar'
                var user = new ApplicationUser();
                user.UserName = "artur@ipt.pt";
                user.Email = "artur@ipt.pt";
                // user.EmailConfirmed = true;
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva ROLE-COLABORADORES
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Colaboradores");
                }
            }
            //************************************************  FIM ****************************************************

            //********************************************* INICIO GESTORES *********************************************
            // Criar a role 'GESTORES'
            if (!roleManager.RoleExists("Gestores"))
            {
                var role = new IdentityRole();
                role.Name = "Gestores";
                roleManager.Create(role);

                // criar utilizadores do tipo 'GESTOR'
                string[] loginDosUtilizadores = { "arrua.ti2@ipt.pt", "joana@ipt.pt", "casimiro@ipt.pt"};

                // define a mesma password para todos
                string userPWD = "kl096Ab:";

                // cria os utilizadores
                foreach (var email in loginDosUtilizadores)
                {
                    var user = new ApplicationUser();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;
                    var chkUser = userManager.Create(user, userPWD);

                    //Adicionar o Utilizador à respetiva Role-Colaborador-
                    if (chkUser.Succeeded)
                    {
                        var result1 = userManager.AddToRole(user.Id, "Gestores");
                    }
                }
            }
            //************************************************  FIM ****************************************************


            // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        }

    }
}