using System.Collections.Generic;

namespace FoodUp.Web.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }

        public string GetRecipeFullName()
        {
            return $"{Id}  - {Title}";
        }

    }
}
