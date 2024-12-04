using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapDirections.Data;
using MapDirections.Models;
using Microsoft.AspNetCore.Authorization;
using BaiThucHanh.Models.Process;
using OfficeOpenXml;
using X.PagedList.Extensions;

namespace MapDirections.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly MapContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public AdminController(MapContext context)
        {
            _context = context;
        }

        // GET: Mall
        public async Task<IActionResult> Index(int? page)
        {
            var model = _context.Mall.ToList().ToPagedList(page ?? 1, 15);
            return View(model);
        }
        public IActionResult CreateMall()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMall([Bind("MallId,Name,Address,Description,OpeningHours,Phone,Website,Latitude,Longitude")] Mall mall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mall);
        }

        public async Task<IActionResult> EditMall(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mall = await _context.Mall.FindAsync(id);
            if (mall == null)
            {
                return NotFound();
            }
            return View(mall);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMall(int id, [Bind("MallId,Name,Address,Description,OpeningHours,Phone,Website,Latitude,Longitude")] Mall mall)
        {
            if (id != mall.MallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MallExists(mall.MallId))
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
            return View(mall);
        }
        public async Task<IActionResult> DetailMall(int id)
        {


            var mall = await _context.Mall
                .FirstOrDefaultAsync(m => m.MallId == id);
            if (mall == null)
            {
                return NotFound();
            }


            return View(mall);
        }

        public async Task<IActionResult> DeleteMall(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mall = await _context.Mall
                .FirstOrDefaultAsync(m => m.MallId == id);
            if (mall == null)
            {
                return NotFound();
            }

            return View(mall);
        }

        [HttpPost, ActionName("DeleteMall")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMallConfirmed(int id)
        {
            var mall = await _context.Mall.FindAsync(id);
            if (mall != null)
            {
                _context.Mall.Remove(mall);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MallExists(int id)
        {
            return _context.Mall.Any(e => e.MallId == id);
        }
        public async Task<IActionResult> Service()
        {
            var mapContext = _context.MMallService.Include(m => m.Mall);
            return View(await mapContext.ToListAsync());
        }

        public IActionResult CreateService()
        {
            ViewData["MallId"] = new SelectList(_context.Mall, "MallId", "Name");
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService([Bind("FacilityId,Name,Description,MallId,Event")] MallService mallService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mallService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Service));
            }
            ViewData["MallId"] = new SelectList(_context.Mall, "MallId", "Name", mallService.MallId);
            return View(mallService);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> EditService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mallService = await _context.MMallService.FindAsync(id);
            if (mallService == null)
            {
                return NotFound();
            }
            ViewData["MallId"] = new SelectList(_context.Mall, "MallId", "Name", mallService.MallId);
            return View(mallService);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, [Bind("FacilityId,Name,Description,MallId,Event")] MallService mallService)
        {
            if (id != mallService.FacilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mallService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MallServiceExists(mallService.FacilityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Service));
            }
            ViewData["MallId"] = new SelectList(_context.Mall, "MallId", "Name", mallService.MallId);
            return View(mallService);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> DeleteService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mallService = await _context.MMallService
                .Include(m => m.Mall)
                .FirstOrDefaultAsync(m => m.FacilityId == id);
            if (mallService == null)
            {
                return NotFound();
            }

            return View(mallService);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("DeleteService")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteServiceConfirmed(int id)
        {
            var mallService = await _context.MMallService.FindAsync(id);
            if (mallService != null)
            {
                _context.MMallService.Remove(mallService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Service));
        }

        private bool MallServiceExists(int id)
        {
            return _context.MMallService.Any(e => e.FacilityId == id);
        }
        public async Task<IActionResult> UserManagers()
        {
            return View(await _context.Users.ToListAsync());
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserManagers));
        }

        private bool AppUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required and cannot be empty.");
            }

            var fileExtention = Path.GetExtension(file.FileName).ToLower();
            if (fileExtention != ".xlsx" && fileExtention != ".xls")
            {
                return BadRequest("Invalid file type. Only .xlsx and .xls files are allowed.");
            }

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var filePath = Path.Combine(uploadFolder, file.FileName);

            try
            {
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var dt = _excelProcess.ExcelToDataTable(filePath);

                if (dt.Columns.Count != 8)
                {
                    return BadRequest("Excel file format is incorrect. Make sure it has 8 columns.");
                }

                var mallsToAdd = new List<Mall>();
                var existingNames = await _context.Mall.Select(e => e.Name).ToListAsync();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var name = dt.Rows[i][0]?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(name) ||
                        !double.TryParse(dt.Rows[i][1]?.ToString(), out var latitude) ||
                        !double.TryParse(dt.Rows[i][2]?.ToString(), out var longitude))
                    {
                        return BadRequest($"Invalid data at row {i + 1}: Name, Latitude, or Longitude is missing or invalid.");
                    }

                    if (existingNames.Contains(name))
                    {
                        continue;
                    }

                    mallsToAdd.Add(new Mall
                    {
                        Name = name,
                        Latitude = latitude,
                        Longitude = longitude,
                        Address = dt.Rows[i][3]?.ToString()?.Trim(),
                        Description = dt.Rows[i][4]?.ToString()?.Trim(),
                        OpeningHours = dt.Rows[i][5]?.ToString()?.Trim(),
                        Phone = dt.Rows[i][6]?.ToString()?.Trim(),
                        Website = dt.Rows[i][7]?.ToString()?.Trim(),
                    });
                }

                if (mallsToAdd.Any())
                {
                    await _context.Mall.AddRangeAsync(mallsToAdd);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing the file: {ex.Message}");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAll()
        {
            var model = await _context.Mall.ToListAsync();
            _context.RemoveRange(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
