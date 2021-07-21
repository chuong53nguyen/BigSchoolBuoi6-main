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
    public class FollowController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(Followinga follow)
        {
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login first!");
            if (userID == follow.FolloweeId)
                return BadRequest("Can not follow myself!");
            Model1 context = new Model1();
           
            Followinga find = context.Followingas.FirstOrDefault(p => p.FollowerId ==
            userID && p.FolloweeId == follow.FolloweeId);
            if (find != null)
            {
                context.Followingas.Remove(context.Followingas.SingleOrDefault(p => p.FollowerId == userID && p.FolloweeId == follow.FolloweeId));
                context.SaveChanges();
                return Ok("cancel");
            }
            //set object follow
            follow.FollowerId = userID;
            context.Followingas.Add(follow);
            context.SaveChanges();
            return Ok();
        }
    }
}
