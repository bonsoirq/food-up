using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodUp.Web.Models
{
    public class RecipeRankingService
    {
        private List<Recipe> recipes;
        private List<Review> reviews;

        public RecipeRankingService(List<Recipe> recipes, List<Review> reviews)
        {
            this.recipes = recipes;
            this.reviews = reviews;
        }

        public Recipe GetRecipeWithBestReview()
        {
            Recipe topRecipe;
            Review topReview = reviews.OrderByDescending(x => x.Rating).FirstOrDefault();
            topRecipe = recipes.FirstOrDefault(x => x.Id == topReview.RecipeId);
            return topRecipe;
        }
    }
}
