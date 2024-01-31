using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lahori_Pizza.Data;
using Lahori_Pizza.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lahori_Pizza.PagesPizzas
{
    [Authorize]//ne donne pas accès sans valider le login
    public class CreateModel : PageModel
    {
        private readonly Lahori_Pizza.Data.DataContext _context;

        public CreateModel(Lahori_Pizza.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pizza Pizza { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //pas de id parce qu'il crée une nouvelle pizza
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pizzas.Add(Pizza);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
