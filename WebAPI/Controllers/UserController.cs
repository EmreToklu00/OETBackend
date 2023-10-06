using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(template: "getuserbyid")]
        [Authorize()]
        public ActionResult GetUserById(Guid userId)
        {
            var result = _userService.GetById(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);

            }
            return BadRequest(result.Message);

        }
    }
}
