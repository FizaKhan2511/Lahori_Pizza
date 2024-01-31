using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lahori_Pizza.Data;
using Lahori_Pizza.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lahori_Pizza.PagesPizzas
{
    [Authorize]//ne donne pas accès sans valider le login
    public class EditModel : PageModel
    {
        private readonly Lahori_Pizza.Data.DataContext _context;

        public EditModel(Lahori_Pizza.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pizza Pizza { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)//int ? parce que le int peut être nul
        {
            if (id == null)
            {
                return NotFound();
            }

            Pizza = await _context.Pizzas.FirstOrDefaultAsync(m => m.PizzaID == id);//trouve le 1er élément qui a cette id la

            if (Pizza == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // _context.Attach(Pizza).State = EntityState.Modified; il y a plus simple comme la en bas
            _context.Update(Pizza);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(Pizza.PizzaID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.PizzaID == id);
        }
    }
}
