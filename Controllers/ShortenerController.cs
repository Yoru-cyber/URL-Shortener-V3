using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using URL_Shortener.Context;
using Url_Shortener.Dtos;
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
    public async Task<IActionResult> Index()
    {
        var shortenedUrls = await _context.ShortenedUrls.OrderBy(u => u.Id).Take(10).ToListAsync();
        return Ok(shortenedUrls);
    }


    // POST api/url/encode
    [HttpPost("encode")]
    public async Task<IActionResult> EncodeUrl([FromForm] UrlDtoRequest request)
    {
        if (string.IsNullOrEmpty(request.Url)) return BadRequest("URL cannot be empty");

        // Generate a unique number based on the URL and encode it to Base62
        var uniqueNumber = Base62Encoder.GenerateUniqueNumber(request.Url);
        var encodedUrl = Base62Encoder.Encode(uniqueNumber);
        var u = new ShortenedUrl { originalUrl = request.Url, shortenedUrl = encodedUrl };
        EntityEntry<ShortenedUrl> newShortenedUrl = await _context.AddAsync(u);
        //Converting the new entry to DTO so it only has necessary info to consume
        var urlDto = new UrlDtoResponse(newShortenedUrl.Entity.Id, newShortenedUrl.Entity.originalUrl,
            newShortenedUrl.Entity.shortenedUrl);
        //Returns the number of writes, in this case it should be one so if it is bigger than 0, return Ok
        var saveTask = await _context.SaveChangesAsync();
        if (saveTask > 0) return Created($"shortenedUrls/{urlDto.Id}", urlDto);
        return Conflict("Could not add URL");
    }

    [HttpGet("Url/{id:int}")]
    public async Task<IActionResult> GetShortenedUrl(int id)
    {
        if (id < 0) return BadRequest("Invalid id");
        var shortenedUrl = await _context.ShortenedUrls.FindAsync(id);
        if (shortenedUrl is null) return NotFound();
        return Ok(shortenedUrl);
    }

    [HttpGet("{shortenedUrl}")]
    public async Task<IActionResult> DecodeUrl([FromRoute] string? shortenedUrl)
    {
        if (shortenedUrl == null) return Redirect("/");
        var u = await _context.ShortenedUrls.Where(url => url.shortenedUrl == shortenedUrl).FirstOrDefaultAsync();
        if (u == null) return NotFound("Could not find shortened url");
        var urlDto = new UrlDtoResponse(u.Id, u.originalUrl, u.shortenedUrl);
        return Redirect(urlDto.OriginalUrl);
    }
}