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
    public class LichSuSuaChuaController : Controller
    {
        private readonly ThietBiContext _context;

        public LichSuSuaChuaController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: LichSuSuaChua
        public async Task<IActionResult> Index()
        {
            var thietBiContext = _context.TbLichSuSuaChuas.Include(t => t.IdThietBiNavigation);
            return View(await thietBiContext.ToListAsync());
        }

        // GET: LichSuSuaChua/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuSuaChua = await _context.TbLichSuSuaChuas
                .Include(t => t.IdThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdLichSuSuaChua == id);
            if (tbLichSuSuaChua == null)
            {
                return NotFound();
            }

            return View(tbLichSuSuaChua);
        }

        // GET: LichSuSuaChua/Create
        public IActionResult Create()
        {
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi");
            return View();
        }

        // POST: LichSuSuaChua/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: LichSuSuaChua/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLichSuSuaChua,IdThietBi,IdNguoiBao,IdDonViBao,IdCanBoSua,IdDonViSua,ThoiGianBatDau,ThoiGianKetThuc,HienTuong,NguyenNhanXacDinh,KetQuaSua,ThongTinSuaChua,CachSua")] TbLichSuSuaChua tbLichSuSuaChua)
        {
            try
            {
                

               

                // Kiểm tra nếu Model hợp lệ
                if (ModelState.IsValid)
                {
                    _context.Add(tbLichSuSuaChua);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbLichSuSuaChua.IdThietBi);
                return View(tbLichSuSuaChua);
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu có ngoại lệ xảy ra
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbLichSuSuaChua.IdThietBi);
                return View(tbLichSuSuaChua);
            }
        }




        // GET: LichSuSuaChua/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuSuaChua = await _context.TbLichSuSuaChuas.FindAsync(id);
            if (tbLichSuSuaChua == null)
            {
                return NotFound();
            }
            ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbLichSuSuaChua.IdThietBi);
            return View(tbLichSuSuaChua);
        }

        // POST: LichSuSuaChua/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: LichSuSuaChua/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLichSuSuaChua,IdThietBi,IdNguoiBao,IdDonViBao,IdCanBoSua,IdDonViSua,ThoiGianBatDau,ThoiGianKetThuc,HienTuong,NguyenNhanXacDinh,KetQuaSua,ThongTinSuaChua,CachSua")] TbLichSuSuaChua tbLichSuSuaChua)
        {
            if (id != tbLichSuSuaChua.IdLichSuSuaChua)
            {
                return NotFound();
            }

            try
            {
               

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tbLichSuSuaChua);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbLichSuSuaChuaExists(tbLichSuSuaChua.IdLichSuSuaChua))
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

                ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbLichSuSuaChua.IdThietBi);
                return View(tbLichSuSuaChua);
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu có ngoại lệ xảy ra
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                ViewData["IdThietBi"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbLichSuSuaChua.IdThietBi);
                return View(tbLichSuSuaChua);
            }
        }




        // GET: LichSuSuaChua/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLichSuSuaChua = await _context.TbLichSuSuaChuas
                .Include(t => t.IdThietBiNavigation)
                .FirstOrDefaultAsync(m => m.IdLichSuSuaChua == id);
            if (tbLichSuSuaChua == null)
            {
                return NotFound();
            }

            return View(tbLichSuSuaChua);
        }

        // POST: LichSuSuaChua/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbLichSuSuaChua = await _context.TbLichSuSuaChuas.FindAsync(id);
            if (tbLichSuSuaChua != null)
            {
                _context.TbLichSuSuaChuas.Remove(tbLichSuSuaChua);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChartData()
        {
            try
            {
                // Lấy dữ liệu sửa chữa từ DbContext
                var data = await _context.TbLichSuSuaChuas
                    .Include(t => t.IdThietBiNavigation) // Bao gồm bảng liên kết thiết bị
                    .ToListAsync();

                // Nhóm dữ liệu theo tên thiết bị và đếm số lượng sửa chữa
                var chartData = data.GroupBy(x => x.IdThietBiNavigation.TenThietBi)
                    .Select(g => new
                    {
                        Label = g.Key,  // Tên thiết bị
                        Count = g.Count() // Số lần sửa chữa
                    }).ToList();

                // Trả về dữ liệu JSON cho biểu đồ
                return Json(new
                {
                    labels = chartData.Select(x => x.Label).ToArray(),
                    values = chartData.Select(x => x.Count).ToArray()
                });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                return BadRequest(new { error = ex.Message });
            }
        }

        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ database
            List<TbLichSuSuaChua> data = await _context.TbLichSuSuaChuas.Include(t => t.IdThietBiNavigation)
                .ToListAsync();

            // Tạo một file Excel với EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách lịch sử sửa chữa");

                // Hợp nhất và đặt tiêu đề lớn
                worksheet.Cells[1, 1, 1, 12].Merge = true;
                worksheet.Cells[1, 1].Value = "Báo cáo danh sách lịch sử sửa chữa";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Tiêu đề bảng
                worksheet.Cells[2, 1].Value = "ID NGƯỜI BÁO";
                worksheet.Cells[2, 2].Value = "ID ĐƠN VỊ BÁO";
                worksheet.Cells[2, 3].Value = "ID CÁN BỘ SỬA CHỮA";
                worksheet.Cells[2, 4].Value = "ID ĐƠN VỊ SỬA CHỮA";
                worksheet.Cells[2, 5].Value = "THỜI GIAN BẮT ĐẦU";
                worksheet.Cells[2, 6].Value = "THỜI GIAN KẾT THÚC";
                worksheet.Cells[2, 7].Value = "HIỆN TƯỢNG";
                worksheet.Cells[2, 8].Value = "NGUYÊN NHÂN XÁC ĐỊNH";
                worksheet.Cells[2, 9].Value = "KẾT QUẢ SỬA CHỮA";
                worksheet.Cells[2, 10].Value = "THÔNG TIN SỬA CHỮA";
                worksheet.Cells[2, 11].Value = "CÁCH SỬA";
                worksheet.Cells[2, 12].Value = "THIẾT BỊ";

                // Định dạng tiêu đề
                using (var range = worksheet.Cells[2, 1, 2, 8])
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
                    worksheet.Cells[row, 1].Value = item.IdNguoiBao;
                    worksheet.Cells[row, 2].Value = item.IdDonViBao;
                    worksheet.Cells[row, 3].Value = item.IdCanBoSua;
                    worksheet.Cells[row, 4].Value = item.IdDonViSua;
                    worksheet.Cells[row, 5].Value = item.ThoiGianBatDau;
                    worksheet.Cells[row, 6].Value = item.ThoiGianKetThuc;
                    worksheet.Cells[row, 7].Value = item.HienTuong;
                    worksheet.Cells[row, 8].Value = item.NguyenNhanXacDinh;
                    worksheet.Cells[row, 9].Value = item.KetQuaSua;
                    worksheet.Cells[row, 10].Value = item.ThongTinSuaChua;
                    worksheet.Cells[row, 11].Value = item.CachSua;
                    worksheet.Cells[row, 12].Value = item.IdThietBiNavigation?.TenThietBi;
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

                string excelName = $"DanhSachLichSuSuaChua_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
        public IActionResult Chart()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SuccessRateChartData()
        {
            try
            {
                // Lấy dữ liệu sửa chữa từ DbContext
                var data = await _context.TbLichSuSuaChuas.ToListAsync();

                // Tính toán tỷ lệ sửa thành công và không thành công
                var successCount = data.Count(x => x.KetQuaSua == "Thành Công ");
                var failCount = data.Count(x => x.KetQuaSua != "Thành Công ");

                return Json(new
                {
                    labels = new[] { "Thành Công", "Không Sửa Được" },
                    values = new[] { successCount, failCount }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        public IActionResult RepairChart()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> RepairCountByMonthChartData()
        {
            try
            {
                // Lấy dữ liệu sửa chữa từ DbContext
                var data = await _context.TbLichSuSuaChuas.ToListAsync();

                // Nhóm dữ liệu theo tháng và năm từ trường ThoiGianBatDau
                var chartData = data
                    .Where(x => x.ThoiGianBatDau.HasValue)  // Kiểm tra trường thời gian bắt đầu có giá trị
                    .GroupBy(x => new
                    {
                        Year = x.ThoiGianBatDau.Value.Year,
                        Month = x.ThoiGianBatDau.Value.Month
                    })
                    .Select(g => new
                    {
                        YearMonth = $"{g.Key.Month}/{g.Key.Year}",
                        Count = g.Count()
                    })
                    .OrderBy(g => g.YearMonth)
                    .ToList();

                // Trả về dữ liệu JSON cho biểu đồ
                return Json(new
                {
                    labels = chartData.Select(x => x.YearMonth).ToArray(),
                    values = chartData.Select(x => x.Count).ToArray()
                });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return BadRequest(new { error = ex.Message });
            }
        }
        // GET: LichSuSuaChua/TraCuu
        public IActionResult TraCuu()
        {
            return View();
        }
        
        public async Task<IActionResult>  LSSC(int? idThietBi)
        {
            if (idThietBi == null)
            {
                return NotFound("ID Người Sở Hữu không hợp lệ.");
            }

            // Lấy danh sách lịch sử sửa chữa theo ID thiết bị
            var lichSuSuaChuas = await _context.TbLichSuSuaChuas
                .Include(t => t.IdThietBiNavigation)
                .Where(t => t.IdThietBi == idThietBi)
                .ToListAsync();

            if (lichSuSuaChuas == null || !lichSuSuaChuas.Any())
            {
                return NotFound("Không tìm thấy lịch sử của thiết này");
            }

            // Trả về View hiển thị danh sách thiết bị
            ViewData["IdThietBi"] = idThietBi;
            return View(lichSuSuaChuas);
        }

        private bool TbLichSuSuaChuaExists(int id)
        {
            return _context.TbLichSuSuaChuas.Any(e => e.IdLichSuSuaChua == id);
        }
    }
}
