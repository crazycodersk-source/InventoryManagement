using InventoryManagement.Data;
using InventoryManagement.DTOs;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class InventoryRepository : IInventoryRepository
	{
		private AppDbContext _db;
		public InventoryRepository(AppDbContext db)
		{
			_db = db;
		}
		public List<Product> GetAll()
		{
			return _db.Products.ToList();
		}
		  
		public List<Warehouse> GetAllWarehouses()
		{
			return _db.Warehouses.ToList();
		}

		public void Update(Product product)
		{
			_db.Products.Update(product);
			_db.SaveChanges();
		}
	}
}
