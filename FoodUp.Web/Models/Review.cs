using System.ComponentModel.DataAnnotations;

namespace FoodUp.Web.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
        public int ReviewerId { get; set; }
        public int RecipeId { get; set; }

        public string GetFullReviewRate()
        {
            return $"Review: {Id}, Rate: {Rating}";
        }

    }
}
