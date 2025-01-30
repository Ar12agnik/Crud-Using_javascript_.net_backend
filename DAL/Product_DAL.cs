using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Crud_Using_javascript.Models;

namespace Crud_Using_javascript.DAL
{
    public class Product_DAL
    {
        string constring =ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        public List<Products> GetProducts()
        {
            List<Products> _products = new List<Products>();
            using (SqlConnection conn = new SqlConnection(constring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetAllProducts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                conn.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    _products.Add(new Products
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return _products;
        }
        public bool deleteItem(int id)
        {
            int del = 0;
            using (SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                // call the store procedure created
                cmd.CommandText = "deleteItem";
                cmd.Parameters.AddWithValue("@ProductId", id);
                conn.Open();
                del = cmd.ExecuteNonQuery();
                conn.Close();

            }
            return del > 0;
        }
        public bool Insert_prod(Products _product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_createandupdate";
                cmd.Parameters.AddWithValue("@ProductName", _product.ProductName);
                cmd.Parameters.AddWithValue("@Price", _product.Price);
                cmd.Parameters.AddWithValue("@Qty", _product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", _product.Remarks);
                conn.Open();
                id = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return id > 0;
        }
        public List<Products> getProdById(int id)
        {
            List<Products> _products = new List<Products>();
            using (SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                // call the store procedure created
                cmd.CommandText = "sp_getItemById";
                cmd.Parameters.AddWithValue("@ProductId", id);
                SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conn.Open();
                //get the data in dt
                SqlDa.Fill(dt);
                conn.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    _products.Add(new Products
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()

                    });
                }
                return _products;
            }
        }
        public bool Update_prod(Products _product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_createandupdate";
                cmd.Parameters.AddWithValue("@ProductId", _product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", _product.ProductName);
                cmd.Parameters.AddWithValue("@Price", _product.Price);
                cmd.Parameters.AddWithValue("@Qty", _product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", _product.Remarks);
                conn.Open();
                id = cmd.ExecuteNonQuery();
                Console.WriteLine(id);
                conn.Close();
            }
            return id > 0;
        }


    }
}