using Lahori_Pizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Lahori_Pizza.Pages
{
    public class PizzaMenuModel : PageModel
    {
        /*il faut aller voir dans PizzaMenu.cshtml*/
        private readonly Lahori_Pizza.Data.DataContext _context;

        public PizzaMenuModel(Lahori_Pizza.Data.DataContext context)
        {
            _context = context;
           
        }
       
        public IList<Pizza> Pizza { get; set; }//IList c'est comme une liste. IList est une interface et List c'est une impl�mentation de la liste
                                               //une IList peut repr�senter n'importe quelle collection qui est diff�rent de List mais impl�mente
                                               //les m�me fonctionnalit�es comme l'acc�s par index
                                               //on aurait pu juste mettre List

        public async Task OnGetAsync()
        {
            //Pizza contient l'ensemble des donn�es des pizzas
            Pizza = await _context.Pizzas.ToListAsync();
            Pizza = Pizza.OrderBy(p => p.price).ToList();//pour afficher les pizzas par ordre des prix
        }
    }
}
