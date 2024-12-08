using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapDirections.Data;
using MapDirections.Models;

namespace MapDirections.Controllers
{
    public class RoleController : Controller
    {
        private readonly MapContext _context;

        public RoleController(MapContext context)
        {
            _context = context;
        }


    }
}
