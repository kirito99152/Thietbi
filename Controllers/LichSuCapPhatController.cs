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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLichSuCapPhat,IdThietBi,IdNguoiDuocGiao,IdDonViDuocGiao,IdNguoiCapPhat,ThoiGianCapPhat,ThoiGianThuHoi,GhiChu")] TbLichSuCapPhat tbLichSuCapPhat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng IdNguoiDuocGiao
                    if (_context.TbLichSuCapPhats.Any(e => e.IdNguoiDuocGiao == tbLichSuCapPhat.IdNguoiDuocGiao))
                    {
                        ModelState.AddModelError("IdNguoiDuocGiao", "ID Người Được Giao đã tồn tại.");
                        throw new Exception("ID Người Được Giao bị trùng.");
                    }
                    // Kiểm tra trùng IdDonViDuocGiao
                    if (_context.TbLichSuCapPhats.Any(e => e.IdDonViDuocGiao == tbLichSuCapPhat.IdDonViDuocGiao))
                    {
                        ModelState.AddModelError("IdDonViDuocGiao", "ID Đơn Vị Được Giao đã tồn tại.");
                        throw new Exception("ID Đơn Vị Được Giao bị trùng.");
                    }
                    // Kiểm tra trùng IdNguoiCapPhat
                    if (_context.TbLichSuCapPhats.Any(e => e.IdNguoiCapPhat == tbLichSuCapPhat.IdNguoiCapPhat))
                    {
                        ModelState.AddModelError("IdNguoiCapPhat", "ID Người Cấp Phát đã tồn tại.");
                        throw new Exception("ID Người Cấp Phát bị trùng.");
                    }

                    _context.Add(tbLichSuCapPhat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLichSuCapPhat,IdThietBi,IdNguoiDuocGiao,IdDonViDuocGiao,IdNguoiCapPhat,ThoiGianCapPhat,ThoiGianThuHoi,GhiChu")] TbLichSuCapPhat tbLichSuCapPhat)
        {
            if (id != tbLichSuCapPhat.IdLichSuCapPhat)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng IdNguoiDuocGiao
                    if (_context.TbLichSuCapPhats.Any(e => e.IdNguoiDuocGiao == tbLichSuCapPhat.IdNguoiDuocGiao && e.IdLichSuCapPhat != id))
                    {
                        ModelState.AddModelError("IdNguoiDuocGiao", "ID Người Được Giao đã tồn tại.");
                        throw new Exception("ID Người Được Giao bị trùng.");
                    }
                    // Kiểm tra trùng IdDonViDuocGiao
                    if (_context.TbLichSuCapPhats.Any(e => e.IdDonViDuocGiao == tbLichSuCapPhat.IdDonViDuocGiao && e.IdLichSuCapPhat != id))
                    {
                        ModelState.AddModelError("IdDonViDuocGiao", "ID Đơn Vị Được Giao đã tồn tại.");
                        throw new Exception("ID Đơn Vị Được Giao bị trùng.");
                    }
                    // Kiểm tra trùng IdNguoiCapPhat
                    if (_context.TbLichSuCapPhats.Any(e => e.IdNguoiCapPhat == tbLichSuCapPhat.IdNguoiCapPhat && e.IdLichSuCapPhat != id))
                    {
                        ModelState.AddModelError("IdNguoiCapPhat", "ID Người Cấp Phát đã tồn tại.");
                        throw new Exception("ID Người Cấp Phát bị trùng.");
                    }

                    _context.Update(tbLichSuCapPhat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
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
