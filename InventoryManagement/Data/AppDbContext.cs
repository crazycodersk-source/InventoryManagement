using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Warehouse> Warehouses { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("data source=IBM-PF255106; initial catalog=InventoryDB;integrated security=True;TrustServerCertificate=true");
		}

		public AppDbContext(DbContextOptions<AppDbContext> options)
	   : base(options)
		{
			
		}
	}
}
