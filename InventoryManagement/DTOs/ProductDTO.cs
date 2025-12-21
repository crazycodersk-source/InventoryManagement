namespace InventoryManagement.DTOs
{
	public class ProductDTO
	{
		public int ProductId { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public int Stock { get; set; }

		public int WarehouseId { get; set; }
		public string Location { get; set; }

	}
}
