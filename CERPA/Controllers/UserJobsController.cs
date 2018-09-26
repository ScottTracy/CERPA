using CERPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;

namespace CERPA.Controllers
{
    public class UserJobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserJobs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeSearch(string searchTerm)
        {
            List<UserJobsViewmodel> UserJobs = new List<UserJobsViewmodel>();
            var users = db.Users.Where(x => x.UserName.Contains(searchTerm) || searchTerm == null).ToList();
            foreach(var user in users)
            {
                var jobs = db.Jobs.Where(y => y.UserID == user.Id && y.IsConfirmed == true).Select(y => y).ToList();
                foreach(var job in jobs)
                {
                    UserJobs.Add(new UserJobsViewmodel
                    {
                        Job = job,
                        User = user
                    });
                }
            }

            return View(UserJobs);
        }
        public TimeSpan? GetTime(Job job)
        {
            return job.ConfirmedOn - job.Start;
        }
        public TimeSpan? GetRoutedTime(Job job)
        {
            return db.PartProcesses.Where(x => x.PartID == job.PartID).Select(x => x.ProcessTime).First();
        }
        public double GetPercentage(Job job)
        {
            var time = GetTime(job);
            var routedTime = GetRoutedTime(job);
            return ((double)routedTime.Value.Ticks / (double)time.Value.Ticks)*(100);
        }
    }
}