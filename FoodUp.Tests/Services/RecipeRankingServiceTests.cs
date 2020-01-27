using FoodUp.Web.Models;
using FoodUp.Web.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodUp.Tests
{
    public class RecipeRankingServiceTests
    {
        private RecipeRankingService rankingService;

        [SetUp]
        public void Setup()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes.Add(new Recipe() { Id = 1, Title = "TestRecipe1" });
            recipes.Add(new Recipe() { Id = 2, Title = "TestRecipe2" });

            List<Review> reviews = new List<Review>();
            reviews.Add(new Review() { Id = 1, Rating = 1, RecipeId =1, Comment="TestComment1" });
            reviews.Add(new Review() { Id = 2, Rating = 5, RecipeId =1, Comment="TestComment2" });
            reviews.Add(new Review() { Id = 3, Rating = 2, RecipeId =2, Comment="TestComment3" });

            rankingService = new RecipeRankingService(recipes, reviews);

        }

        [Test]
        public void RecipeRankingServiceGetsBestRecipeByReviewRating()
        {
            Recipe result = rankingService.GetRecipeWithBestReview();
            Assert.AreEqual(1, result.Id);
        }
    }
}
