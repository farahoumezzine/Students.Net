using PeojetEtudiant.Data;
using PeojetEtudiant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeojetEtudiant.Data;
using PeojetEtudiant.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PeojetEtudiant.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Classes.OrderBy(c => c.labelle).ToListAsync());
        }

        // GET: Classe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .Include(c => c.Etudiants)
                .FirstOrDefaultAsync(m => m.Id_c == id);
            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);
        }

        // GET: Classe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_c,Labelle")] Classse classe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classe);
        }

        // GET: Classe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }
            return View(classe);
        }

        // POST: Classe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_c,Labelle")] Classse classe)
        {
            if (id != classe.Id_c)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasseExists(classe.Id_c))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(classe);
        }

        // GET: Classe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .FirstOrDefaultAsync(m => m.Id_c == id);
            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);
        }

        // POST: Classe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classe = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasseExists(int id)
        {
            return _context.Classes.Any(e => e.Id_c == id);
        }
    }
}