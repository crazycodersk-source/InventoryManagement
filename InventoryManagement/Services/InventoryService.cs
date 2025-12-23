
using InventoryManagement.DTOs;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public List<ProductDTO> GetAll()
        {
            var products = _inventoryRepository.GetAll();
            var warehouses = _inventoryRepository.GetAllWarehouses();

            // LINQ join products with warehouses to enrich DTO
            var inventoryInfo =
                from prd in products
                join ware in warehouses on prd.WarehouseId equals ware.WarehouseId
                select new ProductDTO
                {
                    ProductId = prd.ProductId,
                    Name = prd.Name,
                    Price = prd.Price,
                    Stock = prd.Stock,
                    Location = ware.Location,
                    WarehouseId = ware.WarehouseId
                };

            return inventoryInfo.ToList();
        }

        public List<Warehouse> GetAllWarehouses()
        {
            return _inventoryRepository.GetAllWarehouses();
        }

        public void Update(Product product)
        {
            _inventoryRepository.Update(product);
        }
    }
}

