using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsCalendar;

namespace EventsCalendar.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (EventsDataEntities db = new EventsDataEntities())
            {
                var events = db.Tables.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Table e)
        {
            var status = false;

            using (EventsDataEntities db = new EventsDataEntities())
            {
                if (e.EventId > 0)
                {
                    //update the event
                    var v = db.Tables.Where(x => x.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    db.Tables.Add(e);
                }

                db.SaveChanges();
                status = true;
            } 
                return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventId)
        {
            var status = false;
            using (EventsDataEntities db = new EventsDataEntities())
            {

              var v = db.Tables.Where(x => x.EventId == eventId).FirstOrDefault();
                if (v != null)
                {
                    db.Tables.Remove(v);
                    db.SaveChanges();
                    status = true;
                }                     
            }
            return new JsonResult {Data = new {status = status}};
        }
    }
}