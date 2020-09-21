using Api_jwt.Middleware;
using Api_jwt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Api_jwt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IJWTManager _jWTManager;

        public LoginController(IJWTManager jWTManager)
        {
            _jWTManager = jWTManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            IActionResult response = Unauthorized();

            UserModel model = new UserModel
            {
                Username = username,
                Password = password
            };

            var user = _jWTManager.AuthenticateUser(model);

            if (user != null)
            {
                var tokenStr = _jWTManager.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
                return response;
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var username = claim[0].Value;

            return "Welcome To : " + username;
        }

        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[]
            {
                "Value 1","Value 2","Value 3"
            };
        }
    }
}