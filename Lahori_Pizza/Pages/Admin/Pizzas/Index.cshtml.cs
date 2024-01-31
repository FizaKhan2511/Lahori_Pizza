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
    [Authorize]//on doit ajouter �a apr�s avoir ajouter l'authentification dans le startup. C'est pour ne plus acceder � la page admin 
               //sans valider le login
    public class IndexModel : PageModel
    {
        private readonly Lahori_Pizza.Data.DataContext _context;

        public IndexModel(Lahori_Pizza.Data.DataContext context)
        {
            _context = context;
        }

        //il a cr�e une liste de pizza 
        //il a utiliser le datacontexe que l'on a cr�e et l'a appel� context et l'a stock� dans _context
        //c'est comme �a qu'il a lu dans la base de donn�es
        public IList<Pizza> Pizza { get;set; }

        public async Task OnGetAsync()
        {
            //Pizza contient l'ensemble des donn�es des pizzas
            Pizza = await _context.Pizzas.ToListAsync();
        }
    }
}
