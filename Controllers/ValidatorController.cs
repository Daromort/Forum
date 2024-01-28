using Forum_Management_System.Exceptions;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    public class ValidatorController : Controller
    {
        private readonly IUsersService _usersService;

        public ValidatorController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            try
            {
                bool isUnuque = await this._usersService.CheckEmailUniqueness(email);
                return Json(true);
            }
            catch (DuplicatedEmailException ex)
            {
                return Json(ex.Message);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameUnique(string username)
        {
            try
            {
                bool isUnuque = await this._usersService.CheckUsernameUniqueness(username);
                return Json(true);
            }
            catch (DuplicateEntityException ex)
            {
                return Json(ex.Message);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsPhoneUnique(string phoneNumber)
        {
            try
            {
                bool isUnuque = await this._usersService.CheckPhoneUniqueness(phoneNumber);
                return Json(true);
            }
            catch (DuplicateEntityException ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
