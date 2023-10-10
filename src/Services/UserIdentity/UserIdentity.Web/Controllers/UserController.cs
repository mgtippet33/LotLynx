using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Web.Models;

namespace UserIdentity.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                     ?? string.Empty;
        
        var userProfile = await _userService.GetUserProfileAsync(userId);
        var result = _mapper.Map<UserProfileResponseModel>(userProfile);
        
        return Ok(result);
    }
}