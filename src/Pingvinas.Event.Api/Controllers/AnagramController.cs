using Microsoft.AspNetCore.Mvc;

namespace Pingvinas.Event.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnagramController : ControllerBase
{
    [HttpGet($"{nameof(AreAnagrams)}")]
    [ProducesResponseType(typeof(bool), 200)]
    public bool AreAnagrams(string word, string potentialAnagram)
    {
        if (word == null || potentialAnagram == null || word.Length != potentialAnagram.Length)
        {
            return false;
        }

        return word.Order().SequenceEqual(potentialAnagram.Order());
    }
}