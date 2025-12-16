using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public sealed class PostDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;
}
