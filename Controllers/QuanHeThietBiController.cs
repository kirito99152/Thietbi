using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Thietbi.Models;

namespace Thietbi.Controllers
{
    public class QuanHeThietBiController : Controller
    {
        private readonly ThietBiContext _context;

        public QuanHeThietBiController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: QuanHeThietBi
        public async Task<IActionResult> Index()
        {
            var thietBiContext = _context.TbQuanHeThietBis.Include(t => t.IdThietBiChaNavigation).Include(t => t.IdThietBiConNavigation);
            return View(await thietBiContext.ToListAsync());
        }

        // GET: QuanHeThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis
                .Include(t => t.IdThietBiChaNavigation)
                .Include(t => t.IdThietBiConNavigation)
                .FirstOrDefaultAsync(m => m.IdQuanHeThietBi == id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }

            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Create
        public IActionResult Create()
        {
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi");
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi");
            return View();
        }

        // POST: QuanHeThietBi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuanHeThietBi,IdThietBiCha,IdThietBiCon")] TbQuanHeThietBi tbQuanHeThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbQuanHeThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis.FindAsync(id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // POST: QuanHeThietBi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuanHeThietBi,IdThietBiCha,IdThietBiCon")] TbQuanHeThietBi tbQuanHeThietBi)
        {
            if (id != tbQuanHeThietBi.IdQuanHeThietBi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbQuanHeThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbQuanHeThietBiExists(tbQuanHeThietBi.IdQuanHeThietBi))
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
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis
                .Include(t => t.IdThietBiChaNavigation)
                .Include(t => t.IdThietBiConNavigation)
                .FirstOrDefaultAsync(m => m.IdQuanHeThietBi == id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }

            return View(tbQuanHeThietBi);
        }

        // POST: QuanHeThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbQuanHeThietBi = await _context.TbQuanHeThietBis.FindAsync(id);
            if (tbQuanHeThietBi != null)
            {
                _context.TbQuanHeThietBis.Remove(tbQuanHeThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbQuanHeThietBiExists(int id)
        {
            return _context.TbQuanHeThietBis.Any(e => e.IdQuanHeThietBi == id);
        }
    }
}
