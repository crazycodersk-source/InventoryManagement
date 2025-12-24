using InventoryManagement.DTOs;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public interface IInventoryRepository
	{
		List<Product> GetAll();
		void Update(Product product);
		List<Warehouse> GetAllWarehouses();
        Product? GetById(int productId);
    }
}
