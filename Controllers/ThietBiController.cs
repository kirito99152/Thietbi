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
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "LoaiThietBi");
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "TrangThaiThietBi");
            return View();
        }

        // POST: ThietBi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThietBi,IdNguoiSoHuu,IdDonViSoHuu,IdTrangThaiThietBi,IdLoaiThietBi,NgayThemThietBi,TenThietBi,MoTa,MaThietBiHv,MaThietBiNhaSx,CauHinh,ViTriDat")] TbThietBi tbThietBi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng IdNguoiSoHuu
                    if (_context.TbThietBis.Any(e => e.IdNguoiSoHuu == tbThietBi.IdNguoiSoHuu))
                    {
                        ModelState.AddModelError("IdNguoiSoHuu", "ID Người Sở Hữu đã tồn tại.");
                        throw new Exception("ID Người Sở Hữu bị trùng.");
                    }

                    // Kiểm tra trùng IdDonViSoHuu
                    if (_context.TbThietBis.Any(e => e.IdDonViSoHuu == tbThietBi.IdDonViSoHuu))
                    {
                        ModelState.AddModelError("IdDonViSoHuu", "ID Đơn Vị Sở Hữu đã tồn tại.");
                        throw new Exception("ID Đơn Vị Sở Hữu bị trùng.");
                    }

                    _context.Add(tbThietBi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
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
            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "LoaiThietBi", tbThietBi.IdLoaiThietBi);
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "TrangThaiThietBi", tbThietBi.IdTrangThaiThietBi);
            return View(tbThietBi);
        }

        // POST: ThietBi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThietBi,IdNguoiSoHuu,IdDonViSoHuu,IdTrangThaiThietBi,IdLoaiThietBi,NgayThemThietBi,TenThietBi,MoTa,MaThietBiHv,MaThietBiNhaSx,CauHinh,ViTriDat")] TbThietBi tbThietBi)
        {
            if (id != tbThietBi.IdThietBi)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng IdNguoiSoHuu
                    if (_context.TbThietBis.Any(e => e.IdNguoiSoHuu == tbThietBi.IdNguoiSoHuu && e.IdThietBi != id))
                    {
                        ModelState.AddModelError("IdNguoiSoHuu", "ID Người Sở Hữu đã tồn tại.");
                        throw new Exception("ID Người Sở Hữu bị trùng.");
                    }

                    // Kiểm tra trùng IdDonViSoHuu
                    if (_context.TbThietBis.Any(e => e.IdDonViSoHuu == tbThietBi.IdDonViSoHuu && e.IdThietBi != id))
                    {
                        ModelState.AddModelError("IdDonViSoHuu", "ID Đơn Vị Sở Hữu đã tồn tại.");
                        throw new Exception("ID Đơn Vị Sở Hữu bị trùng.");
                    }

                    _context.Update(tbThietBi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
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
