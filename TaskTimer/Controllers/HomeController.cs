using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.ModelBinding;
using System.Web.Mvc;
using TaskTimer.Models;

namespace TaskTimer.Controllers
{
    //[AllowCrossSiteJson]

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowCrossSiteJson]
        public JsonResult Message()
        {
            return Json(new { foo = "bar", baz = "Blech" }, JsonRequestBehavior.AllowGet);
        }

        [AllowCrossSiteJson]

        public JsonResult Projects()
        {
            using (TaskTimerDbContext context = new TaskTimerDbContext())
            {
                return Json(context.Projects.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Events(Event newEvent)
        {
            using (TaskTimerDbContext context = new TaskTimerDbContext())
            {
                if (newEvent.Id > 0)
                {
                    context.Events.Attach(newEvent);
                    context.ChangeTracker.DetectChanges();
                    context.Entry(newEvent).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.Events.Add(newEvent);
                    context.SaveChanges();
                }

                return Json(newEvent.Id);
            }
        }

        [AllowCrossSiteJson]

        public JsonResult EventHistory()
        {
            using (TaskTimerDbContext context = new TaskTimerDbContext())
            {
                var vrlEvents = (from vrlEvent in context.Events
                                 where vrlEvent.FinishDate != null
                                 select new { StartDate = vrlEvent.StartDate, FinishDate = vrlEvent.FinishDate,vrlEvent.Id,vrlEvent.Description}).
                    ToList();
                return Json(vrlEvents, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CurrenEvent()
        {
            using (TaskTimerDbContext context = new TaskTimerDbContext())
            {
                var vrlEvents = (from vrlEvent in context.Events
                                 where vrlEvent.FinishDate == null
                                 select new { StartDate = vrlEvent.StartDate, FinishDate = vrlEvent.FinishDate, vrlEvent.Id, vrlEvent.Description }).
                    ToList();


                var vrlLastNotFinishedTask = vrlEvents.LastOrDefault();

                return Json(vrlLastNotFinishedTask, JsonRequestBehavior.AllowGet);
            }
        }
    }
}