using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using APBD6.Models;
using WebApplication6.Models;

namespace Task6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly string connectionString = "Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;User Id=s28320;Password=*****;";

        [HttpPost]
        public IActionResult AddProductToWarehouse([FromBody] ProductWarehouseRequest request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("Amount has to be greater than zero");
            }
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    string productQuery = "SELECT 1 FROM Product WHERE IdProduct = @IdProduct";
                    var productCmd = new SqlCommand(productQuery, connection, transaction);
                    productCmd.Parameters.AddWithValue("@IdProduct", request.ProductId);
                    if (productCmd.ExecuteScalar() == null)
                    {
                        transaction.Rollback();
                        return NotFound("Product wasn\'t found");
                    }

                    
                    string warehouseQuery = "SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
                    var warehouseCmd = new SqlCommand(warehouseQuery, connection, transaction);
                    warehouseCmd.Parameters.AddWithValue("@IdWarehouse", request.WarehouseId);
                    if (warehouseCmd.ExecuteScalar() == null)
                    {
                        transaction.Rollback();
                        return NotFound("Warehouse wasn\'t found");
                    }

                    
                    string orderQuery = @"SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount 
                              AND CreatedAt < @CreatedAt AND IdOrder NOT IN (SELECT IdOrder FROM Product_Warehouse)";
                    var orderCmd = new SqlCommand(orderQuery, connection, transaction);
                    orderCmd.Parameters.AddWithValue("@IdProduct", request.ProductId);
                    orderCmd.Parameters.AddWithValue("@Amount", request.Amount);
                    orderCmd.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

                    
                    using (var reader = orderCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            transaction.Rollback();
                            return BadRequest("Order wasn\'t found");
                        }

                        int orderId = reader.GetInt32(0);
                        reader.Close();
                        
                        decimal price;
                        string priceQuery = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
                        var priceCmd = new SqlCommand(priceQuery, connection, transaction);
                        priceCmd.Parameters.AddWithValue("@IdProduct", request.ProductId);
                        using (var reader1 = priceCmd.ExecuteReader())
                        {
                            if (!reader1.Read())
                            {
                                transaction.Rollback();
                                return BadRequest("Product price wasn\'t found");
                            }

                            price = reader1.GetDecimal(0);
                            reader1.Close();
                        }
                        
                        decimal totalCost = price * request.Amount;
                        
                        string updateOrderQuery = "UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";
                        var updateCmd = new SqlCommand(updateOrderQuery, connection, transaction);
                        updateCmd.Parameters.AddWithValue("@IdOrder", orderId);
                        updateCmd.ExecuteNonQuery();

                        string insertQuery = @"INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, IdOrder, Amount, Price, CreatedAt)
                            VALUES (@IdProduct, @IdWarehouse, @IdOrder, @Amount, @Price, GETDATE()); SELECT SCOPE_IDENTITY();";
                        var insertCmd = new SqlCommand(insertQuery, connection, transaction);
                        insertCmd.Parameters.AddWithValue("@IdProduct", request.ProductId);
                        insertCmd.Parameters.AddWithValue("@IdWarehouse", request.WarehouseId);
                        insertCmd.Parameters.AddWithValue("@IdOrder", orderId);
                        insertCmd.Parameters.AddWithValue("@Amount", request.Amount);
                        insertCmd.Parameters.AddWithValue("@Price", totalCost);

                        int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());

                        transaction.Commit();
                        return Ok(insertedId);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"An error occured: {ex.Message}");
                }
            }
        }

    }
}