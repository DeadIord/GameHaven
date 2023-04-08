using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;
using Web_Application.Data;
using Web_Application.Models;


namespace Web_Application.Controllers
{
    public class VisitingController : Controller
    {
        private ApplicationDbContext db;
        public VisitingController(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ListVisiting()
        {
            var visiting = await db.Visiting
                .Include(p => p.Visitor)
                .Include(p => p.Halls)
                .Include(p => p.Services)
                .Include(p =>p.Computers)
                .Include(p => p.ApplicationUser)
                .ToListAsync();

        

            return View(visiting.ToList());
        }
        public record SelectOptions
        {
            public int value { get; set; }
            public string text { get; set; }


        }
        public record SelectOption
        {
            public string value { get; set; }
            public string text { get; set; }


        }
        public async Task<IActionResult> AddVisitingAsync()
        {

            var services = await db.Services
              .Select(s => new SelectOptions
              {
                  value = s.ServiceCode,
                  text = s.NameOfTheService,
              })
              .ToListAsync();
            ViewData["Services"] = services;


            var halls = await db.Halls
                .Select(s => new SelectOptions
                {
                    value = s.HallsCode,
                    text = s.NameOfTheHall,
                })
                .ToListAsync();
            ViewData["Halls"] = halls;

            var employees = await db.Users
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    text = s.UserName,
                })
                .ToListAsync();
            ViewData["Employees"] = employees;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVisiting(Visiting visiting)
        {

            try
            {
              
                    db.Visiting.Add(visiting);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ListVisiting");
                
            }
            catch (Exception)
            {
                var services = await db.Services
                    .Select(s => new SelectOptions
                    {
                        value = s.ServiceCode,
                        text = s.NameOfTheService,
                    })
                    .ToListAsync();
                ViewData["Services"] = services;

                var halls = await db.Halls
                    .Select(s => new SelectOptions
                    {
                        value = s.HallsCode,
                        text = s.NameOfTheHall,
                    })
                    .ToListAsync();
                ViewData["Halls"] = halls;

                var employees = await db.Users
                    .Select(s => new SelectOption
                    {
                        value = s.Id,
                        text = s.UserName,
                    })
                    .ToListAsync();
                ViewData["Employees"] = employees;

                ModelState.AddModelError("", "Этот компьютер уже занят, выберите другое время.");

                return View(visiting);
            }

        }
        public async Task<IActionResult> EditVisiting(int? id)
        {
            var services = await db.Services
          .Select(s => new SelectOptions
          {
              value = s.ServiceCode,
              text = s.NameOfTheService,
          })
          .ToListAsync();
            TempData["Services"] = services;


            var halls = await db.Halls
                .Select(s => new SelectOptions
                {
                    value = s.HallsCode,
                    text = s.NameOfTheHall,
                })
                .ToListAsync();
            TempData["Halls"] = halls;

            var employees = await db.Users
                .Select(s => new SelectOption
                {
                    value = s.Id,
                    text = s.UserName,
                })
                .ToListAsync();
            TempData["Employees"] = employees;

            if (id != null)
            {
                Visiting visiting = await db.Visiting.FirstOrDefaultAsync(p => p.VisitingCode == id);
                if (visiting != null)
                    return View(visiting);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditVisiting(Visiting visiting, int selectedHallCode, int selectedComputerCode, DateTime selectedDate)
        {
           try
            {
                var existingVisit = db.Visiting
                    .SingleOrDefault(v => v.HallsCode == selectedHallCode
                        && v.ComputersId == selectedComputerCode
                        && v.DateOfVisit == selectedDate);


                db.Visiting.Update(visiting);
                await db.SaveChangesAsync();
                return RedirectToAction("ListVisiting");
                
            }
            catch (Exception)
            {
                var services = await db.Services
                    .Select(s => new SelectOptions
                    {
                        value = s.ServiceCode,
                        text = s.NameOfTheService,
                    })
                    .ToListAsync();
                TempData["Services"] = services;

                var halls = await db.Halls
                    .Select(s => new SelectOptions
                    {
                        value = s.HallsCode,
                        text = s.NameOfTheHall,
                    })
                    .ToListAsync();
                TempData["Halls"] = halls;

                var employees = await db.Users
                    .Select(s => new SelectOption
                    {
                        value = s.Id,
                        text = s.UserName,
                    })
                    .ToListAsync();
                TempData["Employees"] = employees;

                ModelState.AddModelError("", "Этот компьютер уже занят, выберите другое время");

                return View(visiting);
            }

        }

        [HttpGet]
        public JsonResult GetComputersByHallId(int hallId)
        {
            var computers = db.Computers
                .Where(c => c.HallsCode == hallId)
                .Select(c => new SelectOptions
                {
                    value = c.ComputersId,
                    text = c.ComputerName,
                })
                .ToList();
            return Json(computers);
        }

        [HttpGet]
        [ActionName("DeleteVisiting")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Visiting visiting = await db.Visiting.FirstOrDefaultAsync(p => p.VisitingCode == id);
                if (visiting != null)
                    return View(visiting);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Visiting visiting = new Visiting { VisitingCode = id.Value };
                db.Entry(visiting).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ListVisiting");
            }
            return NotFound();
        }

    

    }
}
