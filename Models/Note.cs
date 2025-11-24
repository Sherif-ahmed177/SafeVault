namespace SafeVault.Models;

public class Note
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty; // sanitized content
    public string? RawHtml { get; set; } // if user provided HTML (will be sanitized)
}
