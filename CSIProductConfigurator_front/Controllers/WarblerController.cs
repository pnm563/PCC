﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSISalesConfigurator_front.Controllers
{
    public class WarblerController : Controller
    {
        // GET: Warbler
        public ActionResult Index()
        {
            return View();
        }

        // GET: Warbler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Warbler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warbler/Create
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

        // GET: Warbler/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Warbler/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Warbler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Warbler/Delete/5
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