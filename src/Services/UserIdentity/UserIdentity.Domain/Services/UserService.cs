using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;

namespace UserIdentity.Domain.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<UserModel> GetUserProfileAsync(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        
        //// ToDo implement error handling
        
        return _mapper.Map<UserModel>(user);
    }
}
