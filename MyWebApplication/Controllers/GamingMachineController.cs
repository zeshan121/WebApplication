using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebApp.Web.Models.GamingMachine;
using AutoMapper;
using MyWebApp.Core.Interfaces;
using X.PagedList;
using X.PagedList.Mvc;
using MyWebApp.Core.Entities;
using System.Net;
using System.Data;

namespace MyWebApp.Web.Controllers
{
    public class GamingMachineController : Controller
    {
        #region Fields
        private readonly IGamingMachine _gamingMachine;
        private readonly IMapper _mapper;
        private const int PAGE_SIZE = 10;
        #endregion

        #region Ctor
        public GamingMachineController(IGamingMachine gamingMachine, IMapper mapper)
        {
            _gamingMachine = gamingMachine;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all gaming machines details base on current filter, search text or page number.
        /// </summary>
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (TempData["CurrentFilter"] != null)
                currentFilter = TempData["CurrentFilter"].ToString();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;
            int totalRecords;
            var model = _gamingMachine.Get(out totalRecords, pageIndex, PAGE_SIZE, ViewBag.CurrentFilter);
            var modelAsIPagedList = new StaticPagedList<GamingMachine>(model, pageIndex + 1, pageSize, totalRecords);
            ViewBag.OnePageOfGamingMachine = modelAsIPagedList;
            return View(model);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GamingSerialNumber,GamingMachinePosition,GameName")]GamingMachineInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelToCreate = _mapper.Map<GamingMachineInfoModel, GamingMachine>(model);
                    var result =_gamingMachine.CreateGamingMachine(modelToCreate);

                    if (result.Succeeded)
                        TempData["SuccessMessage"] = modelToCreate.GameName.ToString() + "'s record created successfully.";
                    else
                    {
                        TempData["ErrorMessage"] = result.ToString();
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(model);
        }
        //GET: GamingMachine/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gamingMachineModel = _gamingMachine.Get(Convert.ToInt64(id));
            var model = _mapper.Map<GamingMachine, GamingMachineInfoModel>(gamingMachineModel);

            if (gamingMachineModel == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //POST: GamingMachine/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "GamingSerialNumber,GamingMachinePosition,GameName")]GamingMachineInfoModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var modelToUpdate = _mapper.Map<GamingMachineInfoModel, GamingMachine>(model);
                    var result = _gamingMachine.UpdateGamingMachine(modelToUpdate);

                    if (result.Succeeded)
                        TempData["SuccessMessage"] = modelToUpdate.GameName.ToString() + "'s record updated successfully.";
                    else
                    {
                        TempData["ErrorMessage"] = result.ToString();
                        return View(model);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(model);


        }

        // GET: GamingMachine/Delete/5
        public ActionResult Delete(long id, string currentFilter)
        {
            var gamingMachine = _gamingMachine.Get(id);
            var result = _gamingMachine.DeleteGamingMachine(gamingMachine);

            if (result.Succeeded)
                TempData["SuccessMessage"] = gamingMachine.GameName.ToString() + "'s record deleted successfully.";
            else
                TempData["ErrorMessage"] = result.ToString();

            TempData["CurrentFilter"] = currentFilter;

            return RedirectToAction("Index");
        }
        #endregion
    }
}
