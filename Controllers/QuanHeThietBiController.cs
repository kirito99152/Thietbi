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
    public class QuanHeThietBiController : Controller
    {
        private readonly ThietBiContext _context;

        public QuanHeThietBiController(ThietBiContext context)
        {
            _context = context;
        }

        // GET: QuanHeThietBi
        public async Task<IActionResult> Index()
        {
            var thietBiContext = _context.TbQuanHeThietBis.Include(t => t.IdThietBiChaNavigation).Include(t => t.IdThietBiConNavigation);
            return View(await thietBiContext.ToListAsync());
        }

        // GET: QuanHeThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis
                .Include(t => t.IdThietBiChaNavigation)
                .Include(t => t.IdThietBiConNavigation)
                .FirstOrDefaultAsync(m => m.IdQuanHeThietBi == id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }

            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Create
        public IActionResult Create()
        {
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi");
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi");
            return View();
        }

        // POST: QuanHeThietBi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuanHeThietBi,IdThietBiCha,IdThietBiCon")] TbQuanHeThietBi tbQuanHeThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbQuanHeThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis.FindAsync(id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "TenThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // POST: QuanHeThietBi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuanHeThietBi,IdThietBiCha,IdThietBiCon")] TbQuanHeThietBi tbQuanHeThietBi)
        {
            if (id != tbQuanHeThietBi.IdQuanHeThietBi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbQuanHeThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbQuanHeThietBiExists(tbQuanHeThietBi.IdQuanHeThietBi))
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
            ViewData["IdThietBiCha"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCha);
            ViewData["IdThietBiCon"] = new SelectList(_context.TbThietBis, "IdThietBi", "IdThietBi", tbQuanHeThietBi.IdThietBiCon);
            return View(tbQuanHeThietBi);
        }

        // GET: QuanHeThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbQuanHeThietBi = await _context.TbQuanHeThietBis
                .Include(t => t.IdThietBiChaNavigation)
                .Include(t => t.IdThietBiConNavigation)
                .FirstOrDefaultAsync(m => m.IdQuanHeThietBi == id);
            if (tbQuanHeThietBi == null)
            {
                return NotFound();
            }

            return View(tbQuanHeThietBi);
        }

        // POST: QuanHeThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbQuanHeThietBi = await _context.TbQuanHeThietBis.FindAsync(id);
            if (tbQuanHeThietBi != null)
            {
                _context.TbQuanHeThietBis.Remove(tbQuanHeThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ database
            List<TbQuanHeThietBi> data = await _context.TbQuanHeThietBis.Include(t => t.IdThietBiChaNavigation).Include(t => t.IdThietBiConNavigation)
                .ToListAsync();

            // Tạo một file Excel với EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách quan hệ thiết bị");

                // Hợp nhất và đặt tiêu đề lớn
                worksheet.Cells[1, 1, 1, 3].Merge = true;
                worksheet.Cells[1, 1].Value = "Báo cáo danh sách quan hệ thiết bị";
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Tiêu đề bảng
                worksheet.Cells[2, 1].Value = "STT";
                worksheet.Cells[2, 2].Value = "THIẾT BỊ CHA";
                worksheet.Cells[2, 3].Value = "ITHIẾT BỊ CON";
            

                // Định dạng tiêu đề
                using (var range = worksheet.Cells[2, 1, 2, 3])
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
                    worksheet.Cells[row, 1].Value = item.IdQuanHeThietBi;
                    worksheet.Cells[row, 2].Value = item.IdThietBiChaNavigation?.TenThietBi;
                    worksheet.Cells[row, 3].Value = item.IdThietBiConNavigation?.TenThietBi;
                    row++;
                }

                // Thêm viền cho toàn bộ bảng
                worksheet.Cells[2, 1, row - 1, 3].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells[2, 1, row - 1, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                worksheet.Cells.AutoFitColumns();

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"DanhSachQuanHeThietBi_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            var chartData = await _context.TbQuanHeThietBis
                .GroupBy(t => t.IdThietBiChaNavigation.TenThietBi)
                .Select(g => new
                {
                    LoaiThietBi = g.Key,
                    SoLuong = g.Count()
                }).ToListAsync();

            return Json(chartData);
        }
        private bool TbQuanHeThietBiExists(int id)
        {
            return _context.TbQuanHeThietBis.Any(e => e.IdQuanHeThietBi == id);
        }
    }
}
