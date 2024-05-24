using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Licenta.Models;

namespace Licenta.Data
{
    public class LicentaContext : DbContext
    {
        public LicentaContext (DbContextOptions<LicentaContext> options)
            : base(options)
        {
        }

        public DbSet<Licenta.Models.Dish> Dish { get; set; } = default!;

        public DbSet<Licenta.Models.Category> Category { get; set; } = default!;

        public DbSet<Licenta.Models.Ingredient> Ingredient { get; set; } = default!;

        public DbSet<Licenta.Models.Allergen> Allergen { get; set; } = default!;
    }
}
