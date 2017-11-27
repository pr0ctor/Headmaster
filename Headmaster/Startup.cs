using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Headmaster.Models;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(Headmaster.Startup))]
namespace Headmaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "headmasteradmin@headmaster.com";
                user.Email = "headmasteradmin@headmaster.com";

                string userPWD = "Password11_";

                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    //var currentUser = UserManager.FindByName(user.UserName);
                    //var result1 = UserManager.AddToRole(currentUser.Id, "Administrator");
                    UserManager.AddToRole(user.Id, "Administrator");
                }
            }

            /* // creating Creating Manager role    
             if (!roleManager.RoleExists("Professor"))
             {
                 var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                 role.Name = "Professor";
                 roleManager.Create(role);

             }*/

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

            }
        }
    }
}
