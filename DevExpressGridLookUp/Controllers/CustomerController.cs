using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevExpressGridLookUp.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        DevExpressGridLookUp.Models.AdventureWorksDW2017Entities db = new DevExpressGridLookUp.Models.AdventureWorksDW2017Entities();

        [ValidateInput(false)]
        public ActionResult GridLookupPartial()
        {
            var model = db.DimCustomer;
            return PartialView("_GridLookupPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] DevExpressGridLookUp.Models.DimCustomer item)
        {
            var model = db.DimCustomer;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridLookupPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] DevExpressGridLookUp.Models.DimCustomer item)
        {
            var model = db.DimCustomer;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerKey == item.CustomerKey);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridLookupPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridLookupPartialDelete(System.Int32 CustomerKey)
        {
            var model = db.DimCustomer;
            if (CustomerKey >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerKey == CustomerKey);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridLookupPartial", model.ToList());
        }
    }
}