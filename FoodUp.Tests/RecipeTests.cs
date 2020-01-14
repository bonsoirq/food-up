using FoodUp.Web.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodUp.Tests
{
    public class RecipeTests
    {
        private Recipe recipe;

        [SetUp]
        public void Setup()
        {
            recipe = new Recipe();
            recipe.Id = 1;
            recipe.Title = "Test Title";

        }

        [Test]
        public void RecipeFullNameIsNotNull()
        {
            string result = recipe.GetRecipeFullName();
            Assert.IsNotNull(result);
        }
    }
}
