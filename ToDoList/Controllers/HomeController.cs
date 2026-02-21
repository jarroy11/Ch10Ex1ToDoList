using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx;

        public ViewResult Index(string id)
        {
            // load current filters and data needed for filter drop downs in ViewBag
<<<<<<< HEAD
            var model = new ToDoViewModel
            {
                Filters = new Filters(id),
                Categories = context.Categories.ToList(),
                Statuses = context.Statuses.ToList(),
                DueFilters = Filters.DueFilterValues
            };
=======
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9

            // get open tasks from database based on current filters
            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Category).Include(t => t.Status);

<<<<<<< HEAD
            if (model.Filters.HasCategory)
                query = query.Where(t => t.StatusId == model.Filters.StatusId);
            if (model.Filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == model.Filters.StatusId);
            }
            if (model.Filters.HasDue)
            {
                var today = DateTime.Today;
                if (model.Filters.IsPast)
                    query = query.Where(t => t.DueDate < today);
                else if (model.Filters.IsFuture)
                    query = query.Where(t => t.DueDate > today);
                else if (model.Filters.IsToday)
                    query = query.Where(t => t.DueDate == today);
            }
            model.Tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(model);
=======
            if (filters.HasCategory) {
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }
            if (filters.HasStatus) {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            if (filters.HasDue) {
                var today = DateTime.Today;
                if (filters.IsPast)
                    query = query.Where(t => t.DueDate < today);
                else if (filters.IsFuture)
                    query = query.Where(t => t.DueDate > today);
                else if (filters.IsToday)
                    query = query.Where(t => t.DueDate == today);
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(tasks);
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9
        }

        [HttpGet]
        public ViewResult Add()
        {
<<<<<<< HEAD
            var model = new ToDoViewModel
            {
                Categories = context.Categories.ToList(),
                Statuses = context.Statuses.ToList(),
                CurrentTask = new ToDo { StatusId = "open" }, // set default value for drop-down
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ToDoViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(model.CurrentTask);
=======
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            var task = new ToDo { StatusId = "open" };  // set default value for drop-down
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
<<<<<<< HEAD
                model.Categories = context.Categories.ToList();
                model.Statuses = context.Statuses.ToList();
                return View(model);
=======
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                return View(task);
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
<<<<<<< HEAD
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
=======
        public IActionResult MarkComplete([FromRoute]string id, ToDo selected)
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9
        {
            selected = context.ToDos.Find(selected.Id)!;  // use null-forgiving operator to suppress null warning
            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDos
                .Where(t => t.StatusId == "closed").ToList();

<<<<<<< HEAD
            foreach (var task in toDelete)
=======
            foreach(var task in toDelete)
>>>>>>> d451ef7aa1368b04911ea21640ee9a47e3fdfdd9
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}