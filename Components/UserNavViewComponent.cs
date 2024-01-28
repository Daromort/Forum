using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum_Management_System.Models;
using Forum_Management_System.Services.Interfaces;
using AutoMapper;
using Forum_Management_System.Models.View;

public class UserNavbarViewComponent : ViewComponent
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public UserNavbarViewComponent(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimID = ((ClaimsPrincipal)User).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = new User();
        if (claimID != null)
        {
            user = await _usersService.GetUserByID(int.Parse(claimID));
        }
        var userMini = _mapper.Map<UserViewModelMini>(user);
        return View(userMini);
    }
}
