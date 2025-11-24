using Ganss.XSS;

namespace SafeVault.Services;

public class Sanitizer
{
    private readonly HtmlSanitizer _sanitizer;

    public Sanitizer()
    {
        _sanitizer = new HtmlSanitizer();
        // Customize allowed tags/attrs if needed (demo: allow a tiny safe set)
        _sanitizer.AllowedTags.Clear();
        _sanitizer.AllowedTags.Add("b");
        _sanitizer.AllowedTags.Add("i");
        _sanitizer.AllowedAttributes.Add("href");
    }

    public string Sanitize(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var cleaned = new string(input.Where(c => !char.IsControl(c)).ToArray()).Trim();
        return _sanitizer.Sanitize(cleaned);
    }
}
