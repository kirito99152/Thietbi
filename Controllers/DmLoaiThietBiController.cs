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
    public class DmLoaiThietBiController : Controller
    {
        private readonly ThietBiContext _context;

        public DmLoaiThietBiController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: DmLoaiThietBis
        public async Task<IActionResult> Index()
        {
            return View(await _context.DmLoaiThietBis.ToListAsync());
        }

        // GET: DmLoaiThietBis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmLoaiThietBi = await _context.DmLoaiThietBis
                .FirstOrDefaultAsync(m => m.IdLoaiThietBi == id);
            if (dmLoaiThietBi == null)
            {
                return NotFound();
            }

            return View(dmLoaiThietBi);
        }

        // GET: DmLoaiThietBis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DmLoaiThietBis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLoaiThietBi,LoaiThietBi")] DmLoaiThietBi dmLoaiThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dmLoaiThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dmLoaiThietBi);
        }

        // GET: DmLoaiThietBis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmLoaiThietBi = await _context.DmLoaiThietBis.FindAsync(id);
            if (dmLoaiThietBi == null)
            {
                return NotFound();
            }
            return View(dmLoaiThietBi);
        }

        // POST: DmLoaiThietBis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLoaiThietBi,LoaiThietBi")] DmLoaiThietBi dmLoaiThietBi)
        {
            if (id != dmLoaiThietBi.IdLoaiThietBi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dmLoaiThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DmLoaiThietBiExists(dmLoaiThietBi.IdLoaiThietBi))
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
            return View(dmLoaiThietBi);
        }

        // GET: DmLoaiThietBis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmLoaiThietBi = await _context.DmLoaiThietBis
                .FirstOrDefaultAsync(m => m.IdLoaiThietBi == id);
            if (dmLoaiThietBi == null)
            {
                return NotFound();
            }

            return View(dmLoaiThietBi);
        }

        // POST: DmLoaiThietBis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dmLoaiThietBi = await _context.DmLoaiThietBis.FindAsync(id);
            if (dmLoaiThietBi != null)
            {
                _context.DmLoaiThietBis.Remove(dmLoaiThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DmLoaiThietBiExists(int id)
        {
            return _context.DmLoaiThietBis.Any(e => e.IdLoaiThietBi == id);
        }
    }
}
