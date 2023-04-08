using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Web_Application.Data;
using Web_Application.Models;

namespace Web_Application.Controllers
{
    public class ComputerController : Controller
    {
        private ApplicationDbContext db;
        public ComputerController(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ListComputer()
        {
            var computer = db.Computers.Include(p => p.Halls);
            return View(await computer.ToListAsync());
        }
      

        public IActionResult AddComputer()
        {
            var halls = db.Halls
                .Select(s => new SelectOptions
                {
                    value = s.HallsCode,
                    text = s.NameOfTheHall
                })
                .ToList();
            TempData["halls"] = halls;


            return View();

        }
        public record SelectOptions
        {
            public int value { get; set; }
            public string text { get; set; }
        }

        public async Task<IActionResult> EditComputer(int? id)
        {
           

            if (id != null)
            {
                Computers computers = await db.Computers.FirstOrDefaultAsync(p => p.ComputersId == id);
                var halls = db.Halls
             .Select(s => new SelectOptions
             {
                 value = s.HallsCode,
                 text = s.NameOfTheHall
             })
             .ToList();
                TempData["halls"] = halls;
               
                if (computers != null)
                    return View(computers);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddComputer(Computers computers)
        {
            if (ModelState.IsValid)
            {
                db.Computers.Add(computers);
                await db.SaveChangesAsync();
                return RedirectToAction("ListComputer");
            }
            else
            {
                var halls = db.Halls
              .Select(s => new SelectOptions
              {
                  value = s.HallsCode,
                  text = s.NameOfTheHall
              })
              .ToList();
                TempData["halls"] = halls;
                return View(computers);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditComputer(Computers computers)
        {
            if (ModelState.IsValid)
            {
                db.Computers.Update(computers);
                await db.SaveChangesAsync();
                return RedirectToAction("ListComputer");
            }
            else
            {
                var halls = db.Halls
         .Select(s => new SelectOptions
         {
             value = s.HallsCode,
             text = s.NameOfTheHall
         })
         .ToList();
                TempData["halls"] = halls;
                return View(computers);
            }
           
        }



        [HttpGet]
        [ActionName("DeleteComputer")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Computers computers = await db.Computers.FirstOrDefaultAsync(p => p.ComputersId == id);
                if (computers != null)
                    return View(computers);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Computers computers = new() { ComputersId = id.Value };
                db.Entry(computers).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ListComputer");
            }
            return NotFound();
        }
    }
}
