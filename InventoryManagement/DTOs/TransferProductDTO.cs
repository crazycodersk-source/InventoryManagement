namespace InventoryManagement.DTOs
{

    namespace InventoryManagement.DTOs
    {
        /// <summary>
        /// Minimal payload for warehouse transfer.
        /// </summary>
        public class TransferProductDto
        {
            public int ProductId { get; set; }
            public int WarehouseId { get; set; } // destination warehouse
        }
    }

}
