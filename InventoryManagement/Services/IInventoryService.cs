using InventoryManagement.DTOs;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public interface IInventoryService
	{
		List<ProductDTO> GetAll();
		void Update(Product product);
	}
}
