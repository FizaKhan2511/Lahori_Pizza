using Lahori_Pizza.Data;
using Lahori_Pizza.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lahori_Pizza.Controllers
{
    [Route("[controller]")]//dans la route(le chemin, l'url) il y a déja api. Donc on peut enlever le api sinon on l'écrirait deux fois
    [ApiController]
    public class ApiController : Controller/*Base*///contrillerBase dérive de Controller. J'ai du enlever le base pour que le json fonctionne
    {
        // GET: api/<ApiController>
        /* [HttpGet]
         public IEnumerable<string> Get()//le get() correspond à l'index
         {
             return new string[] { "value1", "value2" };
         }*/
        /***********************************************/
        DataContext context;//pour recupérer les pizzas en forme json à l'aide de la méthode getPizza
                            //aussi rajout du constructeur juste en dessous en plus de getPizza

        public ApiController(DataContext context)
        {
           this.context = context;    

        }

       
        /******************************************************/

        [HttpGet]
        [Route("GetPizzas")]//c'est ça la fonction pour récuperer les pizzas et voila le chemin  /api/getpizzas
        public IActionResult /*IEnumerable<string>*/ GetPizzas()
        {
            
                var pizza = context.Pizzas.ToList();
                
                 return Json(pizza);
           
        }

        //j'ai effacé le reste du code car on en a pas besoin pour ce que l'on doit faire

        //pour d'autre controller on peut juste faire des copiers collers juste en dessous de cette fonction et changer les routes

    }
}
