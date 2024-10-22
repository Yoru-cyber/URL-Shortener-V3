using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Context;
using Url_Shortener.DTOs;
using URL_Shortener.Entities;
using URL_Shortener.Utils;

namespace URL_Shortener.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShortenerController : ControllerBase
{
    private readonly ShortenedUrlContext _context;

    public ShortenerController(ShortenedUrlContext shortenedUrlContext)
    {
        _context = shortenedUrlContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> index()
    {
        return Ok();
    }


    // POST api/url/encode
    [HttpPost("encode")]
    public async Task<IActionResult> EncodeUrl([FromForm] UrlDTORequest request)
    {
        if (string.IsNullOrEmpty(request.Url)) return BadRequest("URL cannot be empty");

        // Generate a unique number based on the URL and encode it to Base62
        var uniqueNumber = Base62Encoder.GenerateUniqueNumber(request.Url);
        var encodedUrl = Base62Encoder.Encode(uniqueNumber);
        var u = new ShortenedUrl { originalUrl = request.Url, shortenedUrl = encodedUrl };
        var newShortenedUrl = await _context.AddAsync(u);
        var saveTask = await _context.SaveChangesAsync();
        if (saveTask > 0) return Ok(newShortenedUrl.Entity);
        return Conflict("Could not add URL");
    }

    [HttpGet("{shortenedUrl}")]
    public async Task<IActionResult> DecodeUrl([FromRoute] string? shortenedUrl)
    {
        if (shortenedUrl == null) return Redirect("/");
        var u = await _context.ShortenedUrls.Where(url => url.shortenedUrl == shortenedUrl).FirstOrDefaultAsync();
        if (u == null) return NotFound("Could not find shortened url");
        return Redirect(u.originalUrl);
    }
}