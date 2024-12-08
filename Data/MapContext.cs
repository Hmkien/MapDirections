using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MapDirections.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MapDirections.Data
{
    public class MapContext : IdentityDbContext<AppUser>
    {
        public MapContext(DbContextOptions<MapContext> options)
            : base(options)
        {
        }
        public DbSet<AppRole> AppRole { get; set; } = default!;
        public DbSet<Mall> Mall { get; set; } = default!;
        public DbSet<MallService> MMallService { get; set; } = default!;

    }
}
