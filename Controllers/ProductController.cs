//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Helpers;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Web.Script.Serialization;
//using Crud_Using_javascript.DAL;
//using Crud_Using_javascript.Models;
//using Newtonsoft.Json;

//namespace Crud_Using_javascript.Controllers
//{
//    public class ProductController : Controller
//    {
//        Product_DAL _product_DAL = new Product_DAL();
//        // GET: Product
//        public JsonResult Index()
//        {
//            var product_list = _product_DAL.GetProducts();

//            return Json(product_list, JsonRequestBehavior.AllowGet);
//        }

//        // GET: Product/Details/5
//        public ActionResult Details(int id)
//        {
//            var productlist = _product_DAL.getProdById(id);
//            return Json(productlist, JsonRequestBehavior.AllowGet);
//        }

//        // GET: Product/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Product/Create
//        [System.Web.Mvc.HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Product/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: Product/Edit/5
//        [System.Web.Mvc.HttpPost]
//        public JsonResult Edit(Products _product)
//        {
//            // Ensure the CORS headers are added manually in the response
//            Response.Headers["Access-Control-Allow-Origin"] = "http://127.0.0.1:5500";
//            Response.Headers["Access-Control-Allow-Methods"] = "*";
//            Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, application/Json";

//            if (_product == null)
//            {
//                return Json(new { message = "Null Values Received!" }, JsonRequestBehavior.AllowGet);
//            }

//            try
//            {
//                bool isUpdated = _product_DAL.Update_prod(_product);
//                return Json(new { message = isUpdated ? "success" : "failed" }, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception e)
//            {
//                return Json(new { message = e.Message }, JsonRequestBehavior.AllowGet);
//            }
//        }


//        // GET: Product/Delete/5
//        public JsonResult Delete(int id)
//        {
//            bool isdeleted = false;
//            try
//            {
//                isdeleted = _product_DAL.deleteItem(id);
//                if (isdeleted)
//                {
//                    return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);
//                }
//                else
//                {
//                    return Json(new { message = "failed!" }, JsonRequestBehavior.AllowGet);
//                }

//                //return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {

//                return Json(new { message = e.Message }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        // POST: Product/Delete/5
//        [System.Web.Mvc.HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Crud_Using_javascript.DAL;
using Crud_Using_javascript.Models;
using Newtonsoft.Json;
namespace Crud_Using_javascript.Controllers
{
    [EnableCors(origins: "http://127.0.0.1:5500", headers: "*", methods: "*")]
    public class ProductController : Controller
    {
        Product_DAL _product_DAL = new Product_DAL();
        public ActionResult Index()
        {
            
            return View();
        }
        // GET: Product/Create
        public ActionResult Create()
        {
            try
            {
                // Read the JSON data from the request body
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                var _product = JsonConvert.DeserializeObject<Products>(jsonString);

                if (_product == null)
                    return Json(new { message = "Null Values Received!" }, JsonRequestBehavior.AllowGet);

                bool isUpdated = _product_DAL.Insert_prod(_product);
                return Json(new { message = isUpdated ? "success" : "failed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = "Error occurred", details = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Product/get_prods
        public JsonResult get_prods()
        {
            var product_list = _product_DAL.GetProducts();
            return Json(product_list, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Details/5
        public JsonResult Details(int id)
        {
            var productlist = _product_DAL.getProdById(id);
            return Json(productlist, JsonRequestBehavior.AllowGet);
        }

        // POST: Product/Edit/5
        // POST: Product/Edit/5
        [System.Web.Mvc.HttpPost]
        public JsonResult Edit()
        {
            try
            {
                // Read the JSON data from the request body
                var jsonString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                var _product = JsonConvert.DeserializeObject<Products>(jsonString);

                if (_product == null)
                    return Json(new { message = "Null Values Received!" }, JsonRequestBehavior.AllowGet);

                bool isUpdated = _product_DAL.Update_prod(_product);
                return Json(new { message = isUpdated ? "success" : "failed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = "Error occurred", details = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Product/Delete/5
        public JsonResult Delete(int id)
        {
            try
            {
                bool isdeleted = _product_DAL.deleteItem(id);
                return Json(new { message = isdeleted ? "Success" : "failed!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = "Error occurred", details = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Product/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool isdeleted = _product_DAL.deleteItem(id);
                if (isdeleted)
                    return RedirectToAction("Index");

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
