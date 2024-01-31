using Lahori_Pizza.Models;
using Microsoft.EntityFrameworkCore;//j'ai du installer moi meme "dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0"
                                    //dans le dossier de Lahori_Pizza car dans la rechercher de la gestion des depences la version n'était
                                    //pas compatible avec .net core 6

namespace Lahori_Pizza.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Pizza> Pizzas { get; set; }//enregistrement de model Pizza dans la collection DbSet qui va s'appeler Pizzas
                                                //et par défault la table qui va se créer dans la base de données va s'appeler Pizzas
                                                //c'est à partir de cette propirétée plublique que l'on pourra gérer les pizzas
    }

}
