using ClosedXML.Excel;
using InventoryManagement.DTOs.InventoryManagement.DTOs;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
	[EnableCors("AllowAll")]
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class InventoryController : ControllerBase
	{
		IInventoryService _iInventoryService;
		public InventoryController(IInventoryService iInventoryService)
		{
			_iInventoryService = iInventoryService;
		}

		/// <summary>
		/// Retrieve current inventory levels for all products across all warehouses.
		/// </summary>
		[Authorize(Roles = "WarehouseManager,WarehouseOperator")]
		[HttpGet]
		public async Task<IActionResult> GetAllInventory()
		{
			var inventory = _iInventoryService.GetAll();
			return Ok(inventory);
		}

        ///// <summary>
        ///// Transfer a product between warehouses.
        ///// </summary>
        //[Authorize(Roles = "WarehouseManager")]
        //[HttpPost("transfer")]
        //public async Task<IActionResult> TransferProduct([FromBody] Product product)
        //{
        //	try
        //	{
        //		_iInventoryService.Update(product);
        //		return Ok(product);
        //	}
        //	catch (Exception ex)
        //	{
        //		return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //	}
        //}

        [Authorize(Roles = "WarehouseManager")]
        [HttpPost("transfer")]
        public IActionResult TransferProduct([FromBody] TransferProductDto dto)
        {
            try
            {
                // 1) Load the existing product
                var product = _iInventoryService.GetById(dto.ProductId);
                if (product == null)
                    return NotFound($"Product {dto.ProductId} not found.");

                // 2) Update ONLY the destination warehouse
                product.WarehouseId = dto.WarehouseId;

                // 3) Persist
                _iInventoryService.Update(product);

                // 4) Return just a flag (no product payload)
                return Ok(new { success = true });
                // If you prefer a bare boolean: return Ok(true);
            }
            catch (Exception ex)
            {
                // Return a failure flag (still 500 status)
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, error = ex.Message });
            }
        }

        /// <summary>
        /// Generate a stock report.
        /// </summary>
        [Authorize(Roles = "WarehouseManager")]
		[HttpGet]
		public async Task<IActionResult> GetInventoryReport()
		{
			// 1. Retrieve your data (from database, service, etc.)
			var inventories = _iInventoryService.GetAll();

			// 2. Generate the Excel file in a MemoryStream
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Inventories");
				worksheet.Cell(1, 1).InsertTable(inventories); // Insert data with headers

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();

					// 3. Return the file
					var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
					var fileName = "Inventories.xlsx";
					return File(content, contentType, fileName);
				}
			}
		}
	}
}
