namespace FoodUp.Web.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ReviewerId { get; set; }
        public int RecipeId { get; set; }

        public string GetFullReviewRate()
        {
            return $"Review: {Id}, Rate: {Rating}";
        }

    }
}
