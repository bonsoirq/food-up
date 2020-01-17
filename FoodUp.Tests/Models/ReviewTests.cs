using FoodUp.Web.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodUp.Tests
{
    public class ReviewTests
    {
        private Review review;

        [SetUp]
        public void Setup()
        {
            review = new Review();
            review.Id = 1;
            review.Rating = 3;
            review.Comment = "Test comment";
            
        }

        [Test]
        public void FullReviewRateIsNotNull()
        {
            string result = review.GetFullReviewRate();
            Assert.IsNotNull(result);
        }
    }
}
