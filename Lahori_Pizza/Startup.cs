using Lahori_Pizza.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

namespace Lahori_Pizza
{
    public class Startup
    {
        public Startup(IConfiguration configuration)//on va avoir besoin de �a dans le index.cshtml.cs de admin pour avoir acc�s a configuration
                                                    //depuis le onPost
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //point 2 du guide pdf. Ce n'est pas pour que l'utilisateur cr�� un compte pour se connecter. La c'est juste pour valider la connection dans
            //la page de login de admin pour accecder a la page d'admin. Ca sauvegarde les info de connection dans des cookies. Les cookies sont 
            //des informations qui sont stock�es dans notre navigateur interne pour chaque site web et eviter de redonner le mdp � chaque fois
            //il faut aussi ajouter useauthentification plus bas dans cette page et aussi ajouter dans l'index.cshtml.cs de pizza [authorize]
              services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
               options.LoginPath = "/Admin";//on dit que la page de login est dans /admin(l'index)
              });

            //il faut allez voir le point 3 dans le pdf et copier les ligne pour enregistrer le datacontext
            services.AddDbContext<DataContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));//sql c'est une base de donn�es local donc pas pour
                                                                                       //un serveur web
                                                                                       //le getConnectionString est li� au mot cl� connectionStrings
                                                                                       //qui se trouve dans appsettings.json

            //pour le UseSqlite aussi j'ai du installer le package qui est compatible avec .net core 6.0 dans le terminal
            //configuration nous permet d'acceder � appsetting.json

            services.AddRazorPages();
            services.AddControllers(); //Ca il faut rajouter quand on cr�� les controllers pour les API. J'ai ajout� le dossier 
                                       //controller juste avant. Il faut aussi ajouter le  endpoints.MapControllers() � la fin
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-GB");//code pris du guide dans le point 8 culture. Pour le prix d�cimal des pizzas
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";//on est en culture anglaises et que pour les nombres on va prendre le s�parateur point
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;//on doit tenir compte de �a dans la thread principale
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;//et dans la UI
            //Mais ces lignes de code de culture ne me servent � rien parce que �a fonctionne sans mais avec virgule pas le point

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//�a aussi il faut rajouter pour utiliser authentification par les cookies
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();//Il faut ajout� �a quand on cr�e les controllers
            });
        }
    }
}
