using Microsoft.AspNetCore.Mvc;
using SafeVault.Models;
using SafeVault.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SafeVault.Services;

namespace SafeVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly Sanitizer _sanitizer;

    public VaultController(ApplicationDbContext db, Sanitizer sanitizer)
    {
        _db = db;
        _sanitizer = sanitizer;
    }

    // Public: list users (demonstrates EF parameterized query)
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _db.Users.AsNoTracking().ToListAsync();
        return Ok(users);
    }

    // Create a note (validates input, sanitizes HTML)
    [HttpPost("notes")]
    public async Task<IActionResult> CreateNote([FromBody] UserInputDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var note = new Note
        {
            Content = _sanitizer.Sanitize(dto.FreeText),
            RawHtml = _sanitizer.Sanitize(dto.FreeText)
        };

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    [HttpGet("notes/{id:int}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note == null) return NotFound();
        // Return sanitized content; clients should still treat it as untrusted
        return Ok(new { note.Id, Content = note.Content, RawHtml = note.RawHtml });
    }

    // Admin only example endpoint
    [Authorize(Policy = "RequireAdmin")]
    [HttpDelete("users/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
