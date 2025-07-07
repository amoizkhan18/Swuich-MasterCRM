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
    public class CategoriesController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();

        // GET: Categories
        public ActionResult Index()
        {
            var categories = categoryManager.GetCategoryDtos();
            return View(categories);
        }


        // GET: Categories/PartialDetailsOnId/5
        public ActionResult PartialDetailsOnId(Guid? id)
        {
            CategoryDto category = categoryManager.GetCategoryOnId(id);

            return PartialView("_PartialCategoryDetail", category);
        }
        // GET: Categories/PartialDeleteOnId/5
        public ActionResult PartialDeleteOnId(Guid? id)
        {
            CategoryDto category = categoryManager.GetCategoryOnId(id);
            return PartialView("_PartialCategoryDelete", category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(categoryManager.Status, "StatusId", "Name");
            ViewBag.AssignedTeam = new SelectList(categoryManager.Teams, "TeamId", "Name");
            ViewBag.AssignedUser = new SelectList(categoryManager.Users, "UserId", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId();
                bool status = categoryManager.SubmitCategory(categoryDto, CurrentUserId);
                if (status == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.StatusId = new SelectList(categoryManager.Status, "StatusId", "Name", categoryDto.StatusId);
            ViewBag.AssignedTeam = new SelectList(categoryManager.Teams, "TeamId", "Name", categoryDto.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(categoryManager.Users, "UserId", "Name", categoryDto.AssignedUser);
            return View(categoryDto);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDto category = categoryManager.GetCategoryOnId(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(categoryManager.Status, "StatusId", "Name", category.StatusId);
            ViewBag.AssignedTeam = new SelectList(categoryManager.Teams, "TeamId", "Name", category.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(categoryManager.Users, "UserId", "Name", category.AssignedUser);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,Description,IsDeleted,StatusId,AssignedUser,AssignedTeam,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                string CurrentUserId = User.Identity.GetUserId();
                bool status = categoryManager.SubmitCategory(categoryDto, CurrentUserId,true);
                if (status == true)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.StatusId = new SelectList(categoryManager.Status, "StatusId", "Name", categoryDto.StatusId);
            ViewBag.AssignedTeam = new SelectList(categoryManager.Teams, "TeamId", "Name", categoryDto.AssignedTeam);
            ViewBag.AssignedUser = new SelectList(categoryManager.Users, "UserId", "Name", categoryDto.AssignedUser);
            return View(categoryDto);
        }


        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CategoryDto categoryDto = categoryManager.GetCategoryOnId(id);
            // pass current userid
            string CurrentUserId = User.Identity.GetUserId();
            bool status = categoryManager.SubmitCategory(categoryDto, CurrentUserId, true, true);
            return RedirectToAction("Index");
        }

      
    }
}
