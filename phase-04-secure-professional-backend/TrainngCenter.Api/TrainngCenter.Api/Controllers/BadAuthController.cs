/* using Microsoft.AspNetCore.Mvc;
using System;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class BadAuthController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public BadAuthController(AppDbContext db) { this.db = db; }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null) return Ok("wrong email");
            if (user.PasswordHash != request.Password) return Ok("wrong password");
            var token = "fake-token-" + user.Id;
            return Ok(new { token = token, user = user });
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var user = new ApplicationUser();
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.PasswordHash = request.Password;
            user.Role = request.Role;
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}
*/