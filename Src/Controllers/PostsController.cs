using EvalApi.Src.Core.Exceptions;
using EvalApi.Src.Core.Services.Interfaces;
using EvalApi.Src.Models;
using EvalApi.Src.Views.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api")]
public sealed class PostsController : ControllerBase
{
    private readonly IPostService _posts;

    public PostsController(IPostService posts)
    {
        _posts = posts;
    }

    [HttpPost("users/{userId:int:min(1)}/posts")]
    public async Task<ActionResult<PostDto>> CreateForUser(
        [FromRoute] int userId,
        [FromBody] CreatePostRequestDto request,
        CancellationToken ct)
    {
        if (request.UserId != userId)
            throw new BadRequestException("userId mismatch between route and body");

        var model = new Post
        {
            UserId = request.UserId,
            Title = request.Title,
            Body = request.Body
        };

        var created = await _posts.CreateForUserAsync(userId, model, ct);
        var dto = MapToDto(created);

        return CreatedAtAction(nameof(GetById), new { postId = dto.Id }, dto);
    }

    [HttpGet("users/{userId:int:min(1)}/posts")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetByUser(
        [FromRoute] int userId,
        CancellationToken ct)
    {
        var posts = await _posts.GetByUserIdAsync(userId, ct);
        var dtos = posts.Select(MapToDto).ToList();
        return Ok(dtos);
    }

    [HttpGet("posts/{postId:int:min(1)}")]
    public async Task<ActionResult<PostDto>> GetById([FromRoute] int postId, CancellationToken ct)
    {
        var post = await _posts.GetByIdAsync(postId, ct);
        return Ok(MapToDto(post));
    }

    [HttpPut("posts/{postId:int:min(1)}")]
    public async Task<ActionResult<PostDto>> Update(
        [FromRoute] int postId,
        [FromBody] PostDto dto,
        CancellationToken ct)
    {
        if (dto.Id != postId)
            throw new BadRequestException("id mismatch between route and body");

        var updated = await _posts.UpdateTitleBodyAsync(postId, dto.UserId, dto.Title, dto.Body, ct);
        return Ok(MapToDto(updated));
    }

    [HttpDelete("posts/{postId:int:min(1)}")]
    public async Task<IActionResult> Delete([FromRoute] int postId, CancellationToken ct)
    {
        await _posts.DeleteAsync(postId, ct);
        return NoContent();
    }

    private static PostDto MapToDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            Title = post.Title,
            Body = post.Body
        };
    }
}
