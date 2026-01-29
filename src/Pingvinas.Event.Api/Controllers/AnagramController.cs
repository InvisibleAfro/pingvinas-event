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
        // TODO: Eirik says this is not correct, whattodo?
        bool result = word == potentialAnagram;
        return result;
    }
}