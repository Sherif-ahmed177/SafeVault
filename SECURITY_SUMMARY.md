Vulnerabilities identified:
- SQL injection risk from concatenated SQL (fixed by using EF Core LINQ and parameterized access)
- Stored XSS where user-supplied HTML was rendered without sanitization (fixed by HtmlSanitizer)
- Missing server-side validation (fixed via DataAnnotations and ModelState checks)

How Copilot assisted:
- Generated validation DTO templates and suggested sanitizer usage and test scaffolding which were adapted and hardened manually.
