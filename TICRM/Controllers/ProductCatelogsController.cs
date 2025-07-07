using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DAL;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    public class ProductCatelogsController : Controller
    {
        private ProductCatelogManager catelogManager = new ProductCatelogManager();
        private CategoryManager categoryManager = new CategoryManager();

        // GET: ProductCatelogs
        public ActionResult Index()
        {
            var productCatelogs = catelogManager.GetProductCatelogDtos();
            ViewBag.CategoryId = categoryManager.GetCategoryDtos();
            return View(productCatelogs.ToList());
        }


        // GET: ProductCatelogs/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            ProductCatelogDTO productCatelogDTO = catelogManager.GetProductCatelogOnId(id);
            return PartialView("_PartialProductDetails", productCatelogDTO);
        }
        // GET: ProductCatelogs/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            ProductCatelogDTO productCatelogDTO = catelogManager.GetProductCatelogOnId(id);
            return PartialView("_PartialProductDelete", productCatelogDTO);
        }

        // GET: ProductCatelogs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryManager.GetCategoryDtos(), "CategoryId", "Name");
            ViewBag.StatusId = new SelectList(catelogManager.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(catelogManager.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(catelogManager.Users, "UserId", "Name");
            return View();
        }

        // POST: ProductCatelogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,SerialNumber,ProductName,CategoryId,ValidFrom,ValidTo,Description,ProductNote,StatusId,IsDeleted,AssignedUser,AssignedTeam,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] ProductCatelogDTO productCatelog)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId();
                bool status = catelogManager.SubmitProductCatelog(productCatelog, CurrentUserId);
                if (status == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(categoryManager.GetCategoryDtos(), "CategoryId", "Name", productCatelog.CategoryId);
            ViewBag.StatusId = new SelectList(catelogManager.Status, "StatusId", "Name", productCatelog.StatusId);
            ViewBag.AssignedTeam = new SelectList(catelogManager.Teams, "TeamId", "Name", productCatelog.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(catelogManager.Users, "UserId", "Name", productCatelog.AssignedUser);
            return View(productCatelog);
        }

        // GET: ProductCatelogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatelogDTO productCatelog = catelogManager.GetProductCatelogOnId(id); ;
            if (productCatelog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryManager.GetCategoryDtos(), "CategoryId", "Name", productCatelog.CategoryId);
            ViewBag.StatusId = new SelectList(catelogManager.Status, "StatusId", "Name", productCatelog.StatusId);
            ViewBag.AssignedTeam = new SelectList(catelogManager.Teams, "TeamId", "Name", productCatelog.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(catelogManager.Users, "UserId", "Name", productCatelog.AssignedUser);
            return View(productCatelog);
        }

        // POST: ProductCatelogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,SerialNumber,ProductName,CategoryId,ValidFrom,ValidTo,Description,ProductNote,StatusId,IsDeleted,AssignedUser,AssignedTeam,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] ProductCatelogDTO productCatelog)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId();
                bool status = catelogManager.SubmitProductCatelog(productCatelog, CurrentUserId, true);
                if (status == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(categoryManager.GetCategoryDtos(), "CategoryId", "Name", productCatelog.CategoryId);
            ViewBag.StatusId = new SelectList(catelogManager.Status, "StatusId", "Name", productCatelog.StatusId);
            ViewBag.AssignedTeam = new SelectList(catelogManager.Teams, "TeamId", "Name", productCatelog.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(catelogManager.Users, "UserId", "Name", productCatelog.AssignedUser);
            return View(productCatelog);
        }

        // GET: ProductCatelogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCatelogDTO productCatelog = catelogManager.GetProductCatelogOnId(id);
            if (productCatelog == null)
            {
                return HttpNotFound();
            }
            return View(productCatelog);
        }

        // POST: ProductCatelogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductCatelogDTO productCatelog = catelogManager.GetProductCatelogOnId(id);
            // pass current userid
            string CurrentUserId = User.Identity.GetUserId();
            bool status = catelogManager.SubmitProductCatelog(productCatelog, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

    }
}
