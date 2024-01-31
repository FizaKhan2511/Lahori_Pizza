using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Lahori_Pizza.Models
{

    //IMPORTANT une fois le projet terminer il faut aller voir la vidéo consommer l'apo rest dans la serction 26 pour avoir un localhost

    //il faut aller voir le fichier guide pdf pour voir les diffétents étapes
    public class Pizza
    {
        [JsonIgnore] //pour ne pas afficher le id lors de la sortie en json(vois ApiController.cs)
        public int PizzaID { get; set; }

        [Display(Name = "Name")]//pour changer les noms dans le head du tableau
        public string name { get; set; }

        [Display(Name = "Price(euros)")]
        public float price { get; set; }

        [Display(Name = "Vegetarian")]
        public bool vegetarien { get; set; }


        [Display(Name = "Ingredient")]
        [JsonIgnore]
        public string ingredient { get; set; }

        [NotMapped] //ça veut dire qu'il ne faut pas stocker ça dans la base de données avec les autres propriétées plus haut
        [JsonPropertyName("Ingredient")]//changer le nom de TabIngredient en Ingredient lors de la sortie json
        public string[] TabIngredient { 
            get
            {
                if ((ingredient == null) || (ingredient.Count() == 0)) { return null; } 
               else return ingredient.Split(',');//ça va transformer la chaine de caractère en tableau d'ingrédient
            }
        }
    }
}
