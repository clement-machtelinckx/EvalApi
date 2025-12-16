using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public sealed class CreateUserRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
