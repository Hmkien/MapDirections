using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MapDirections.Models;
using Microsoft.AspNetCore.Authorization;
using MapDirections.Data;
using Microsoft.EntityFrameworkCore;

namespace MapDirections.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly MapContext _context;

    public HomeController(MapContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var model = await _context.Mall
                                  .Include(e => e.MallServices)
                                  .Select(m => new MallViewModel
                                  {
                                      Name = m.Name,
                                      Latitude = m.Latitude,
                                      Longitude = m.Longitude,
                                      Address = m.Address,
                                      Description = m.Description,
                                      OpeningHours = m.OpeningHours,
                                      Phone = m.Phone,
                                      Website = m.Website,
                                      Facilities = m.MallServices
                                          .Select(s => $"{s.Name} ({s.Event})")
                                          .ToList()
                                  })
                                  .ToListAsync();

        return View(model);
    }



}
