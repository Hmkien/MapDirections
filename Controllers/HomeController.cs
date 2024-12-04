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
        var model = await _context.Mall.Include(e => e.MallServices).ToListAsync();
        return View(model);
    }


}
