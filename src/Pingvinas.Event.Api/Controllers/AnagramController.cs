using Microsoft.AspNetCore.Mvc;

namespace Pingvinas.Event.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnagramController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<bool> AreAnagrams(string word, string potentialAnagram)
    {
        if (word == null || potentialAnagram == null)
            return BadRequest("Both strings must be non-null");

        if (word.Length != potentialAnagram.Length)
            return Ok(false);

        return Ok(word.Order().SequenceEqual(potentialAnagram.Order()));
    }
}