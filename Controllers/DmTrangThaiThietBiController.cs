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
    public class DmTrangThaiThietBiController : Controller
    {
        private readonly ThietBiContext _context;

        public DmTrangThaiThietBiController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: DmTrangThaiThietBis
        public async Task<IActionResult> Index()
        {
            return View(await _context.DmTrangThaiThietBis.ToListAsync());
        }

        // GET: DmTrangThaiThietBis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmTrangThaiThietBi = await _context.DmTrangThaiThietBis
                .FirstOrDefaultAsync(m => m.IdTrangThaiThietBi == id);
            if (dmTrangThaiThietBi == null)
            {
                return NotFound();
            }

            return View(dmTrangThaiThietBi);
        }

        // GET: DmTrangThaiThietBis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DmTrangThaiThietBis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTrangThaiThietBi,TrangThaiThietBi")] DmTrangThaiThietBi dmTrangThaiThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dmTrangThaiThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dmTrangThaiThietBi);
        }

        // GET: DmTrangThaiThietBis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmTrangThaiThietBi = await _context.DmTrangThaiThietBis.FindAsync(id);
            if (dmTrangThaiThietBi == null)
            {
                return NotFound();
            }
            return View(dmTrangThaiThietBi);
        }

        // POST: DmTrangThaiThietBis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTrangThaiThietBi,TrangThaiThietBi")] DmTrangThaiThietBi dmTrangThaiThietBi)
        {
            if (id != dmTrangThaiThietBi.IdTrangThaiThietBi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dmTrangThaiThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DmTrangThaiThietBiExists(dmTrangThaiThietBi.IdTrangThaiThietBi))
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
            return View(dmTrangThaiThietBi);
        }

        // GET: DmTrangThaiThietBis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmTrangThaiThietBi = await _context.DmTrangThaiThietBis
                .FirstOrDefaultAsync(m => m.IdTrangThaiThietBi == id);
            if (dmTrangThaiThietBi == null)
            {
                return NotFound();
            }

            return View(dmTrangThaiThietBi);
        }

        // POST: DmTrangThaiThietBis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dmTrangThaiThietBi = await _context.DmTrangThaiThietBis.FindAsync(id);
            if (dmTrangThaiThietBi != null)
            {
                _context.DmTrangThaiThietBis.Remove(dmTrangThaiThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DmTrangThaiThietBiExists(int id)
        {
            return _context.DmTrangThaiThietBis.Any(e => e.IdTrangThaiThietBi == id);
        }
    }
}
