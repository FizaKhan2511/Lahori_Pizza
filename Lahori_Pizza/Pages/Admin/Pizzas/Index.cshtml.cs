using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lahori_Pizza.Data;
using Lahori_Pizza.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lahori_Pizza.PagesPizzas
{
    [Authorize]//on doit ajouter ça après avoir ajouter l'authentification dans le startup. C'est pour ne plus acceder à la page admin 
               //sans valider le login
    public class IndexModel : PageModel
    {
        private readonly Lahori_Pizza.Data.DataContext _context;

        public IndexModel(Lahori_Pizza.Data.DataContext context)
        {
            _context = context;
        }

        //il a crée une liste de pizza 
        //il a utiliser le datacontexe que l'on a crée et l'a appelé context et l'a stocké dans _context
        //c'est comme ça qu'il a lu dans la base de données
        public IList<Pizza> Pizza { get;set; }

        public async Task OnGetAsync()
        {
            //Pizza contient l'ensemble des données des pizzas
            Pizza = await _context.Pizzas.ToListAsync();
        }
    }
}
