using System;
using Microsoft.AspNetCore.Mvc;

namespace FoodUp.Web.Controllers
{
    public interface ICookieGetter
    {
        bool ContainsCookie(string key);
    }

    public class CookieController : Controller, ICookieGetter
    {
        virtual public bool ContainsCookie(string key)
        {
            return Request.Cookies.ContainsKey(key);
        }
    }
}
