using Core.Security.Dtos;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.Auths.Commands.Login;
using Kodlama.io.Devs.Application.Features.Auths.Commands.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var registerCommand = new RegisterCommand
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };
            var result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var loginCommand = new LoginCommand
            {
                UserForLoginDto = userForLoginDto,
                IpAddress = GetIpAddress()
            };

            var result = await Mediator!.Send(loginCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
