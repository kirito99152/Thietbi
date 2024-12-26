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
        public async Task<IActionResult> Create([Bind("IdNguoiSoHuu,IdDonViSoHuu,IdTrangThaiThietBi,IdLoaiThietBi,NgayThemThietBi,TenThietBi,MoTa,MaThietBiHv,MaThietBiNhaSx,CauHinh,ViTriDat")] TbThietBi tbThietBi)
        {
            try
            {
                if (ModelState.IsValid)
                {
              

                    _context.Add(tbThietBi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            ViewData["IdLoaiThietBi"] = new SelectList(_context.DmLoaiThietBis, "IdLoaiThietBi", "LoaiThietBi", tbThietBi.IdLoaiThietBi);
            ViewData["IdTrangThaiThietBi"] = new SelectList(_context.DmTrangThaiThietBis, "IdTrangThaiThietBi", "TrangThaiThietBi", tbThietBi.IdTrangThaiThietBi);
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

        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ database
            List<TbThietBi> data = await _context.TbThietBis
                .Include(t => t.IdLoaiThietBiNavigation)
                .Include(t => t.IdTrangThaiThietBiNavigation)
                .ToListAsync();

            // Tạo một file Excel với EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách thiết bị");

                // Hợp nhất và đặt tiêu đề lớn
                worksheet.Cells[1, 1, 1, 12].Merge = true;
                worksheet.Cells[1, 1].Value = "Báo cáo danh sách thiết bị";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Tiêu đề bảng
                worksheet.Cells[2, 1].Value = "ID";
                worksheet.Cells[2, 2].Value = "ID Người Sở Hữu";
                worksheet.Cells[2, 3].Value = "ID Đơn Vị Sở Hữu";
                worksheet.Cells[2, 4].Value = "Ngày Thêm";
                worksheet.Cells[2, 5].Value = "Tên Thiết Bị";
                worksheet.Cells[2, 6].Value = "Mô Tả";
                worksheet.Cells[2, 7].Value = "Mã HV";
                worksheet.Cells[2, 8].Value = "Mã Nhà SX";
                worksheet.Cells[2, 9].Value = "Cấu Hình";
                worksheet.Cells[2, 10].Value = "Vị Trí Đặt";
                worksheet.Cells[2, 11].Value = "Loại Thiết Bị";
                worksheet.Cells[2, 12].Value = "Trang Thái";

                // Định dạng tiêu đề
                using (var range = worksheet.Cells[2, 1, 2, 12])
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
                    worksheet.Cells[row, 1].Value = item.IdThietBi;
                    worksheet.Cells[row, 2].Value = item.IdNguoiSoHuu;
                    worksheet.Cells[row, 3].Value = item.IdDonViSoHuu;
                    worksheet.Cells[row, 4].Value = item.NgayThemThietBi;
                    worksheet.Cells[row, 5].Value = item.TenThietBi;
                    worksheet.Cells[row, 6].Value = item.MoTa;
                    worksheet.Cells[row, 7].Value = item.MaThietBiHv;
                    worksheet.Cells[row, 8].Value = item.MaThietBiNhaSx;
                    worksheet.Cells[row, 9].Value = item.CauHinh;
                    worksheet.Cells[row, 10].Value = item.ViTriDat;
                    worksheet.Cells[row, 11].Value = item.IdLoaiThietBiNavigation?.LoaiThietBi;
                    worksheet.Cells[row, 12].Value = item.IdTrangThaiThietBiNavigation?.TrangThaiThietBi;
                    row++;
                }

                // Thêm viền cho toàn bộ bảng
                worksheet.Cells[2, 1, row - 1, 12].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 12].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 12].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 12].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells.AutoFitColumns();

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DanhSachThietBi_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
        
        // GET: ThietBi/Statistics
        public IActionResult Statistics()
        {
            return View();
        }
        // GET: ThietBi/DeviceStatistics
        public async Task<IActionResult> DeviceStatistics()
        {
            var data = await _context.TbThietBis
                .GroupBy(t => t.IdLoaiThietBiNavigation.LoaiThietBi)
                .Select(g => new
                {
                    LoaiThietBi = g.Key,
                    SoLuong = g.Count()
                })
                .ToListAsync();

            return Json(data);
        }

        // GET: ThietBi/ByOwner/5
        public async Task<IActionResult> ByOwner(int? idNguoiSoHuu)
        {
            if (idNguoiSoHuu == null)
            {
                return NotFound("ID Người Sở Hữu không được cung cấp.");
            }

            // Lấy danh sách thiết bị thuộc về người sở hữu
            var devices = await _context.TbThietBis
                .Where(t => t.IdNguoiSoHuu == idNguoiSoHuu)
                .Include(t => t.IdLoaiThietBiNavigation)
                .Include(t => t.IdTrangThaiThietBiNavigation)
                .ToListAsync();

            if (devices == null || !devices.Any())
            {
                return NotFound($"Không tìm thấy thiết bị nào cho ID Người Sở Hữu {idNguoiSoHuu}.");
            }

            // Trả về View hiển thị danh sách thiết bị
            ViewData["IdNguoiSoHuu"] = idNguoiSoHuu;
            return View(devices);
        }

        public async Task<IActionResult> ByGroup(int? idDonViSoHuu)
        {
            if (idDonViSoHuu == null)
            {
                return NotFound("ID Người Sở Hữu không được cung cấp.");
            }

            // Lấy danh sách thiết bị thuộc về người sở hữu
            var device = await _context.TbThietBis
                .Where(t => t.IdDonViSoHuu == idDonViSoHuu)
                .Include(t => t.IdLoaiThietBiNavigation)
                .Include(t => t.IdTrangThaiThietBiNavigation)
                .ToListAsync();

            if (device == null || !device.Any())
            {
                return NotFound($"Không tìm thấy thiết bị nào cho ID Đơn Vị Sở Hữu {idDonViSoHuu}.");
            }

            // Trả về View hiển thị danh sách thiết bị
            ViewData["IdDonViSoHuu"] = idDonViSoHuu;
            return View(device);
        }
        public IActionResult TraCuu()
        {
            return View();
        }

        private bool TbThietBiExists(int id)
        {
            return _context.TbThietBis.Any(e => e.IdThietBi == id);
        }
    }
}
