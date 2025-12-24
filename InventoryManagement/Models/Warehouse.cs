namespace InventoryManagement.Models
{
	public class Warehouse
	{
		public int WarehouseId { get; set; }
		public string Location { get; set; }

	
		public ICollection<Product> Products { get; set; }
	}
}
