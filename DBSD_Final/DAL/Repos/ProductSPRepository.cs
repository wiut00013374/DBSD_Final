using DBSD_Final.DAL.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DBSD_Final.DAL.Repos
{
    public class ProductSPRepository : IProductRepository
    {
        private readonly string _connStr;
        public ProductSPRepository(string _connStr)
        {
            this._connStr = _connStr;
        }
        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "spDeleteProduct";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", id);
            conn.Open();
            int numberOfActuallyUpdated = cmd.ExecuteNonQuery();
            if (numberOfActuallyUpdated == 0)
                throw new Exception($"Product does not exist! {id}");
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            var list = new List<Product>();
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "spGetAllProducts";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await conn.OpenAsync();
            using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                var product = await MapDbDataReaderToProductAsync(rdr);
                list.Add(product);
            }
            return list;
        }

        public Product? GetById(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "spGetProductsByID";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", id);

            conn.Open();
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                return MapDbDataReaderToProduct(rdr);
            }
            return null;
        }

        public int Insert(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "spInsertProduct";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            conn.Open();
            int id = (int)cmd.ExecuteScalar();
            product.ProductID = id;
            return id;
        }

        public void Update(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "spUpdateProduct";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            conn.Open();
            int numberOfActuallyUpdated = cmd.ExecuteNonQuery();
            if (numberOfActuallyUpdated == 0)
                throw new Exception($"Product does not exist! {product.ProductID}");
        }
        private async Task<Product> MapDbDataReaderToProductAsync(DbDataReader rdr)
        {
            return new Product()
            {
                ProductID = await rdr.GetFieldValueAsync<int>("ProductID"),
                ProductName = await rdr.GetFieldValueAsync<string>("ProductName"),
                ProductPrice = await rdr.GetFieldValueAsync<double>("ProductPrice"),

            };
        }
        public Product MapDbDataReaderToProduct(DbDataReader rdr)
        {
            return new Product()
            {
                ProductID = rdr.GetInt32("ProductID"),
                ProductName = rdr.GetString("ProductName"),
                ProductPrice = rdr.GetDouble("ProductPrice")
            };
        }
    }
}
