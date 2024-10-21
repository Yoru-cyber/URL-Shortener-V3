using Microsoft.AspNetCore.Mvc;
using Url_Shortener.DTOs;
using URL_Shortener.Utils;

namespace URL_Shortener.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShortenerController : ControllerBase
{
    // POST api/url/encode
    [HttpPost("encode")]
    public async Task<IActionResult> EncodeUrl([FromForm] UrlPostRequest request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            return BadRequest("URL cannot be empty");
        }
        
        // Generate a unique number based on the URL and encode it to Base62
        var uniqueNumber = Base62Encoder.GenerateUniqueNumber(request.Url);
        var encodedUrl = Base62Encoder.Encode(uniqueNumber);

        return Ok(new { originalUrl = request.Url, shortenedUrl = encodedUrl });
    }
}