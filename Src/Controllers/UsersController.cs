using EvalApi.Src.Core.Services.Interfaces;
using EvalApi.Src.Models;
using EvalApi.Src.Views.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _users;

    public UsersController(IUserService users)
    {
        _users = users;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequestDto request, CancellationToken ct)
    {
        // Mapping manuel DTO -> Model
        var model = new User
        {
            Name = request.Name,
            Username = request.Username,
            Email = request.Email
        };

        var created = await _users.CreateAsync(model, ct);

        // Mapping manuel Model -> DTO
        var dto = MapToDto(created);

        return StatusCode(StatusCodes.Status201Created, dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken ct)
    {
        var users = await _users.GetAllAsync(ct);
        var dtos = users.Select(MapToDto).ToList();
        return Ok(dtos);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email
        };
    }
}
