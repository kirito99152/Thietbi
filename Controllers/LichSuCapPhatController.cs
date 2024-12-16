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
    public class LichSuCapPhatController : Controller
    {
        private readonly ThietBiContext _context;

        public LichSuCapPhatController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: LichSuCapPhat
        public async Task<IActionResult> Index()
        {
            var thietBiContext = _context.TbLichSuCapPhats.Include(t => t.IdThietBiNavigation);
            return View(await thietBiContext.ToListAsync());
        }

        // GET: LichSuCapPhat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuCapPhat = await _context.TbLichSuCapPhats
                .Include(t => t.IdThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdLichSuCapPhat == id);
            if (tbLichSuCapPhat == null)
            {
                return NotFound();
            }

            return View(tbLichSuCapPhat);
        }

        // GET: LichSuCapPhat/Create
        public IActionResult Create()
        {
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi");
            return View();
        }

        // POST: LichSuCapPhat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLichSuCapPhat,IdThietBi,IdNguoiDuocGiao,IdDonViDuocGiao,IdNguoiCapPhat,ThoiGianCapPhat,ThoiGianThuHoi,GhiChu")] TbLichSuCapPhat tbLichSuCapPhat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbLichSuCapPhat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbLichSuCapPhat.IdThietBi);
            return View(tbLichSuCapPhat);
        }

        // GET: LichSuCapPhat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuCapPhat = await _context.TbLichSuCapPhats.FindAsync(id);
            if (tbLichSuCapPhat == null)
            {
                return NotFound();
            }
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbLichSuCapPhat.IdThietBi);
            return View(tbLichSuCapPhat);
        }

        // POST: LichSuCapPhat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLichSuCapPhat,IdThietBi,IdNguoiDuocGiao,IdDonViDuocGiao,IdNguoiCapPhat,ThoiGianCapPhat,ThoiGianThuHoi,GhiChu")] TbLichSuCapPhat tbLichSuCapPhat)
        {
            if (id != tbLichSuCapPhat.IdLichSuCapPhat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbLichSuCapPhat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbLichSuCapPhatExists(tbLichSuCapPhat.IdLichSuCapPhat))
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
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbLichSuCapPhat.IdThietBi);
            return View(tbLichSuCapPhat);
        }

        // GET: LichSuCapPhat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuCapPhat = await _context.TbLichSuCapPhats
                .Include(t => t.IdThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdLichSuCapPhat == id);
            if (tbLichSuCapPhat == null)
            {
                return NotFound();
            }

            return View(tbLichSuCapPhat);
        }

        // POST: LichSuCapPhat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbLichSuCapPhat = await _context.TbLichSuCapPhats.FindAsync(id);
            if (tbLichSuCapPhat != null)
            {
                _context.TbLichSuCapPhats.Remove(tbLichSuCapPhat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbLichSuCapPhatExists(int id)
        {
            return _context.TbLichSuCapPhats.Any(e => e.IdLichSuCapPhat == id);
        }
    }
}
