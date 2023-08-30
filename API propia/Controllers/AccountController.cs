using API_propia.Data_Models;
using API_propia.DataAccess;
using API_propia.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_propia.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly HotelDBContext _hotelDBContext;
        public AccountController(JwtSettings jwtSettings, HotelDBContext hotelDBContext)
        {
            _jwtSettings = jwtSettings;
            _hotelDBContext = hotelDBContext;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var searchUser = _hotelDBContext.Users.Select(x => x).Where(x => x.Name == userLogin.UserName && x.Password == userLogin.Password).FirstOrDefault(); 
                
                if (searchUser != null)
                {
                    Token = JwtHelpers.GenerateTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.EmailAddress,
                        Id = searchUser.Id,
                        GuiId = Guid.NewGuid()
                    }, _jwtSettings);
                }

                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token); 

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(_hotelDBContext.Users);
        }

    }
}
