using System.Net;
using System.Net.Http.Json;

using QuizyZunaAPI.Application.Questions.CreateQuestion;

namespace QuizyZunaAPI.Api.FunctionalTests.Questions;

public class GetAllQuestionTests(FunctionalTestWebAppFactory functionalTestWebAppFactory) : BaseFunctionalTest(functionalTestWebAppFactory)
{
    [Fact]
    public async Task ShouldReturn_200Ok_WhenQuestionsAreFound()
    {
        //Arrange
        CreateQuestionRequest createRequest = new("Is this a Question ?", "Yes", ["No", "Maybe", "?"],
                "Novice", "", ["Literature", "Mangas"]);
        await HttpClient.PostAsJsonAsync(BaseApiUrl, createRequest);

        //Act
        var response = await HttpClient.GetAsync(BaseApiUrl);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenDifficultiesIsInvalid()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?difficulties=???");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenThemesIsInvalid()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?themes=???");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenNumberOfQuestionsGreaterThan40()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?amount=41");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenNumberOfQuestionsLowerThan1()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?amount=0");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenDifficultiesIsEmpty()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?difficulties=");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn_400BadRequest_WhenThemesIsEmpty()
    {
        //Arrange
        var requestPath = new Uri(BaseApiUrl.OriginalString[..^1] + "?themes=");

        //Act
        var response = await HttpClient.GetAsync(requestPath);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
