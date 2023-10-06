using Business.Abstract;
using Entity.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(template: "login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.IsSuccess)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost(template: "register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            //TODO: Validasyon çalışmıyor .
            var userExists = _authService.UserExist(userForRegisterDto.Email);
            if (!userExists.IsSuccess)
            {
                return BadRequest(userExists.Message);

            }
            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        /*[HttpPost(template: "transaction")]
        public IActionResult TransactionTest(UserForRegisterDto userForRegisterDto)
        {
            var result = _authService.TransactionalOperation(userForRegisterDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
                return BadRequest(result.Message);

        }*/
    }
}
