using productassignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace productassignment.Controllers
{
    public class HomeController : Controller
    {
        storeEntities db = new storeEntities();

        public ActionResult Products(string q, string S)
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        
            
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Insert()
        {
            try
            {
                string ProductName = Request["txtPName"].ToString();
                int CatId = Convert.ToInt32(Request["Categories"].ToString());
                int NextId = db.Products.Max(p => (int)p.ProductId) + 1;

                Product NewProduct = new Product();
                NewProduct.ProductId = NextId;
                NewProduct.ProductName = ProductName;
                db.Products.Add(NewProduct);
                db.SaveChanges();
                TempData["Message"] = "Reccord saved successfully";
            }
            catch
            {
                TempData["Message"] = "Error while saving record";
            }
            return RedirectToAction("Products");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product editProduct = db.Products.Find(id);
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            if (editProduct == null)
            {
                return HttpNotFound();
            }
            return View(editProduct);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId.ProductName")] Product editProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int CatId = Convert.ToInt32(Request["Categories"].ToString());
                    editProduct.CatId = CatId;
                    db.Entry(editProduct).State = EntityState.Modified;
                    db.SaveChanges();
                    editProduct = null;
                    TempData["Message"] = "Record updated successfully";
                    return RedirectToAction("Products");
                }
            }
            catch
            {
                TempData["Message"] = "Error while updating record";
            }
            return RedirectToAction("Products");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product deleteProduct = db.Products.Find(id);
            if (deleteProduct == null)
            {
                return HttpNotFound();
            }
            return View(deleteProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product deleteProduct = db.Products.Find(id);
                db.Products.Remove(deleteProduct);
                db.SaveChanges();
                deleteProduct = null;
                TempData["Message"] = "Record Deleted sucessfully";
                return RedirectToAction("Products");
            }
            catch
            {
                TempData["Message"] = "Error while deleting record";
            }
            return RedirectToAction("Products");
        }

    }
}
        
    
