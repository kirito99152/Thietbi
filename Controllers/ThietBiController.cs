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
    public class ThietBiController : Controller
    {
        private readonly ThietBiContext _context;

        public ThietBiController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: ThietBi
        public async Task<IActionResult> Index()
        {
            var thietBiContext = _context.TbThietBis.Include(t => t.IdLoaiThietBiNavigation).Include(t => t.IdTrangThaiThietBiNavigation);
            return View(await thietBiContext.ToListAsync());
        }

        // GET: ThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThietBi = await _context.TbThietBis
                .Include(t => t.IdLoaiThietBiNavigation)
                .Include(t => t.IdTrangThaiThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdThietBi == id);
            if (tbThietBi == null)
            {
                return NotFound();
            }

            return View(tbThietBi);
        }

        // GET: ThietBi/Create
        public IActionResult Create()
        {
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "IdLoaiThietBi");
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "IdTrangThaiThietBi");
            return View();
        }

        // POST: ThietBi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThietBi,IdNguoiSoHuu,IdDonViSoHuu,IdTrangThaiThietBi,IdLoaiThietBi,NgayThemThietBi,TenThietBi,MoTa,MaThietBiHv,MaThietBiNhaSx,CauHinh,ViTriDat")] TbThietBi tbThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "IdLoaiThietBi", tbThietBi.IdLoaiThietBi);
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "IdTrangThaiThietBi", tbThietBi.IdTrangThaiThietBi);
            return View(tbThietBi);
        }

        // GET: ThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThietBi = await _context.TbThietBis.FindAsync(id);
            if (tbThietBi == null)
            {
                return NotFound();
            }
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "IdLoaiThietBi", tbThietBi.IdLoaiThietBi);
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "IdTrangThaiThietBi", tbThietBi.IdTrangThaiThietBi);
            return View(tbThietBi);
        }

        // POST: ThietBi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThietBi,IdNguoiSoHuu,IdDonViSoHuu,IdTrangThaiThietBi,IdLoaiThietBi,NgayThemThietBi,TenThietBi,MoTa,MaThietBiHv,MaThietBiNhaSx,CauHinh,ViTriDat")] TbThietBi tbThietBi)
        {
            if (id != tbThietBi.IdThietBi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbThietBiExists(tbThietBi.IdThietBi))
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
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "IdLoaiThietBi", tbThietBi.IdLoaiThietBi);
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "IdTrangThaiThietBi", tbThietBi.IdTrangThaiThietBi);
            return View(tbThietBi);
        }

        // GET: ThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThietBi = await _context.TbThietBis
                .Include(t => t.IdLoaiThietBiNavigation)
                .Include(t => t.IdTrangThaiThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdThietBi == id);
            if (tbThietBi == null)
            {
                return NotFound();
            }

            return View(tbThietBi);
        }

        // POST: ThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbThietBi = await _context.TbThietBis.FindAsync(id);
            if (tbThietBi != null)
            {
                _context.TbThietBis.Remove(tbThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbThietBiExists(int id)
        {
            return _context.TbThietBis.Any(e => e.IdThietBi == id);
        }
    }
}
