using System.ComponentModel.DataAnnotations;

namespace SafeVault.Models;

public class UserInputDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(0, 1000000)]
    public decimal Amount { get; set; }

    [RegularExpression(@"^[A-Za-z0-9 _\-]+$", ErrorMessage = "Only letters, numbers, spaces, hyphens and underscores allowed.")]
    public string FreeText { get; set; } = string.Empty;
}
