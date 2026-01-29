using Pingvinas.Event.Api.Controllers;

namespace TestProject;

public class AnagramTests
{
    private readonly AnagramController _anagramController = new();


    [Fact]
    public void AreAnagramsReturnsTrueIfAnagram() =>
        Assert.True(_anagramController.AreAnagrams("dusty", "study"));

    [Fact]
    public void AreAnagramsReturnsFalseIfNotAnagram() =>
        Assert.False(_anagramController.AreAnagrams("apple", "banana"));

    [Fact]
    public void AreAnagramsReturnsTrueIfIdentical() =>
        Assert.True(_anagramController.AreAnagrams("apple", "apple"));

    [Fact]
    public void AreAnagramsReturnsFalseIfOnlySimilarCharacters() =>
        Assert.False(_anagramController.AreAnagrams("elephant", "elaphant"));

    [Fact]
    public void AreAnagramsReturnsFalseIfOnlyOneCharacters() =>
        Assert.False(_anagramController.AreAnagrams("eeeeeeee", "elephant"));

    [Fact]
    public void AreAnagramsReturnsTrueIfAnagramWithSpaces() =>
        Assert.True(_anagramController.AreAnagrams("the morse code", "here come dots"));
}