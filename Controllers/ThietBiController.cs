﻿using System;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFromExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn file Excel để tải lên.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        // Lấy sheet đầu tiên
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            TempData["ErrorMessage"] = "File Excel không hợp lệ.";
                            return RedirectToAction(nameof(Index));
                        }

                        // Đọc dữ liệu từ hàng thứ 2 (bỏ qua tiêu đề)
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            if (worksheet.Cells[row, 1].Value == null) continue; // Bỏ qua hàng rỗng

                            try
                            {
                                var thietBi = new TbThietBi
                                {
                                    IdThietBi = int.Parse(worksheet.Cells[row, 1].Text),
                                    IdNguoiSoHuu = int.Parse(worksheet.Cells[row, 2].Text),
                                    IdDonViSoHuu = int.Parse(worksheet.Cells[row, 3].Text),
                                    NgayThemThietBi = DateTime.TryParse(worksheet.Cells[row, 4].Text, out var ngayThem) ? ngayThem : DateTime.Now,
                                    TenThietBi = worksheet.Cells[row, 5].Text,
                                    MoTa = worksheet.Cells[row, 6].Text,
                                    MaThietBiHv = worksheet.Cells[row, 7].Text,
                                    MaThietBiNhaSx = worksheet.Cells[row, 8].Text,
                                    CauHinh = worksheet.Cells[row, 9].Text,
                                    ViTriDat = worksheet.Cells[row, 10].Text,
                                    IdLoaiThietBi = int.Parse(worksheet.Cells[row, 11].Text),
                                    IdTrangThaiThietBi = int.Parse(worksheet.Cells[row, 12].Text)
                                };

                                // Kiểm tra trùng mã thiết bị
                                if (!_context.TbThietBis.Any(e => e.MaThietBiHv == thietBi.MaThietBiHv))
                                {
                                    _context.TbThietBis.Add(thietBi);
                                }
                            }
                            catch (Exception ex)
                            {
                                TempData["ErrorMessage"] = $"Lỗi tại dòng {row}: {ex.Message}";
                                return RedirectToAction(nameof(Index));
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                }

                TempData["SuccessMessage"] = "Import dữ liệu từ file Excel thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra khi import file Excel: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
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


        private bool TbThietBiExists(int id)
        {
            return _context.TbThietBis.Any(e => e.IdThietBi == id);
        }
    }
}
