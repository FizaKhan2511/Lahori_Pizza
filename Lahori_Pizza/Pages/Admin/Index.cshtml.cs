using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Lahori_Pizza.Pages.Admin
{
    public class IndexModel : PageModel
    {
        IConfiguration configuration;
        public bool messageError = false;
        public IndexModel(IConfiguration configuration) {//voir startup.cs et maintenant on a acc�s � configuration dans le getPost
          this.configuration = configuration;
        }
        public IActionResult OnGet() { 
        //pour tester si on est authentifi� ou pas et �a se fait dans le OnGet

        /*Le processus d'authentification dans ASP.NET Core peut se faire � diff�rents moments du cycle de vie d'une requ�te. 
         * Dans cet exemple sp�cifique, l'authentification est v�rifi�e dans la m�thode OnGet() d'une page Razor.
           Cela peut �tre d� � plusieurs raisons :

           1.V�rification initiale de l'authentification : Le d�veloppeur a choisi de v�rifier l'authentification au moment o� 
           la page est demand�e pour la premi�re fois. Cela permet de rediriger imm�diatement l'utilisateur vers une autre page
           s'il n'est pas authentifi�. Cette strat�gie est couramment utilis�e pour les pages qui n�cessitent une authentification
           avant d'y acc�der.
           2.Contr�le d'acc�s au chargement initial de la page : Dans de nombreux cas, une page peut n�cessiter une authentification
           pour acc�der � certaines fonctionnalit�s ou pour afficher des informations sensibles d�s le chargement initial. 
           V�rifier l'authentification dans OnGet() permet de contr�ler cet acc�s directement lors du chargement de la page.
           3.Simplicit� de mise en �uvre : Pour les besoins sp�cifiques de cette page, v�rifier l'authentification dans OnGet() peut
           simplifier la logique du contr�le d'acc�s. Cela peut �tre une d�cision de conception pour centraliser la logique
           d'authentification sp�cifique � cette page.

           En r�sum�, v�rifier l'authentification dans OnGet() peut �tre une d�cision de conception en fonction des besoins 
           sp�cifiques de la page et de la logique d'authentification envisag�e pour cette application.*/

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

            public async Task<IActionResult> OnPost(string username, string password, string ReturnUrl)
               //la m�thod est de type async parce qu'il y a une fonction ansync dedans et donc elle returne une Task
               //on peut cr�e une m�thod onpost parce que le formulaire dans index.cshtml est post
            {
             var authSection = configuration.GetSection("Auth");//ce sont les informations qui se trouvent dans appsettings.json
            string adminLogin = authSection["AdminLogin"];
            String adminPassword = authSection["AdminPassword"];
              if ((username == adminLogin) && (password == adminPassword))
              {
                var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, username)
                    };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                //le username va se loger avec le cookiesAuthentification et le authorize de chaque sera ok
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl); //redirig� vers la page que l'on souhaite acc�der. 
                                                                                  //il ne faut pas oubli� de le mettre dans les param�tres aussi
                                                                                  //soit l'url n'est pas donn�e alors on va dans /Admin/Pizzas
                                                                                  //soit le returnUrl est donn� et on va pas ex dans
                ///Admin/Pizza/edit...

              }
              else
              {
                messageError = true;//pour afficher le message d'erreur dans index.cshtml
                return Page();//si le user name n'est pas bon on retourne � la page courante c'est pour �a que la fonction est 
                              //<IActionResult>. Si on ne mettait pas �a. On retournerai tout le temps � la page courant et pas que dans certain cas
            }
         }


       public async Task<IActionResult> OngetLogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");//pour retrouner sur la page admin apr�s la deconnection
        }

    }
}
