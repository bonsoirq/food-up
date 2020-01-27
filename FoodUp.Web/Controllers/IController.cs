using System;
using Microsoft.AspNetCore.Mvc;

namespace FoodUp.Web.Controllers
{
    public interface ICookieGetter
    {
        bool ContainsCookie(string key);
    }
}
