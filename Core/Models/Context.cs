using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<CompraGado> CompraGado { get; set; }
        public virtual DbSet<CompraGadoItem> CompraGadoItem { get; set; }
        public virtual DbSet<Pecuarista> Pecuarista { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Data Source=I5-PC-WIN10\\SQL2019;Initial Catalog=Marfrig;User ID=test.Marfrig;Password=Marfrig00;Integrated Security=True;");
            }
        }
    }
}
