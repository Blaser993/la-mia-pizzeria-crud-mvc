using la_mia_pizzeria_static.Database;
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
                    //return NotFound($"La pizza con {id} non è stata trovata");
                    return View("PizzaNotFound");
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }

            using(PizzaContext db = new PizzaContext())
            {
                db.Pizze.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza?pizzaToEdit = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(pizzaToEdit);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza oldPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", oldPizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToEdit = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Name = oldPizza.Name;
                    pizzaToEdit.Description = oldPizza.Description;
                    pizzaToEdit.Image = oldPizza.Image;
                    pizzaToEdit.Prize = oldPizza.Prize;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                else
                {
                    return NotFound();
                }
            }
        }

    }
}
