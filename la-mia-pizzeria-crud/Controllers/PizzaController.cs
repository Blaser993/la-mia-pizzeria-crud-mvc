using la_mia_pizzeria_static.CustomLogger;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(PizzaContext db,ICustomLogger n)
        {
            _myLogger = n;
            _myDatabase = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using(PizzaContext db = new PizzaContext())
            {
                _myLogger.WriteLog("sono nella pizza/index");
                List<Pizza> pizze = db.Pizze.Include(pizza => pizza.Category).ToList<Pizza>();

                return View("Index", pizze);
            }
            
        }

        public IActionResult Details(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza? foundPizza = db.Pizze.Where(pizza => pizza.Id == id)
                    .Include(pizza => pizza.Category).FirstOrDefault();

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
           
            
                List<Category> categories = _myDatabase.Categories.ToList();

                PizzaFormModel model = 
                new PizzaFormModel() { Pizza = new Pizza(), Categories = categories};


                return View("Create", model);
           


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {

                    List<Category> categories = _myDatabase.Categories.ToList();
                    data.Categories = categories;
                    return View("Create", data);
            }

                _myDatabase.Pizze.Add(data.Pizza);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");    
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
   
                Pizza?pizzaToEdit = _myDatabase.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Category> categories = _myDatabase.Categories.ToList();

                    PizzaFormModel model 
                    = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories};

                    return View("Update",model);
                }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                
         
                    List<Category> categories = _myDatabase.Categories.ToList();
                    data.Categories = categories;
                    return View("Update", data);
            
              
            }

            /*
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToEdit = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Name = data.Pizza.Name;
                    pizzaToEdit.Description = data.Pizza.Description;
                    pizzaToEdit.Image = data.Pizza.Image;
                    pizzaToEdit.Prize = data.Pizza.Prize;
                    pizzaToEdit.CategoryId = data.Pizza.CategoryId;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                else
                {
                    return NotFound();
                }
            }
            */

            Pizza? pizzaToUpdate = _myDatabase.Pizze.Find(id);

            if (pizzaToUpdate != null)
            {
                EntityEntry<Pizza> entryEntity = _myDatabase.Entry(pizzaToUpdate);
                entryEntity.CurrentValues.SetValues(data.Pizza);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Mi dispiace non è stata trovata la pizza da aggiornare");
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
           
           
                Pizza pizzaToDelete = _myDatabase.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    _myDatabase.Pizze.Remove(pizzaToDelete);

                    _myDatabase.SaveChanges();

                    return RedirectToAction("Index");
                }else
                {
                    return View("PizzaNotFound");
                }
           

            
        }

    }
}
