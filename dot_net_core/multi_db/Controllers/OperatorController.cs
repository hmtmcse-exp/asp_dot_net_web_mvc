using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using isms.Data;
using isms.Models.system;
using isms.Services;

namespace multi_db.Controllers
{
    public class OperatorController : Controller
    {
        private readonly ISMSContext _context;

        public OperatorController(ISMSContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var operators = from data in _context.Operator select data;
            return View(await ReadHelper<Operator>.listAsync(operators.AsNoTracking(), Request.Query));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operator @operator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@operator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@operator);
        }




    }
}
