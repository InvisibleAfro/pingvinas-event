using Microsoft.AspNetCore.Mvc;
using Pingvinas.Event.Api.Controllers;

namespace TestProject;

public class AnagramTests
{
    private readonly AnagramController _anagramController = new();


    [Fact]
    public void AreAnagramsReturnsTrueIfAnagram()
    {
        var result = _anagramController.AreAnagrams("dusty", "study");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(Assert.IsType<bool>(okResult.Value));
    }

    [Fact]
    public void AreAnagramsReturnsFalseIfLengthsDoNotMatch()
    {
        var result = _anagramController.AreAnagrams("apple", "banana");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.False(Assert.IsType<bool>(okResult.Value));
    }

    [Fact]
    public void AreAnagramsReturnsTrueIfIdentical()
    {
        var result = _anagramController.AreAnagrams("apple", "apple");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(Assert.IsType<bool>(okResult.Value));
    }

    [Fact]
    public void AreAnagramsReturnsFalseIfOnlySimilarCharacters()
    {
        var result = _anagramController.AreAnagrams("elephant", "elaphant");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.False(Assert.IsType<bool>(okResult.Value));
    }

    [Fact]
    public void AreAnagramsReturnsFalseIfOnlyOneCharacters()
    {
        var result = _anagramController.AreAnagrams("eeeeeeee", "elephant");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.False(Assert.IsType<bool>(okResult.Value));
    }

    [Fact]
    public void AreAnagramsReturnsTrueIfAnagramWithSpaces()
    {
        var result = _anagramController.AreAnagrams("the morse code", "here come dots");
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(Assert.IsType<bool>(okResult.Value));
    }
}
