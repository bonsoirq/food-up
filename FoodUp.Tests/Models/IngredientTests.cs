using FoodUp.Web.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodUp.Tests
{
    public class IngredientTests
    {
        private Ingredient ingredient;

        [SetUp]
        public void Setup()
        {
            ingredient = new Ingredient();
            ingredient.Id = 1;
            ingredient.Name = "Bananas";
            ingredient.Quantity = 123;
            ingredient.Unit = "pcs";

        }

        [Test]
        public void IngredientFullDescriptionIsNotNull()
        {
            string result = ingredient.GetIngredientFullDescription();
            Assert.IsNotNull(result);
        }
    }
}
