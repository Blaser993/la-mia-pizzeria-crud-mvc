﻿using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizze = db.Pizze.ToList<Pizza>();

                return View("Index", pizze);
            }
            
        }

        public IActionResult Details(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza? foundPizza = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (foundPizza == null) 
                {
                    return NotFound($"La pizza con {id} non è stata trovata");
                }
                else
                {
                    return View("Details", foundPizza);
                }
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
    }
}