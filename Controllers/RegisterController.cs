using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    public class RegisterController : Controller
    {
        IMapper _mapper;
        IUsersService _userService;
        public RegisterController(IMapper mapper, IUsersService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public IActionResult Register()
        {
            SignUpViewModel viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {
                CreateUserDTO userToCreate = this._mapper.Map<CreateUserDTO>(viewModel);
                await this._userService.Create(userToCreate);
            }
            catch (DuplicatedEmailException ex)
            {
                this.ModelState.AddModelError("Email", ex.Message);
                return this.View(viewModel);
            }
            catch (DuplicateEntityException ex)
            {
                this.ModelState.AddModelError("Username", ex.Message);
                return this.View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            try
            {
                bool isUnuque = await this._userService.CheckEmailUniqueness(email);
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
                bool isUnuque = await this._userService.CheckUsernameUniqueness(username);
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
                bool isUnuque = await this._userService.CheckPhoneUniqueness(phoneNumber);
                return Json(true);
            }
            catch (DuplicateEntityException ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
