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
        public IndexModel(IConfiguration configuration) {//voir startup.cs et maintenant on a accès à configuration dans le getPost
          this.configuration = configuration;
        }
        public IActionResult OnGet() { 
        //pour tester si on est authentifié ou pas et ça se fait dans le OnGet

        /*Le processus d'authentification dans ASP.NET Core peut se faire à différents moments du cycle de vie d'une requête. 
         * Dans cet exemple spécifique, l'authentification est vérifiée dans la méthode OnGet() d'une page Razor.
           Cela peut être dû à plusieurs raisons :

           1.Vérification initiale de l'authentification : Le développeur a choisi de vérifier l'authentification au moment où 
           la page est demandée pour la première fois. Cela permet de rediriger immédiatement l'utilisateur vers une autre page
           s'il n'est pas authentifié. Cette stratégie est couramment utilisée pour les pages qui nécessitent une authentification
           avant d'y accéder.
           2.Contrôle d'accès au chargement initial de la page : Dans de nombreux cas, une page peut nécessiter une authentification
           pour accéder à certaines fonctionnalités ou pour afficher des informations sensibles dès le chargement initial. 
           Vérifier l'authentification dans OnGet() permet de contrôler cet accès directement lors du chargement de la page.
           3.Simplicité de mise en œuvre : Pour les besoins spécifiques de cette page, vérifier l'authentification dans OnGet() peut
           simplifier la logique du contrôle d'accès. Cela peut être une décision de conception pour centraliser la logique
           d'authentification spécifique à cette page.

           En résumé, vérifier l'authentification dans OnGet() peut être une décision de conception en fonction des besoins 
           spécifiques de la page et de la logique d'authentification envisagée pour cette application.*/

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

            public async Task<IActionResult> OnPost(string username, string password, string ReturnUrl)
               //la méthod est de type async parce qu'il y a une fonction ansync dedans et donc elle returne une Task
               //on peut crée une méthod onpost parce que le formulaire dans index.cshtml est post
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
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl); //redirigé vers la page que l'on souhaite accéder. 
                                                                                  //il ne faut pas oublié de le mettre dans les paramètres aussi
                                                                                  //soit l'url n'est pas donnée alors on va dans /Admin/Pizzas
                                                                                  //soit le returnUrl est donné et on va pas ex dans
                ///Admin/Pizza/edit...

              }
              else
              {
                messageError = true;//pour afficher le message d'erreur dans index.cshtml
                return Page();//si le user name n'est pas bon on retourne à la page courante c'est pour ça que la fonction est 
                              //<IActionResult>. Si on ne mettait pas ça. On retournerai tout le temps à la page courant et pas que dans certain cas
            }
         }


       public async Task<IActionResult> OngetLogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");//pour retrouner sur la page admin après la deconnection
        }

    }
}
