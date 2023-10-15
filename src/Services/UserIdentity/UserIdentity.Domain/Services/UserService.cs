using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Data;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Domain.Infrastructure.Exceptions;

namespace UserIdentity.Domain.Services;

public class UserService : BaseApplicationService, IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IServiceProvider serviceProvider, AppDbContext context, IMapper mapper) : base(serviceProvider)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserModel> GetUserProfileAsync(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user != null
            ? _mapper.Map<UserModel>(user)
            : throw new NotFoundUserException();
    }
}
