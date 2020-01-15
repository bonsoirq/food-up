namespace FoodUp.Web.Models
{
    public class Ingredient
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public string GetIngredientFullDescription()
        {
            return $"{Name} - {Quantity} {Unit}";
        }
    }
}
