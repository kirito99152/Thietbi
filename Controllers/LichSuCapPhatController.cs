using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ database
            List<TbLichSuCapPhat> data = await _context.TbLichSuCapPhats.Include(t => t.IdThietBiNavigation)
                .ToListAsync();

            // Tạo một file Excel với EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách lịch sử cấp phát");

                // Hợp nhất và đặt tiêu đề lớn
                worksheet.Cells[1, 1, 1, 7].Merge = true;
                worksheet.Cells[1, 1].Value = "Báo cáo danh sách lịch sử cấp phát";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Tiêu đề bảng
                worksheet.Cells[2, 1].Value = "ID NGƯỜI ĐƯỢC GIAO";
                worksheet.Cells[2, 2].Value = "ID ĐƠN VỊ ĐƯỢC GIAO";
                worksheet.Cells[2, 3].Value = "ID NGƯỜI CẤP PHÁT";
                worksheet.Cells[2, 4].Value = "THỜI GIAN CẤP PHÁT";
                worksheet.Cells[2, 5].Value = "THỜI GIAN THU HỒI";
                worksheet.Cells[2, 6].Value = "GHI CHÚ";
                worksheet.Cells[2, 7].Value = "THIẾT BỊ";

                // Định dạng tiêu đề
                using (var range = worksheet.Cells[2, 1, 2, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                }

                // Thêm dữ liệu vào bảng
                int row = 3;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.IdNguoiDuocGiao;
                    worksheet.Cells[row, 2].Value = item.IdDonViDuocGiao;
                    worksheet.Cells[row, 3].Value = item.IdNguoiCapPhat;
                    worksheet.Cells[row, 4].Value = item.ThoiGianCapPhat;
                    worksheet.Cells[row, 5].Value = item.ThoiGianThuHoi;
                    worksheet.Cells[row, 6].Value = item.GhiChu;
                    worksheet.Cells[row, 7].Value = item.IdThietBiNavigation?.TenThietBi;
                    row++;
                }

                // Thêm viền cho toàn bộ bảng
                worksheet.Cells[2, 1, row - 1, 7].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 7].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 7].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 7].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells.AutoFitColumns();

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DanhSachLichSuCapPhat_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }


        public async Task<IActionResult> LSCP(int? idThietBi)
        {
            if (idThietBi == null)
            {
                return NotFound("ID Người Sở Hữu không hợp lệ.");
            }

            // Lấy danh sách lịch sử sửa chữa theo ID thiết bị
            var lichSuCapPhats = await _context.TbLichSuCapPhats
                .Include(t => t.IdThietBiNavigation)
                .Where(t => t.IdThietBi == idThietBi)
                .ToListAsync();

            if (lichSuCapPhats == null || !lichSuCapPhats.Any())
            {
                return NotFound("Không tìm thấy lịch sử của thiết này");
            }

            // Trả về View hiển thị danh sách thiết bị
            ViewData["IdThietBi"] = idThietBi;
            return View(lichSuCapPhats);
        }
        private bool TbLichSuCapPhatExists(int id)
        {
            return _context.TbLichSuCapPhats.Any(e => e.IdLichSuCapPhat == id);
        }
    }
}
