using ProductMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMvc.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> prod = new List<Product>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from products";

            try
            {
                SqlDataReader dar = cmd.ExecuteReader();
                while(dar.Read())
                {
                    prod.Add(new Product { ProductId = (int)dar["ProductId"], ProductName = (string)dar["ProductName"], Rate = (decimal)dar["Rate"], Description = (string)dar["Description"], CategoryName = (string)dar["CategoryName"] });
                }
                dar.Close();
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                cn.Close();

            }
            return View(prod);

        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from products where ProductId =@ProductId";
            cmd.Parameters.AddWithValue("@ProductId", id);
            SqlDataReader dar1 = cmd.ExecuteReader();
            Product obj = null;
            if(dar1.Read())
            {
                obj = new Product { ProductId = id, ProductName = dar1.GetString(1), Rate = dar1.GetDecimal(2), Description = dar1.GetString(3), CategoryName = dar1.GetString(4) };
            }
            else
            {
                ViewBag.ErrorMessage = "Finish";

            }

            cn.Close();
            return View(obj);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update Product set ProductName=@ProductName ,Rate=@Rate, Description=@Description,CategoryName=@CategoryName";
            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.Parameters.AddWithValue("@ProductName",obj.ProductName);
            cmd.Parameters.AddWithValue("@Rate", obj.Rate);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);
            




            try
            {
                cmd.ExecuteNonQuery();

               return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

            finally
            {
                cn.Close();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
