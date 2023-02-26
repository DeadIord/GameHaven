using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web_Application.Data;
using Web_Application.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace Web_Application.Controllers
{
    public class VisitorController : Controller
    {
        private ApplicationDbContext db;
        public VisitorController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult ListVisitor(string searchString, string constantFilter)
        {
            var visitors = db.Visitors.Include(v => v.Visiting).AsQueryable();

            
            if (!string.IsNullOrEmpty(searchString))
            {
                visitors = visitors.Where(v => v.Name.Contains(searchString));
            }


            if (!string.IsNullOrEmpty(constantFilter))
            {
                visitors = visitors.Where(v => v.Constant == constantFilter);
            }

            return View(visitors.ToList());
        }

        [HttpPost]
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("VisitorCode"),
                                        new DataColumn("ФИО"),
                                        new DataColumn("Дата рождения"),
                                        new DataColumn("Адрес"),
                                         new DataColumn("Постоянный клиент")});

            var visitors = from customer in this.db.Visitors
                            select customer;

            foreach (var visitor in visitors)
            {
                dt.Rows.Add(visitor.VisitorCode, visitor.Name, visitor.DateOfBirth,visitor.Addres, visitor.Constant);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Information about visitors.xlsx");
                }
            }
        }

        public IActionResult AddVisitor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVisitor(Visitor visitor)
        {
            db.Visitors.Add(visitor);
            await db.SaveChangesAsync();
            return RedirectToAction("ListVisitor");
        }


        public async Task<IActionResult> EditVisitor(int? id)
        {
            if (id != null)
            {
                Visitor visitor = await db.Visitors.FirstOrDefaultAsync(p => p.VisitorCode == id);
                if (visitor != null)
                    return View(visitor);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditVisitor(Visitor visitor)
        {
            db.Visitors.Update(visitor);
            await db.SaveChangesAsync();
            return RedirectToAction("ListVisitor");
        }

       

        [HttpGet]
        [ActionName("DeleteVisitor")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Visitor visitor = await db.Visitors.FirstOrDefaultAsync(p => p.VisitorCode == id);
                if (visitor != null)
                    return View(visitor);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Visitor services = new Visitor { VisitorCode = id.Value };
                db.Entry(services).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("ListVisitor");
            }
            return NotFound();
        }
    }
}
