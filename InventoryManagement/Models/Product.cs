using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }
		[Required]
		[Column(TypeName = "varchar(100)")] 
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		[ForeignKey("Warehouse")]
		public int WarehouseId { get; set; }
		public Warehouse Warehouse { get; set; }
	}
}
