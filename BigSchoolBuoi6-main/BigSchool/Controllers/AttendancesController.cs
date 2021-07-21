using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    public class AttendancesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Attent(Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            Model1 context = new Model1();
            if (context.Attendances.Any(p => p.Attendee == userID && p.CourseID == attendanceDto.Id))
            {
                context.Attendances.Remove(context.Attendances.SingleOrDefault(p =>p.Attendee == userID && p.CourseID == attendanceDto.Id));
                context.SaveChanges();
                return Ok("cancel");
            }
            var attendence = new Attendance() { CourseID = attendanceDto.Id, Attendee = User.Identity.GetUserId() };
            context.Attendances.Add(attendence);
            context.SaveChanges();
            return Ok();
        }
    }
}
