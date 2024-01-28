using Forum_Management_System.Helpers;
using Forum_Management_System.Models;
using Forum_Management_System.Services;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Forum_Management_System.Controllers.Api
{
    public class AuthenticationController : ControllerBase
    {
        AuthenticationService _authManager;
        public AuthenticationController(AuthenticationService authManager)
        {
            _authManager = authManager;
        }
       
        [HttpPost]
        [Route("/api/token")]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequestDTO tokenRequest)
        {
           
            var token  =  await _authManager.RequestToken(tokenRequest.Email, tokenRequest.Password);
            
            return Ok(token);
        }
    }
}
