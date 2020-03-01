using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honours_Project_Test_2.Models.ViewModels.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalkAPI.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Honours_Project_Test_2.Controllers
{
    [Route("api/schedule")]
    public class ScheduleController : Controller
    {
        private readonly AppDB _db;

        public ScheduleController(AppDB db)
        {
            _db = db;
        }

        [HttpGet]
        public List<SessionViewModel> Schedule()
        {
            return Get_All_Schedules();
        }

        [HttpGet]
        [Route("get/all")]
        public List<SessionViewModel> Get_All_Schedules()
        {
            var dbSessions = _db.Sessions.ToList();

            var formattedSessions = new List<SessionViewModel>();

            foreach (var session in dbSessions)
            {
                formattedSessions.Add(new SessionViewModel()
                {
                    ID = session.ID,
                    Name = session.Name,
                    Description = session.Description,
                    Start = session.Start.ToShortDateString(),
                    End = session.End.ToShortTimeString()
                });
            }

            return formattedSessions;
        }

        [HttpGet]
        [Route("get/user/schedule/{userID}")]
        public List<Calendar_Event> GetUserSchedule(string userID)
        {
            List<Calendar_Event> events = new List<Calendar_Event>();

            var interests = _db.User_Talk_Interests.Include(x => x.Talk).ThenInclude(x => x.Sessions).ThenInclude(x => x.Session).Where(x => x.User_ID == userID).ToList();

            if (interests.Count() > 0)
            {
                foreach (var interest in interests)
                {
                    var start = new DateTime(
                        interest.Talk.Sessions.FirstOrDefault().Session.Start.Year,
                        interest.Talk.Sessions.FirstOrDefault().Session.Start.Month,
                        interest.Talk.Sessions.FirstOrDefault().Session.Start.Day,
                        interest.Talk.Start.Hours,
                        interest.Talk.Start.Minutes,
                        0);

                    var end = new DateTime(
                        interest.Talk.Sessions.FirstOrDefault().Session.End.Year,
                        interest.Talk.Sessions.FirstOrDefault().Session.End.Month,
                        interest.Talk.Sessions.FirstOrDefault().Session.End.Day,
                        interest.Talk.End.Hours,
                        interest.Talk.End.Minutes,
                        0);

                    events.Add(new Calendar_Event()
                    {
                        Title = interest.Talk.Title,
                        Start = start,
                        End = end,
                        AllDay = false
                    });
                }
            }

            return events;
        }
    }

    public class Calendar_Event
    {
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool AllDay { get; set; }
    }
}
